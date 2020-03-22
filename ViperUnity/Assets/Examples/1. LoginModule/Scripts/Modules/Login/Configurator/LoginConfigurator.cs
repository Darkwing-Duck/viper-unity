namespace UnityViper.Modules
{
    public static class LoginConfigurator
    {
        public static ILoginInput ConfigureWith(LoginView view)
        {
            LoginInteractor interactor = new LoginInteractor();
            LoginRouter router = new LoginRouter();
            LoginPresenter presenter = new LoginPresenter(view, interactor, router);

            view.Output = presenter;
            interactor.Output = presenter;
            router.Output = presenter;
            presenter.Initialize();

            return presenter;
        }
    }
}
