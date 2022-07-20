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
    }


    public void Add(int expAmount) 
    {
        if (expAmount <= 0) 
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
                maxExp = levelExp[level];
                expAmount -= expToNextLevel;
                expToNextLevel = maxExp - minExp;
                Add(expAmount);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
