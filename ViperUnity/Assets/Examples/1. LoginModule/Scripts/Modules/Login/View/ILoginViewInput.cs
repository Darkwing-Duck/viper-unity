using Examples.LoginModule.Model;

namespace UnityViper.Modules
{
    public interface ILoginViewInput
    {
        void Initialize(UserCredencials credencials);

        void SetLoginWarningActive(bool isActive);
        void SetPasswordWarningActive(bool isActive);
        
        void ShowLoginError(string error);
        void ShowLoginSuccessful();
    }
}
