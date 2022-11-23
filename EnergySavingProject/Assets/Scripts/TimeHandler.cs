using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    float timeInSeconds;
    int gameTimeSeconds;
    int gameTimeMinutes;
    int gameTimeHours;
    int gameTimeDays;

    string clock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeInSeconds += Time.deltaTime;
        gameTimeSeconds = Mathf.FloorToInt(timeInSeconds * 100);
        gameTimeMinutes = Mathf.FloorToInt(gameTimeSeconds / 60);
        gameTimeHours = Mathf.FloorToInt(gameTimeMinutes / 60);
        gameTimeDays = Mathf.FloorToInt(gameTimeHours / 24);
        clock = string.Format("Day {0}, {1:D2}:{2:D2}", (gameTimeDays % 24) + 1, gameTimeHours % 60, gameTimeMinutes % 60);
        UIManager.Instance.UpdateClock(clock);
    }
}
