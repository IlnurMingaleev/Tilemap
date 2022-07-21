using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    [SerializeField] private int minExp;
    private int currentExp;
    private int maxExp;
    private int expToNextLevel;
    private int level;

    private int[] levelExp = new int[] { 50, 100, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 5000 };

    private UIExperience uiExp;
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        maxExp = levelExp[level];
        currentExp = minExp;
        expToNextLevel = maxExp - currentExp;
        uiExp = GetComponent<UIExperience>();
        uiExp.SetExperienceToUI(this);
        
    }
    public int GetCurrentExp()
    {
        return currentExp;
    }

    public int GetMaxExp()
    {
        return maxExp;
    }
    public int GetExpToNextLevel()
    {
        return expToNextLevel;
    }
    public int GetLevel()
    {
        return level;
    }
    public int GetMinExp()
    {
        return minExp;
    }


    public void AddExperience(int expAmount) 
    {
        if (expToNextLevel > expAmount)
        {
            currentExp += expAmount;
            expToNextLevel -= expAmount;
        }
        else
        {
            level++;
            minExp = maxExp;
            currentExp = maxExp;
            maxExp = levelExp[level];
            expAmount -= expToNextLevel;
            expToNextLevel = maxExp - minExp;

            if (expAmount > 0)
            {
                AddExperience(expAmount);
            }
        }
        uiExp.SetExperienceToUI(this);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
