using Examples.LoginModule.Model;

namespace UnityViper.Modules
{
    public partial class LoginPresenter : ILoginInput
    {
        public readonly ILoginViewInput View;
        public readonly ILoginInteractorInput Interactor;
        public readonly ILoginRouterInput Router;

        public LoginPresenter(ILoginViewInput view, ILoginInteractorInput interactor, ILoginRouterInput router)
        {
            View = view;
            Interactor = interactor;
            Router = router;
        }

        public void Initialize()
        {
            View.Initialize(Interactor.GetCachedCredencials());
        }
    }

    #region ILoginViewOutput Implementation
    public partial class LoginPresenter : ILoginViewOutput
    {
        public void LoginFieldChanged(string value)
        {
            View.SetLoginWarningActive(!IsLoginValid(value));
        }

        public void PasswordFieldChanged(string value)
        {
            View.SetPasswordWarningActive(!IsPasswordValid(value));
        }

        public void SubmitButtonClickedWith(string login, string password)
        {
            UserCredencials credencials = new UserCredencials(login, password);
            Interactor.SendLoginRequest(credencials);
        }

        private bool IsLoginValid(string value) => value.Length >= 3;
        private bool IsPasswordValid(string value) => value.Length >= 4;
    }
    #endregion

    #region ILoginInteractorOutput Implementation
    public partial class LoginPresenter : ILoginInteractorOutput
    {
        public void LoginSuccess(UserCredencials credencials) => View.ShowLoginSuccessful();
        public void LoginFailed(string error) => View.ShowLoginError(error);
    }
    #endregion

    #region ILoginRouterOutput Implementation
    public partial class LoginPresenter : ILoginRouterOutput
    {
    }
    #endregion
}
