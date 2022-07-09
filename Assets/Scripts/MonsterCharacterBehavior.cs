using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacterBehavior : NonPlayerCharacter
{
    private MonsterCharacterBehavior monster;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private GameObject messageBox;
    private UIController uiController;
    private bool isIdle;
    public bool IsIdle 
    {
        get 
        {
            return isIdle;
        }
        set 
        {
            isIdle = value;
        }
    }
    //Инициализируем поля
    void Start()
    {

        monster = GetComponent<MonsterCharacterBehavior>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        monster.RigidBody2D = GetComponent<Rigidbody2D>();
        monster.AnimateMoves = GetComponent<AnimateMoves>();
        monster.WayPoint = PolarToWayPoint();
        monster.WPradius = 0.3f;
        monster.IsGenerated = true;
        isIdle = false;
    }
    // Метод осуществляет случайное перемещение по вейпоинтам
    // IsIdle осуществляет переключение анимации при появлении сообщения, 
    // IsGenerated использую чтобы вейпоинт генерировался по очереди, а не кучей друг за другом.
    // WPradius дистанция от вейпоинта при которой генерируется новый вейпоинт.
    public override void RandomMove()
    {
        if (!isIdle)
        {
            if (!IsGenerated && Vector3.Distance(WayPoint, transform.position) < WPradius)
            {
                WayPoint = PolarToWayPoint();
            }
            else
            {
                IsGenerated = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, WayPoint, Time.deltaTime * monster.Speed);
            WayVector = WayPoint - RigidBody2D.position; // Определяю вектор на который нужно ориентировать анимацию
        }
        else 
        {
            WayVector = new Vector2(0, 0); // Устанавливаю вектор нулевым чтобы монстр принимал Idle анимацию
         }
        AnimateMoves.SetDirection(WayVector);
    }

    
    // Update is called once per frame
    void Update()
    {

    
        monster.RandomMove();
        
    }
    //Монстр реагирует на появление игрока
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isIdle = true;
        string monstrTriggerMessage = "Монстр отреагировал на появление игрока.";
        
        messageBox.SetActive(true);
        uiController = messageBox.GetComponent<UIController>();
        uiController.SetMonstrTriggerMessage(messageBox, monstrTriggerMessage, 2.0f);


        Debug.Log(monstrTriggerMessage);
    }





}
