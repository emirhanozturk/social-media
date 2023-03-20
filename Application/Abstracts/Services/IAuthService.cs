using Application.Abstracts.Services.Auth;

namespace Application.Abstracts.Services
{
    public interface IAuthService: IExternalAuth,IInternalAuth
    {
        
    }
}
