using TMPro;
using UnityEngine;

public class UILampInteraction : MonoBehaviour
{
    private RectTransform[] _childElement;
    private TextMeshProUGUI _insctiption;

    private void Start()
    {
        _childElement = GetComponentsInChildren<RectTransform>();
        _insctiption = GetComponentInChildren<TextMeshProUGUI>();

        var _playerinteractionController = FindObjectOfType<InteractionEnviromentController>();
        _playerinteractionController.OnCanLampChanged += ShowOrHideUI;
        _playerinteractionController.OnLampStateChanged += ChangeInsctiption;

        TurnOffChildElement();
    }

    private void ShowOrHideUI(InteractiveEntity lamp)
    {
        if (lamp != null)
        {
            TurnOnChildElement();
            var UIText = (lamp as ActivableEntity).UIText;
            ChangeInsctiption(UIText);
        }
        else
        {
            TurnOffChildElement();
        }
    }
    private void TurnOnChildElement()
    {
        foreach (var elem in _childElement)
        {
            elem.gameObject.SetActive(true);
        }
    }
    private void TurnOffChildElement()
    {
        foreach (var elem in _childElement)
        {
            elem.gameObject.SetActive(false);
        }
    }
    private void ChangeInsctiption(string text)
    {
            _insctiption.text = text;
    }
}
