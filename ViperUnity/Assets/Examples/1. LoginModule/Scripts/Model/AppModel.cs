namespace Examples.LoginModule.Model
{
    public class AppModel
    {
        private static readonly AppModel _instance = new AppModel();

        public readonly AuthModel Auth;

        private AppModel()
        {
            Auth = new AuthModel();
        }
        
        public static AppModel Instance => _instance;
    }
}