using UnityEditor;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    private bool _isGameOnPause = false;

    [SerializeField] GameObject _pauseMenu;
    
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
        foreach (var elem in menu.GetComponentsInChildren<RectTransform>())
        {
            elem.gameObject.SetActive(true);
        }
    }

    private void TurnOffChildElement(GameObject menu)
    {
        foreach (var elem in menu.GetComponentsInChildren<RectTransform>())
        {
            elem.gameObject.SetActive(false);
        }
    }
}
