using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacterStats : CommonStats
{
    [SerializeField] private int expAmount;

    public int ExpAmount { get => expAmount; }
}
