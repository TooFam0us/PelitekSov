using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : MonoBehaviour
{

    public enum CharacterStat
    { 
        Hunger,
        Happiness,
        Education
    }

    Dictionary<CharacterStat, float> CharacterStats = new Dictionary<CharacterStat, float>();

    private void Start()
    {
        // Initialize character stats
        foreach(CharacterStat stat in Enum.GetValues(typeof(CharacterStat)))
        {
            CharacterStats.Add(stat, 100);
        }
    }



    public void ChangeCharacterStat(CharacterStat stat, float change)
    {
        if(CharacterStats.ContainsKey(stat))
        {
            CharacterStats[stat] += change;
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
                Debug.Log(CharacterStats[stat]);
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
