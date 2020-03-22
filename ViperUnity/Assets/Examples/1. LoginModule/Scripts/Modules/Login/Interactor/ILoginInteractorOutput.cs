using Examples.LoginModule.Model;

namespace UnityViper.Modules
{
    public interface ILoginInteractorOutput
    {
        void LoginSuccess(UserCredencials credencials);
        void LoginFailed(string error);
    }
}
