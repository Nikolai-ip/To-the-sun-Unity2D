using System.Collections.Generic;
using DefaultNamespace.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    private RectTransform[] _childElement;
    private TextMeshProUGUI _inscription;

     private UINotifier[] _notifiers;

    private void Start()
    {
        _childElement = GetComponentsInChildren<RectTransform>();
        _inscription = GetComponentInChildren<TextMeshProUGUI>();

        FindUINotifiers();
        TurnOffChildElement();
    }
    private void FindUINotifiers()
    {
        _notifiers = FindObjectsOfType<UINotifier>();
        foreach (var uiNotifier in FindObjectsOfType<UINotifier>())
        {
            uiNotifier.EntityCanChanged += SetTextVisibility;
            uiNotifier.StateChanged += ChangeInscription;
        }
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