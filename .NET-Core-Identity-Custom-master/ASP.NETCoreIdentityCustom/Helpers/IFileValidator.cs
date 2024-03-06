using Microsoft.AspNetCore.Http;

namespace ASP.NETCoreIdentityCustom.Helpers
{
    public interface IFileValidator
    {
        bool IsValid(IFormFile file);
    }
}
