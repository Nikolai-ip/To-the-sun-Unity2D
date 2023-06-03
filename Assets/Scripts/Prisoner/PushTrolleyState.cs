using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PushTrolleyState : BaseStatePrisoner
{
    [SerializeField] private Trolley _trolley;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveDuration;
    public override void Enable()
    {
        base.Enable();
        var closestHandlePos = _trolley.Handles.OrderBy(h => Vector2.Distance(prisonerStateMachine.Tr.position, h.position)).FirstOrDefault();
        var finishPoint = _trolley.MovePoints.OrderBy(point => Vector2.Distance(prisonerStateMachine.Tr.position, point.position)).Last();
        StartCoroutine(MoveObjectToPosition(_trolley.Tr, finishPoint.position, _moveDuration));
        StartCoroutine(MoveObjectToPosition(prisonerStateMachine.Tr, new Vector2(finishPoint.position.x + closestHandlePos.localPosition.x,0), _moveDuration));

    }
    private void Update()
    {
        if (isFinishMove)
        {
            prisonerStateMachine.StartNextAction();
        }
    }
}
