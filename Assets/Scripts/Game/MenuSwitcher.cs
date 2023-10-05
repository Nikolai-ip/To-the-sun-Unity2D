using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private bool _isGameOnPause;

    public void TooglePauseMenu()
    {
        if (_isGameOnPause)
        {
            TurnOffChildElement(_pauseMenu);
            _isGameOnPause = false;
        }
        else
        {
            TurnOnChildElement(_pauseMenu);
            _isGameOnPause = true;
        }
    }

    private void TurnOnChildElement(GameObject menu)
    {
        foreach (var elem in menu.GetComponentsInChildren<RectTransform>()) elem.gameObject.SetActive(true);
    }

    private void TurnOffChildElement(GameObject menu)
    {
        foreach (var elem in menu.GetComponentsInChildren<RectTransform>()) elem.gameObject.SetActive(false);
    }
}