using Examples.LoginModule.Model;

namespace UnityViper.Modules
{
    public interface ILoginInteractorInput
    {
        UserCredencials GetCachedCredencials();
        void SendLoginRequest(UserCredencials credencials);
    }
}
