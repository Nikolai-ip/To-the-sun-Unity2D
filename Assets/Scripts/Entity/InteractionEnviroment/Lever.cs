using UnityEngine;

public class Lever : ActivableEntity
{
    protected override void TurnOff()
    {
        Debug.Log("Lever is off");
        IsActive = false;
        OnStateChanged(IsActive);
    }

    protected override void TurnOn()
    {
        Debug.Log("Lever is on");
        IsActive = true;
        OnStateChanged(IsActive);
    }
}