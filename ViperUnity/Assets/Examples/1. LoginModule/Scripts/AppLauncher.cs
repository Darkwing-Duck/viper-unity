using UnityEngine;
using UnityViper.Modules;

public class AppLauncher : MonoBehaviour
{
    [SerializeField]
    private LoginView _loginViewPrefab;

    private void Start()
    {
        LoginView loginView = Instantiate(_loginViewPrefab, transform);
        ILoginInput loginModule = LoginConfigurator.ConfigureWith(loginView);
    }
}
