using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject monster;

   
    public void SetMonstrTriggerMessage(GameObject gObject, string message, float seconds) 
    {
        textMesh.text = message;
        StartCoroutine(MeassgeDuration(gObject,seconds));
        
    }
    IEnumerator MeassgeDuration(GameObject gObject, float seconds)
    { 

        yield return new WaitForSeconds(seconds);

        gObject.SetActive(false);
        monster.GetComponent<MonsterCharacterBehavior>().IsIdle = false;
    }
}
