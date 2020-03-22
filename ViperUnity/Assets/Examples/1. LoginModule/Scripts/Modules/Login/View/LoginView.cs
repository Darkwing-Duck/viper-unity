using Examples.LoginModule.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UnityViper.Modules
{
    public partial class LoginView : MonoBehaviour
    {
        [SerializeField]
        private InputField _loginField;
        
        [SerializeField]
        private Text _loginWarningField;
        
        [SerializeField]
        private InputField _passwordField;
        
        [SerializeField]
        private Text _passwordWarningField;
        
        [SerializeField]
        private Text _statusField;

        [SerializeField]
        private Button _submitButton;

        private bool _isWaitingForLogin;
        
        public ILoginViewOutput Output { get; set; }

        private void InitializeFields(UserCredencials credencials)
        {
            if (credencials == null) return;

            _loginField.text = credencials.Login;
            _passwordField.text = credencials.Password;
        }

        private void CheckSubmitButtonState()
        {
            _submitButton.interactable = !_loginWarningField.gameObject.activeSelf && 
                                         !_passwordWarningField.gameObject.activeSelf &&
                                         !_isWaitingForLogin;
        }

        private void OnSubmitButtonTapped()
        {
            _isWaitingForLogin = true;
            _statusField.color = Color.yellow;
            _statusField.text = "Please Wait...";
            
            CheckSubmitButtonState();
            Output.SubmitButtonClickedWith(_loginField.text, _passwordField.text);
        }
    }

    #region ILoginViewInput Inmplementation

    public partial class LoginView : ILoginViewInput
    {
        public void Initialize(UserCredencials credencials)
        {
            _statusField.text = string.Empty;
            
            InitializeFields(credencials);
            SetLoginWarningActive(true);
            SetPasswordWarningActive(true);
            CheckSubmitButtonState();
            
            _loginField.onValueChanged.AddListener(Output.LoginFieldChanged);
            _passwordField.onValueChanged.AddListener(Output.PasswordFieldChanged);
            _submitButton.onClick.AddListener(OnSubmitButtonTapped);
        }
        
        public void SetLoginWarningActive(bool isActive)
        {
            _loginWarningField.gameObject.SetActive(isActive);
            CheckSubmitButtonState();
        }

        public void SetPasswordWarningActive(bool isActive)
        {
            _passwordWarningField.gameObject.SetActive(isActive);
            CheckSubmitButtonState();
        }

        public void ShowLoginError(string error)
        {
            _statusField.color = Color.red;
            _statusField.text = error;

            _isWaitingForLogin = false;
            CheckSubmitButtonState();
        }

        public void ShowLoginSuccessful()
        {
            _statusField.color = Color.white;
            _statusField.text = "Great! You're logged in!";

            _isWaitingForLogin = false;
            CheckSubmitButtonState();
        }
    }

    #endregion
}
