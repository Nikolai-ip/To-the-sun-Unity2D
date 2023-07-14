using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOfLamp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HideController player))
        {
            player.InShadow = false;
            player.NumberOfLampsAbove++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HideController player))
        {
            if (player.NumberOfLampsAbove == 1)
                player.InShadow = true;
            player.NumberOfLampsAbove--;
        }
    }
}
