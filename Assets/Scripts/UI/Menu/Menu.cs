using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private SettingsMenu _settingsMenu;
    private float _previousTimeScale;

    public bool IsPause { get; private set; }

    private void Start()
    {
        gameObject.SetActive(IsPause);
    }

    public void SwitchSettingsPauseMenu()
    {
        _settingsMenu.gameObject.SetActive(!_settingsMenu.gameObject.activeInHierarchy);
        _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeInHierarchy);
    }

    public void SwitchMenus()
    {
        if (_settingsMenu.gameObject.activeInHierarchy)
        {
            SwitchSettingsPauseMenu();
            return;
        }

        IsPause = !IsPause;

        if (Time.timeScale > 0) _previousTimeScale = Time.timeScale;

        Time.timeScale = IsPause ? 0f : _previousTimeScale;

        gameObject.SetActive(IsPause);
        _pauseMenu.gameObject.SetActive(IsPause);
    }
}