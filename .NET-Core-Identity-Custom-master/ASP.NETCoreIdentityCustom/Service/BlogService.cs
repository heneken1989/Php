using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.Shared;
using ASP.NETCoreIdentityCustom.ViewModels;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
using System.Security.Claims;

namespace ASP.NETCoreIdentityCustom.Service
{
	public class BlogService : IBlogService
	{
		ApplicationDbContext ctx;
		IMapper mapper;
		ICommonMethod CommonMethod;

		public BlogService(ApplicationDbContext ctx, IMapper mapper, ICommonMethod CommonMethod)
		{
			this.ctx = ctx;
			this.mapper = mapper;
			this.CommonMethod = CommonMethod;
		}

		public async Task<List<BlogViewModel>> FindAll()
		{
			var blogs = await ctx.Blogs
				.Include(a=>a.User)
				.ToListAsync();

			return mapper.Map<List<BlogViewModel>>(blogs);
		}

		public async Task<BlogViewModel> FindById(int id)
		{
			var blogs = await ctx.Blogs
				.Include(a => a.User)
				.SingleOrDefaultAsync(a=>a.Id == id);	

			return mapper.Map<BlogViewModel>(blogs);
		}


		public async Task<List<BlogViewModel>> ListRecenlyBlog()
		{
			var blogs = await ctx.Blogs
			.Include(a => a.User)
			.OrderByDescending(a=>a.CreatedAt)
			.Take(4)
			.ToListAsync();

			return mapper.Map<List<BlogViewModel>>(blogs);
		} 

		public async Task<int> Create(BlogViewModel b,IFormFile formFile)
		{
			try
			{
                b.CreatedAt = DateTime.Now;
                var PhotoUrl = await CommonMethod.UploadImage(formFile);
				Blog blog = mapper.Map<Blog>(b);
				blog.ImageUrl = PhotoUrl;
                ctx.Blogs.Add(blog);
				await ctx.SaveChangesAsync();
				return blog.Id;
			}catch (Exception ex) 
			{
				return 0;
			}	
		}


	}
}
