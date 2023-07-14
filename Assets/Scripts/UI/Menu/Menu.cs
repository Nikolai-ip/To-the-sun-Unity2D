using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool _isPause = false;
    private float _previousTimeScale;

    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    public bool IsPause => _isPause;

    private void Start()
    {
        this.gameObject.SetActive(_isPause);
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

        _isPause = !_isPause;

        if (Time.timeScale > 0)
        {
            _previousTimeScale = Time.timeScale;
        }

        Time.timeScale = _isPause ? 0f : _previousTimeScale;

        this.gameObject.SetActive(_isPause);
        _pauseMenu.gameObject.SetActive(_isPause);
    }
}
