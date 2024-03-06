using ASP.NETCoreIdentityCustom.ViewModels;
using System.Security.Claims;

namespace ASP.NETCoreIdentityCustom.Service
{
	public interface IBlogService
	{
		Task<List<BlogViewModel>> FindAll();
		Task<BlogViewModel> FindById(int id);
		Task<List<BlogViewModel>> ListRecenlyBlog();
		Task<int> Create(BlogViewModel b, IFormFile formFile);
	}
}
