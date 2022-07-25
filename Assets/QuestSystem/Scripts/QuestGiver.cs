using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject uiQuest;
    [SerializeField] private IsometricPlayerController player;
    [SerializeField] private Quest quest;
    [SerializeField] private Animator animator;

    // UI
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI expAmount;
    [SerializeField] private TextMeshProUGUI goldAmount;

    private void Start()
    {
        title.SetText(quest.Title);
        description.SetText(quest.Description);
        expAmount.SetText(quest.ExpReward.ToString());
        goldAmount.SetText(quest.GoldReward.ToString());
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        float movementX = player.gameObject.transform.position.x - transform.position.x;
        float movementY = player.gameObject.transform.position.y - transform.position.y;
        Utils.LastInput(movementX, "movementX", animator);
        Utils.LastInput(movementY, "movementY", animator);

    }

    public void AcceptQuest() 
    {
        uiQuest.SetActive(false);
        quest.IsActive = true;
        player.Quest = quest;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        uiQuest.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        uiQuest.SetActive(false);
    }
}
