using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : MonoBehaviour
{

    GameManager gm;
    UIManager um;

    public enum CharacterStat
    { 
        Hunger,
        Happiness,
        Education
    }

    Dictionary<CharacterStat, float> CharacterStats = new();

    private void Start()
    {
        gm = GameManager.Instance;
        um = UIManager.Instance;
        // Initialize character stats
        foreach(CharacterStat stat in Enum.GetValues(typeof(CharacterStat)))
        {
            if(!CharacterStats.ContainsKey(stat))
            {
                CharacterStats.Add(stat, 100);
                um.UpdateStats((int)stat ,CharacterStats[stat]);
            }
        }
    }



    public void ChangeCharacterStat(CharacterStat stat, float change)
    {
        if(CharacterStats.ContainsKey(stat))
        {
            CharacterStats[stat] += change;
            um.UpdateStats((int)stat, CharacterStats[stat]);
            if (CharacterStats[stat] < gm.statMinThreshold)
            {
                gm.LoseGame();
            }
        }
        else
        {
            Debug.LogError("No character stat found");
        }
    }

    public void ChangeAllStats(float change)
    {
        foreach (CharacterStat stat in Enum.GetValues(typeof(CharacterStat)))
        {
            if(CharacterStats.ContainsKey(stat))
            {
                CharacterStats[stat] += change;
                um.UpdateStats((int)stat, CharacterStats[stat]);
                if(CharacterStats[stat] < gm.statMinThreshold)
                {
                    gm.LoseGame();
                }
            }
        }
    }


    //Singleton
    public static StatComponent Instance { get; private set; }
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
