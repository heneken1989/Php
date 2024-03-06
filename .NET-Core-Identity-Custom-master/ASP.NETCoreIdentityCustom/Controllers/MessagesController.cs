using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ASP.NETCoreIdentityCustom.Hubs;
using ASP.NETCoreIdentityCustom.ViewModels;
using System.Text.RegularExpressions;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using System.Text;
using System.Net.Http.Headers;
using ASP.NETCoreIdentityCustom.Service;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        IMessageService service;
     

        public MessagesController(ApplicationDbContext context, IMessageService service,
            IMapper mapper,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
            this.service = service;
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            var messageViewModel = _mapper.Map<Message, MessageViewModel>(message);
            return Ok(messageViewModel);
        }

        [HttpGet("Room/{roomName}")]
        public IActionResult GetMessages(string roomName)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Name == roomName);
            if (room == null)
                return BadRequest();

            var messages = _context.Messages.Where(m => m.ToRoomId == room.Id)
                .Include(m => m.FromUser)
                .Include(m => m.ToRoom)
                .OrderByDescending(m => m.Timestamp)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();

            foreach (var message in messages)
            {
                message.Seen = true;
                _context.Update(message);
                _context.SaveChanges();
            }

            var messagesViewModel = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageViewModel>>(messages);

            return Ok(messagesViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> Create(MessageViewModel viewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var admin = _context.Users.FirstOrDefault(u => u.FullName == "admin");

            var room = _context.Rooms.FirstOrDefault(r => r.Name == viewModel.Room);
            if (room == null)
                return BadRequest();
            string textcontent = Regex.Replace(viewModel.Content, @"<.*?>", string.Empty);
           
            var msg = new Message()
            {
                Content = await service.CheckSpelling1(textcontent),
            FromUser = user,
                ToRoom = room,
                Timestamp = DateTime.Now,
             
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            // Broadcast the message
            var createdMessage = _mapper.Map<Message, MessageViewModel>(msg);
            await _hubContext.Clients.Group(room.Name).SendAsync("newMessage", createdMessage);


            // admin auto reply
            if (Regex.IsMatch(msg.Content, @"\bopen\b.*\bhour(s)?\b|\bhour(s)?\b.*\bopen\b", RegexOptions.IgnoreCase))
            {
                var repmsg = new Message()
                {
                    Content = "Shop mở cửa 8:00AM và đóng cửa lúc 22:00PM",
                    FromUser = admin,
                    ToRoom = room,
                    Timestamp = DateTime.Now
                };
                _context.Messages.Add(repmsg);
                await _context.SaveChangesAsync();
                var createdMessage1 = _mapper.Map<Message, MessageViewModel>(repmsg);
                await _hubContext.Clients.Group(room.Name).SendAsync("newMessage", createdMessage1);

            }  
                
            return CreatedAtAction(nameof(Get), new { id = msg.Id }, createdMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.Messages
                .Include(u => u.FromUser)
                .Where(m => m.Id == id && m.FromUser.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (message == null)
                return NotFound();

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("removeChatMessage", message.Id);

            return Ok();
        }
    }
}
