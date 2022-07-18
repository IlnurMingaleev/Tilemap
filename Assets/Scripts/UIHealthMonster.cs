using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthMonster : MonoBehaviour, IHealth
{
    [SerializeField] private ProgressBar monsterHealhBar;
    [SerializeField] private TextMeshProUGUI monsterHealthTextField;
    private HealthSystem monsterHealthSystem;

    public void SetHealthToUI(int health, int maxHealth)
    {
        monsterHealhBar.Maximum = maxHealth;
        monsterHealhBar.Current = health;
        monsterHealthTextField.text = string.Format("{0}/{1}", health, maxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterHealthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
