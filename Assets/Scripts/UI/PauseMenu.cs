using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private bool _isPause = false;
    private float _previousTimeScale;

    public bool IsPause => _isPause;

    private void Start()
    {
        _menu.SetActive(_isPause);
    }

    public void SwitchPauseMenu()
    {
        _isPause = !_isPause;

        if (Time.timeScale > 0)
        {
            _previousTimeScale = Time.timeScale;
        }

        Time.timeScale = _isPause ? 0f : _previousTimeScale;

        _menu.SetActive(_isPause);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
