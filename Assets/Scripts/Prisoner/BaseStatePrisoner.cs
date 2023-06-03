using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatePrisoner : MonoBehaviour
{
    [SerializeField] protected PrisonerStateMachine prisonerStateMachine;
    [SerializeField] protected string animationName;
    [SerializeField] protected int numberOfAnimationPlayed;

    protected bool isFinishMove = false;
    public int NumberOfAnimationPlayed => numberOfAnimationPlayed;
    private void Start()
    {
        
    }
    public virtual void Enable()
    {
        enabled = true;
        prisonerStateMachine.Animator.SetBool(animationName, true);
        
    }
    public virtual void Disable()
    {
        isFinishMove = false;
        prisonerStateMachine.Animator.SetBool(animationName, false);
        enabled = false;
    }
    protected IEnumerator MoveObjectToPosition(Transform objectToMove, Vector3 targetPosition, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startingPosition = objectToMove.position;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            objectToMove.position = new Vector2( Vector3.Lerp(startingPosition, targetPosition, t).x, objectToMove.position.y);
            yield return null;
        }
        isFinishMove = true;
        objectToMove.position = targetPosition;
    }
    protected IEnumerator MoveTo(Transform @object, Vector3 targetPosition, float speed)
    {
        var rb = @object.GetComponent<Rigidbody2D>();
        while (Mathf.Abs(@object.transform.position.x - targetPosition.x) > 0.01f)
        {
            rb.velocity = (targetPosition - @object.transform.position).normalized * speed;
            yield return null;
        }
        isFinishMove = true;
    }
}
