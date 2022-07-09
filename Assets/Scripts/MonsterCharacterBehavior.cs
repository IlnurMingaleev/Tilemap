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
            WayVector = WayPoint - RigidBody2D.position;
        }
        else 
        {
            WayVector = new Vector2(0, 0);
        }
        AnimateMoves.SetDirection(WayVector);
    }

    
    // Update is called once per frame
    void Update()
    {

    
        monster.RandomMove();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isIdle = true;
        string monstrTriggerMessage = "Монстр отреагировал на появление игрока.";
        
        messageBox.SetActive(true);
        uiController =messageBox.GetComponent<UIController>();
        uiController.SetMonstrTriggerMessage(messageBox, monstrTriggerMessage, 2.0f);


        Debug.Log(monstrTriggerMessage);
    }





}
