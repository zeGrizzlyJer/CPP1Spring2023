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
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<AudioSourceManager>().PlayOneShot(pickupSound, false);
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
                LifeUp();
                return;
            }
            ScoreUp();
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

    void LifeUp()
    {
        GameManager.instance.lives++;
        Destroy(gameObject);
    }

    void ScoreUp()
    {
        GameManager.instance.score++;
        Destroy(gameObject);
    }
}
