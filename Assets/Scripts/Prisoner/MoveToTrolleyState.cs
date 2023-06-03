using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveToTrolleyState : BaseStatePrisoner
{
    [SerializeField] private Trolley _trolley;
    [SerializeField] private float _moveSpeed;

    public override void Enable()
    {
        base.Enable();
        var closestHandlePos = _trolley.Handles.OrderBy(h => Vector2.Distance(prisonerStateMachine.Tr.position, h.position)).FirstOrDefault();
        prisonerStateMachine.Tr.localScale = new Vector3(Mathf.Sign((closestHandlePos.position - prisonerStateMachine.Tr.transform.position).x), 1);
        StartCoroutine(MoveTo(prisonerStateMachine.Tr, closestHandlePos.position, _moveSpeed));
    }
    private void Update()
    {
        if (isFinishMove)
        {
            prisonerStateMachine.StartNextAction();
        }
    }

}
