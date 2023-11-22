using System.Collections.Generic;
using UnityEngine;

public class PrisonerStateMachine : MonoBehaviour
{
    [SerializeField] private List<BaseStatePrisoner> _orderOfStates = new();
    private int _currentNumberOfAnimationPlayed;
    private int _stateIndex;
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Transform Tr { get; private set; }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Tr = GetComponent<Transform>();
        foreach (var state in _orderOfStates) state.Disable();
        _orderOfStates[0].Enable();
    }

    public void StartNextAction()
    {
        _stateIndex++;
        _orderOfStates[_stateIndex - 1].Disable();
        if (_stateIndex == _orderOfStates.Count) _stateIndex = 0;
        _orderOfStates[_stateIndex].Enable();
    }

    public void IncrementNumberOfAnimation()
    {
        _currentNumberOfAnimationPlayed++;
        if (_currentNumberOfAnimationPlayed == _orderOfStates[_stateIndex].NumberOfAnimationPlayed)
        {
            _currentNumberOfAnimationPlayed = 0;
            StartNextAction();
        }
    }
}