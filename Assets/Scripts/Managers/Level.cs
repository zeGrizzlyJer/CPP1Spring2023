using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int StartingLives = 2;
    public Transform spawnPoint;

    private void Start()
    {
        GameManager.instance.SpawnPlayer(spawnPoint);
        GameManager.instance.lives = StartingLives;
    }
}
