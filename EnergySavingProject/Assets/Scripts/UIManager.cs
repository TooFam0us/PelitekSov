using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI clock;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI budget;
    public TextMeshProUGUI energyConsumed;
    public GameObject endScreen;
    public GameObject[] statSliders;

    public void UpdateClock(string msg)
    {
        clock.text = msg;
    }

    public void GameEndedUI()
    {
        int score = Mathf.FloorToInt(TimeHandler.Instance.GetTime() / 10000) * Mathf.FloorToInt(StatComponent.Instance.GetStatParams() / 50) * Mathf.FloorToInt(StatComponent.Instance.GetBudget() * 10);
        endScreen.SetActive(true);
    }

    public void UpdateStats(int i, float amount)
    {
        statSliders[i].GetComponent<Slider>().value = amount / 100;
    }

    public void UpdatePrice(float price)
    {
        energy.text = price.ToString("F") + "€/kWh";
    }

    public void UpdateBudget(float budg)
    {
        budget.text = budg.ToString("F") + " € left";
    }

    public void UpdateEnergyConsumed(float consumed)
    {
        energyConsumed.text = consumed.ToString("F") + " kW consumed";
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void HighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Singleton
    public static UIManager Instance { get; private set; }
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
