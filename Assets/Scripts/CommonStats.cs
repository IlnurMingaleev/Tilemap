using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonStats : MonoBehaviour
{
    [SerializeField] private Stat damage;
    [SerializeField] private Stat armor;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;

    public int Damage
    {
        get
        {
            return damage.GetValue();
        }
    }
    public int Armor
    {
        get
        {
            return armor.GetValue();
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
    }
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
    }
}
