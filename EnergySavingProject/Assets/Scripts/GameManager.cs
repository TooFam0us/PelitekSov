using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int daysToWinGame;
    public int intervalBetweenStatChanges;
    public float statMinThreshold = 0;

    bool gameEnded;

    UIManager um;

    private void Start()
    {
        um = UIManager.Instance;
    }

    public void WinGame()
    {
        Debug.Log("Win");
        GameEnded();
    }

    public void LoseGame()
    {
        Debug.Log("Lost");
        GameEnded();
    }

    public void GameEnded()
    {
        gameEnded = true;
        um.endScreen.SetActive(true);
    }

    public bool IsGameOver()
    {
        return gameEnded;
    }

    //Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
