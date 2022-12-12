using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public TextMeshProUGUI clock;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI budget;
    public TextMeshProUGUI energyConsumed;
    public TextMeshProUGUI scoreText;
    public GameObject endScreen;
    public GameObject[] statSliders;

    public void UpdateClock(string msg)
    {
        clock.text = msg;
    }

    public void GameEndedUI(bool won)
    {
        int score = Mathf.FloorToInt(TimeHandler.Instance.GetTime() / 100) > 1 ? Mathf.FloorToInt(TimeHandler.Instance.GetTime() / 100) : 1 * 
            Mathf.FloorToInt(StatComponent.Instance.GetStatParams() / 20) > 1 ? Mathf.FloorToInt(StatComponent.Instance.GetStatParams() / 50) : 1 * 
            Mathf.FloorToInt(StatComponent.Instance.GetBudget() * 10) > 1 ? Mathf.FloorToInt(StatComponent.Instance.GetBudget() * 10) : 1;
        endText.text = won ? "You won the game, congratz" : "You lost the game unfortunately, try again";
        scoreText.text = "Your score: " + score.ToString() + " points";
        endScreen.SetActive(true);
    }

    public void UpdateStats(int i, float amount)
    {
        statSliders[i].GetComponent<Slider>().value = amount / 100;
    }

    public void UpdatePrice(float price)
    {
        energy.text = Convert.ToDecimal(price.ToString("F"), new CultureInfo("en-US")) + "€/kWh";
    }

    public void UpdateBudget(float budg)
    {
        budget.text = Convert.ToDecimal(budg.ToString("F"), new CultureInfo("en-US")) + " € left";
    }

    public void UpdateEnergyConsumed(float consumed)
    {
        energyConsumed.text = Convert.ToDecimal(consumed.ToString("F"), new CultureInfo("en-US")) + " kW consumed";
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
