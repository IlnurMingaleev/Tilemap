using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    [SerializeField] private GoalType goalType;
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;

    public GoalType GoalType 
    {
        get 
        {
            return goalType;
        }
        set 
        {
            goalType = value;
        }
    }
    public int RequiredAmount
    {
        get
        {
            return requiredAmount;
        }
        set
        {
            requiredAmount = value;
        }
    }
    public int CurrentAmount
    {
        get
        {
            return currentAmount;
        }
        set
        {
            currentAmount = value;
        }
    }
    public bool IsReached() 
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled() 
    {
        if (goalType == GoalType.Kill) 
        {
            currentAmount++;
        }
    }
    public void ItemCollected() 
    {
        if (goalType == GoalType.Gathering) 
        {
            currentAmount++;
        }
    }
}

public enum GoalType 
{
    Kill,
    Gathering
}
