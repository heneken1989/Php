using static ASP.NETCoreIdentityCustom.Service.MessageService;

namespace ASP.NETCoreIdentityCustom.Service
{
    public interface  IMessageService
    {
        Task<string> CheckSpelling(string inputText);
        Task<string> CheckSpelling1(string inputText);
    }
   
}
