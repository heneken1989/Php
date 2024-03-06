namespace ASP.NETCoreIdentityCustom.Shared
{
	public interface ICommonMethod
	{
		Task<string> UploadImage(IFormFile formFile);
	}
}
