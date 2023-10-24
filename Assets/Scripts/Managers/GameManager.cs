using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{ 
    //Instance Manager
    static GameManager _instance = null;
    public static GameManager instance
    {
        get => _instance;
        set
        {
            _instance = value;
        }
    }

    //Life Values
    private int _lives = 2;
    public int maxLives = 3;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0) GameOver();
            if (_lives > value) Respawn();

            _lives = value;

            if (_lives > maxLives) _lives = maxLives;

            Debug.Log("Lives value has changed to " + _lives.ToString());

            //Invoke an event here to listen to life value changes
            OnLifeValueChanged?.Invoke(_lives);
        }
    }

    //Score Values
    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;

            Debug.Log("Score value has changed to " + _score.ToString());
        }
    }

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Transform spawnPoint;
    public UnityEvent<int> OnLifeValueChanged;

    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        //ChangeScene();
    }

    private void ChangeScene()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().name)
            {
            case "Level": case "GameOver":
                SceneManager.LoadScene("Title");
                break;
            case "Title":
                SceneManager.LoadScene("Level");
                break;
            default:
                break;
            }
        }
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        UpdateSpawnPoint(spawnLocation);
    }

    public void UpdateSpawnPoint(Transform updatedPoint)
    {
        spawnPoint = updatedPoint;
    }

    private void Respawn()
    {
        playerInstance.transform.position = spawnPoint.position;
        playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(playerInstance.respawnSound, false);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");

    }
}
