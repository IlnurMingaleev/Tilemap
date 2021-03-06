using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    private IHealth uihealth;

    void Start() 
    {
        uihealth = GetComponent<IHealth>();
        uihealth.SetHealthToUI(health, healthMax);
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
        if (health <= 0) 
        {
            health = 0;
            if (gameObject.CompareTag("Enemy")) 
            {
                gameObject.GetComponent<MonsterCharacterBehavior>().Death();
            }
            if (gameObject.CompareTag("Player")) 
            {
                gameObject.GetComponent<IsometricPlayerController>().Death();
            }
        }
        uihealth.SetHealthToUI(health, healthMax);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        uihealth.SetHealthToUI(health, healthMax);
    }
    
}
