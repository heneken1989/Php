using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ASP.NETCoreIdentityCustom.Hubs;
using Microsoft.AspNetCore.SignalR;
using ASP.NETCoreIdentityCustom.ViewModels;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using System.Text.RegularExpressions;

namespace ASP.NETCoreIdentityCustom.Controllers
{
 /*   [Authorize]*/
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public RoomsController(ApplicationDbContext context,
            IMapper mapper,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<RoomViewModel>>> List()
        {
            List<Room> rooms;
            rooms = await _context.Rooms
              .Include(r => r.Admin)
              .ToListAsync();

            var roomsViewModel = _mapper.Map<IEnumerable<Room>, IEnumerable<RoomViewModel>>(rooms);
            return Ok(roomsViewModel);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomViewModel>>> Get()
        {
            List<Room> rooms;


            if (User.Identity.Name == "admin@gmail.com")
            {
                rooms = await _context.Rooms
                  .Include(r => r.Admin)
                  .ToListAsync();
            }
            else
            {
                rooms = await _context.Rooms
                    .Include(r => r.Admin)
                    .Where(a => a.Admin.UserName == User.Identity.Name)
                    .ToListAsync();
            }
            var roomsViewModel = _mapper.Map<IEnumerable<Room>, IEnumerable<RoomViewModel>>(rooms);
            return Ok(roomsViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            var roomViewModel = _mapper.Map<Room, RoomViewModel>(room);
            return Ok(roomViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Create(RoomViewModel viewModel)
        {
            if (_context.Rooms.Any(r => r.Name == viewModel.Name))
                return BadRequest("Invalid room name or room already exists");


            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (_context.Rooms.Any(r => r.Name == User.Identity!.Name))
            {
                return null;

            }
            else
            {
                var room = new Room()
                {
                    Name = User.Identity.Name,
                    Admin = user
                };
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                var createdRoom = _mapper.Map<Room, RoomViewModel>(room);
                await _hubContext.Clients.All.SendAsync("addChatRoom", createdRoom);


                var accAdmin = _context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
                var msg = new Message()
                {
                    Content = "Welcome To Support Area, Please Send Us If You Have Any Question! Have A Nice Day!!",
                    FromUser = accAdmin,
                    ToRoom = room,
                    Timestamp = DateTime.Now
                };

                _context.Messages.Add(msg);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(Get), new { id = room.Id }, createdRoom);

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, RoomViewModel viewModel)
        {
            if (_context.Rooms.Any(r => r.Name == viewModel.Name))
                return BadRequest("Invalid room name or room already exists");

            var room = await _context.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            room.Name = viewModel.Name;
            await _context.SaveChangesAsync();

            var updatedRoom = _mapper.Map<Room, RoomViewModel>(room);
            await _hubContext.Clients.All.SendAsync("updateChatRoom", updatedRoom);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("removeChatRoom", room.Id);
            await _hubContext.Clients.Group(room.Name).SendAsync("onRoomDeleted");

            return Ok();
        }
    }
}
