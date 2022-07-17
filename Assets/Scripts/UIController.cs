using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject monster;



   // Объявляем метод который должен выводить сообщение в текстмешпро при срабатывании Collision Trigger у монстра.
   // gObject Объект MessageBox в сцене.
   // message Сообщение которое выводиться на экран.
   // seconds Кол-во секунд которое сообщение будет на экране.
    public void SetMonstrTriggerMessage(GameObject gObject, string message, float seconds) 
    {
        textMesh.text = message;
        StartCoroutine(MeassgeDuration(gObject,seconds));
        
    }

     // Создаем IEnumerator для Коурутины для отведения времени активности окна с сообщением.
    IEnumerator MeassgeDuration(GameObject gObject, float seconds)
    { 

        yield return new WaitForSeconds(seconds);

        gObject.SetActive(false);
        monster.GetComponent<MonsterCharacterBehavior>().IsIdle = false;
    }
}
