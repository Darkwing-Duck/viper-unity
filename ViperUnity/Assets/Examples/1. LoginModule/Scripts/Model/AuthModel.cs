using System;
using UnityEngine;

namespace Examples.LoginModule.Model
{
    [Serializable]
    public class UserCredencials
    {
        [SerializeField]
        private string _login;
        [SerializeField]
        private string _password;

        public UserCredencials(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public string Login => _login;
        public string Password => _password;
    }
    
    public class AuthModel
    {
        public UserCredencials Credencials { get; private set; }

        public void InitializeWith(UserCredencials credencials)
        {
            Credencials = credencials;
        }
    }
}