using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    [SerializeField] private bool isActive;

    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int expReward;
    [SerializeField] private int goldReward;
    [SerializeField] private QuestGoal questGoal;


    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set 
        {
            isActive = value;
        }
    }
    public string Title
    {
        get
        {
            return title;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
    }
    
    public int ExpReward
    {
        get 
        {
            return expReward;
        }
    }
    public int GoldReward
    {
        get
        {
            return goldReward;
        }
    }
    public QuestGoal QuestGoal
    {
        get
        {
            return questGoal;
        }
        set
        {
            questGoal = value;
        }
    }

    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " was Completed");
    }
}
