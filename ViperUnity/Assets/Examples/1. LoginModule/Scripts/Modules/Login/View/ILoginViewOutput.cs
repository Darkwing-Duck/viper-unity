namespace UnityViper.Modules
{
    public interface ILoginViewOutput
    {
        void LoginFieldChanged(string value);
        void PasswordFieldChanged(string value);
        void SubmitButtonClickedWith(string login, string password);
    }
}
