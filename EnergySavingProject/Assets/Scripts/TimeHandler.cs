using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    float timeInSeconds = 0;
    int gameTimeSeconds = 0;
    int gameTimeMinutes = 0;
    int gameTimeHours = 0;
    int gameTimeDays = 0;

    int pastCheck = 0;

    string clock;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.IsGameOver())
            return;
        timeInSeconds += Time.deltaTime;
        gameTimeSeconds = Mathf.FloorToInt(timeInSeconds * 100);
        gameTimeMinutes = Mathf.FloorToInt(gameTimeSeconds / 60);
        gameTimeHours = Mathf.FloorToInt(gameTimeMinutes / 60);
        gameTimeDays = Mathf.FloorToInt(gameTimeHours / 24);
        clock = string.Format("Day {0}, {1:D2}:{2:D2}", gameTimeDays + 1, gameTimeHours % 24, gameTimeMinutes % 60);
        UIManager.Instance.UpdateClock(clock);

        // Subjects to change every 2 hour (game-tick) (stats, electricity costs)
        if(gameTimeHours % gm.intervalBetweenStatChanges == 0 && gameTimeHours > pastCheck)
        {
            StatComponent.Instance.ChangeAllStats(Random.Range(-3, -6));
            GameManager.Instance.SetCurrentEnergyPrice(gameTimeDays, gameTimeHours % 24);
            UIManager.Instance.UpdatePrice(GameManager.Instance.GetCurrentElectricityPrice());
            pastCheck = gameTimeHours;
        }
        if(gameTimeDays >= gm.daysToWinGame)
        {
            gm.WinGame();
        }
    }

    public float GetTime()
    {
        return timeInSeconds;
    }


    //Singleton
    public static TimeHandler Instance { get; private set; }
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
