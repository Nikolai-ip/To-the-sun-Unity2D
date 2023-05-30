using System.Collections;
using System.Collections.Generic;
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
        InteractionEnviromentController _playerinteractionController = FindObjectOfType<InteractionEnviromentController>();
        _playerinteractionController.OnCanLampChanged += ShowOrHideUI;
        _playerinteractionController.OnLampStateChanged+= ChangeInsctiption;
        TurnOffChildElement();
    }
    private void ShowOrHideUI(Lamp lamp)
    {
        if (lamp != null)
        {
            TurnOnChildElement();
            ChangeInsctiption(lamp.IsActive);
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
    private void ChangeInsctiption(bool isActive)
    {
        if (isActive)
        {
            _insctiption.text = "Turn off lamp";
        }
        else
        {
            _insctiption.text = "Turn on lamp";
        }
    }
}
