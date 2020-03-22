using System.Threading.Tasks;
using Examples.LoginModule.Model;
using UnityEngine;

namespace UnityViper.Modules
{
    public class LoginInteractor : ILoginInteractorInput
    {
        private const string CREDENCIALS_KEY = "credencials";
        public ILoginInteractorOutput Output { get; set; }

        public UserCredencials GetCachedCredencials()
        {
            return LoadCredencials();
        }

        // Mock of login request
        public async void SendLoginRequest(UserCredencials credencials)
        {
            await Task.Delay(2000);

            if (credencials.Login != "admin")
            {
                Output.LoginFailed("Login is incorrect!");
                return;
            }

            if (credencials.Password != "12345")
            {
                Output.LoginFailed("Password is incorrect!");
                return;
            }
            
            SaveCredencials(credencials);
            Output.LoginSuccess(credencials);
        }

        #region Interact With Persistent Storage

        private UserCredencials LoadCredencials()
        {
            string json = PlayerPrefs.GetString(CREDENCIALS_KEY);
            if (string.IsNullOrEmpty(json)) return null;
            
            UserCredencials result = JsonUtility.FromJson<UserCredencials>(json);
            return result;
        }
        
        private void SaveCredencials(UserCredencials credencials)
        {
            string json = JsonUtility.ToJson(credencials);
            PlayerPrefs.SetString(CREDENCIALS_KEY, json);
        }

        #endregion
    }
}
