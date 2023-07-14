public class LeverDoor : Door
{
    public override string UITextInteraction => "Door is close. Find the lever";

    public override void Trigger(bool state)
    {
        if (state)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
