using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveToCoal : BaseStatePrisoner
{
    [SerializeField] private Transform[] _CoalsTr;
    [SerializeField] private float _moveDuration;
    public override void Enable()
    {
        base.Enable();
        var closestCoal = _CoalsTr.OrderBy(coal=>Vector2.Distance(coal.position, prisonerStateMachine.Tr.position)).FirstOrDefault();
        StartCoroutine(MoveObjectToPosition(prisonerStateMachine.Tr, closestCoal.position, _moveDuration));

    }
    private void Update()
    {
        if (isFinishMove)
        {
            prisonerStateMachine.StartNextAction();
        }
    }
}
