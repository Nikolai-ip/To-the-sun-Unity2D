using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;

public class LightOfLamp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HideController player))
        {
            player.NumberOfLampsAbove++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HideController player))
        {
            player.NumberOfLampsAbove--;
        }
    }
}
