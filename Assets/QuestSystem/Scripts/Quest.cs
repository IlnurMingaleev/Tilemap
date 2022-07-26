using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    [SerializeField] private bool isActive;         // Is Quest Active

    [SerializeField] private string title;          // Title of a quest
    [SerializeField] private string description;    // Description of a quest
    [SerializeField] private int expReward;         // Experience reward for the quest
    [SerializeField] private int goldReward;        // Gold Reward for the quest
    [SerializeField] private QuestGoal questGoal;   // Field to define goals to achieve for the quest

    // Properties for the given fiaelds to have an access to private fiaelds from another classes
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

    // Operations on Quest Complete. Here We can add some UI to clearly see that quest is completed. Now this role is played only by Debug.Log statement.
    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " was Completed");
    }
}
