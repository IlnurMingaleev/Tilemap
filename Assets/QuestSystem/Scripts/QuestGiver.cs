using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Class for giving quest to the player if collider is triggered
public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject uiQuest;                    //Field responsible for the UI part of Quest
    [SerializeField] private IsometricPlayerController player;      // Field for controller of player
    [SerializeField] private Quest quest;                           // Instance of Quest class
    [SerializeField] private Animator animator;                     // Field to control Animations of the QuestGiver character

    // UI Elements
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;   
    [SerializeField] private TextMeshProUGUI expAmount;
    [SerializeField] private TextMeshProUGUI goldAmount;

    //Here we set values for UI elements
    private void Start()
    {
        title.SetText(quest.Title);
        description.SetText(quest.Description);
        expAmount.SetText(quest.ExpReward.ToString());
        goldAmount.SetText(quest.GoldReward.ToString());
        animator = GetComponent<Animator>();
    }

    // Here we assign direction for animator so that QuestGiver Character always faces player
    private void Update()
    {

        float movementX = player.gameObject.transform.position.x - transform.position.x;
        float movementY = player.gameObject.transform.position.y - transform.position.y;
        Utils.LastInput(movementX, "movementX", animator);
        Utils.LastInput(movementY, "movementY", animator);

    }

    //Method is called on ACCEPT button click
    public void AcceptQuest() 
    {
        SoundManager.PlayAudioClip(SoundManager.Sound.buttonOnClick);
        uiQuest.SetActive(false);
        quest.IsActive = true;
        player.Quest = quest;
    }

    //Collider triger methods 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        uiQuest.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        uiQuest.SetActive(false);
    }
}
