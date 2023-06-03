using TMPro;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    private RectTransform[] _childElement;
    private TextMeshProUGUI _inscription;

    [SerializeField] private UINotifier _notifier;

    private void Start()
    {
        _childElement = GetComponentsInChildren<RectTransform>();
        _inscription = GetComponentInChildren<TextMeshProUGUI>();

        _notifier.EntityCanChanged += SetTextVisibility;
        _notifier.StateChanged += ChangeInscription;

        TurnOffChildElement();
    }

    private void SetTextVisibility(string UIText)
    {
        if (UIText == string.Empty)
        {
            TurnOffChildElement();
        }
        else
        {
            TurnOnChildElement();
            ChangeInscription(UIText);
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
    
    private void ChangeInscription(string UIText)
    {
        _inscription.text = UIText;
    }
}
