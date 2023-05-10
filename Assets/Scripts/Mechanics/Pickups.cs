using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Jumpup,
        Sizeup,
        Life,
        Score
    }

    public PickupType currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController myController = collision.gameObject.GetComponent<PlayerController>();
            if (!myController) return;

            if (currentPickup == PickupType.Jumpup)
            {
                JumpUp(myController);
                return;
            }
            if (currentPickup == PickupType.Sizeup)
            {
                SizeUp(myController);
                return;
            }
            if (currentPickup == PickupType.Life)
            {
                LifeUp(myController);
                return;
            }
            ScoreUp(myController);
        }
    }

    void JumpUp(PlayerController player)
    {
        player.StartJumpForceChange();
        Destroy(gameObject);
    }

    void SizeUp(PlayerController player)
    {
        player.StartSizeChange();
        Destroy(gameObject);
    }

    void LifeUp(PlayerController player)
    {
        player.lives++;
        Destroy(gameObject);
    }

    void ScoreUp(PlayerController player)
    {
        player.score++;
        Destroy(gameObject);
    }
}
