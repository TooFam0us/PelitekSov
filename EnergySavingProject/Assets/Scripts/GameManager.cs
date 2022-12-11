using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int daysToWinGame;
    public int intervalBetweenStatChanges;
    public float statMinThreshold = 0;

    public Dictionary<int, float>[] energyPriceTable = new Dictionary<int, float>[7];

    float currentEnergyPrice = 0;
    

    bool gameEnded;

    UIManager um;

    private void Start()
    {
        um = UIManager.Instance;
        for (int i = 0; i < energyPriceTable.Length; i++)
        {
            energyPriceTable[i] = new();
        }
        StartCoroutine(GetComponent<ReadCSV>().ReadCSVFile());
        currentEnergyPrice = energyPriceTable[0].GetValueOrDefault(0);
        UIManager.Instance.UpdatePrice(GetCurrentElectricityPrice());
    }

    public void SetCurrentEnergyPrice(int index, int time)
    {
        currentEnergyPrice = energyPriceTable[index].GetValueOrDefault(time);
    }

    public float GetCurrentElectricityPrice()
    {
        return currentEnergyPrice;
    }

    public void WinGame()
    {
        Debug.Log("Win");
        GameEnded();
    }

    public void AddDataToPriceTable(int index, int time, float amount)
    {
        energyPriceTable[index].Add(time, amount);
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
