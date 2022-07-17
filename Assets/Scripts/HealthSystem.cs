using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    private UIHealth uihealth;

    void Start() 
    {
        uihealth = GetComponent<UIHealth>();
        uihealth.setHealthToUI(health, healthMax);
    }
    public HealthSystem(int healthMax) 
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth() 
    {
        return health;
    }
    public int GetMaxHealth() 
    {
        return healthMax;
    }
    public void Damage(int damageAmount) 
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        uihealth.setHealthToUI(health, healthMax);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        uihealth.setHealthToUI(health, healthMax);
    }
    
}
