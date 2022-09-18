using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIHealth : MonoBehaviour, IHealth
{
    [SerializeField] private ProgressBar healthBarMain;
    [SerializeField] private ProgressBar healthBarInCharacterMenu;
    [SerializeField] private TextMeshProUGUI healthBarMainTextField;
    [SerializeField] private TextMeshProUGUI healthBarInCharacterMenuTextField;

    private HealthSystem healthSystem;

    void Start() 
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    public void SetHealthToUI(int health, int maxHealth) 
    {   
        healthBarMain.Maximum = maxHealth;
        healthBarMain.Current = health;
        healthBarMainTextField.text = string.Format("{0}/{1}", health, maxHealth);
        healthBarInCharacterMenu.Maximum = maxHealth;
        healthBarInCharacterMenu.Current = health;
        healthBarInCharacterMenuTextField.text = string.Format("{0}/{1}", health, maxHealth);
    }
}
