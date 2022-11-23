using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI clock;
    public GameObject endScreen;
    public GameObject[] statSliders;

    public void UpdateClock(string msg)
    {
        clock.text = msg;
    }

    public void UpdateStats(int i, float amount)
    {
        statSliders[i].GetComponent<Slider>().value = amount / 100;
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
