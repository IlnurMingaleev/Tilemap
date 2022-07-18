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
    [SerializeField] private GameObject player;
    private State state;
    [SerializeField] private float attackRange;
    private HealthSystem playerHealthSystem;
    [SerializeField] private float attackspeed;
    private float canAttack;
    public enum State 
    {
        Wander,
        Chase,
        Attack
    }
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
    //�������������� ����
    void Start()
    {
        RigidBody2D = GetComponent<Rigidbody2D>();
        monster = GetComponent<MonsterCharacterBehavior>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        monster.AnimateMoves = GetComponent<AnimateMoves>();
        monster.WayPoint = PolarToWayPoint();
        monster.WPradius = 0.3f;
        monster.IsGenerated = true;
        isIdle = false;
        state = State.Wander;
        playerHealthSystem = player.GetComponent<HealthSystem>();
    }
    // ����� ������������ ��������� ����������� �� ����������
    // IsIdle ������������ ������������ �������� ��� ��������� ���������, 
    // IsGenerated ��������� ����� �������� ������������� �� �������, � �� ����� ���� �� ������.
    // WPradius ��������� �� ��������� ��� ������� ������������ ����� ��������.
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
            Vector3 tempVector = transform.position;
            WayVector = WayPoint - new Vector2(tempVector.x,tempVector.y);// ��������� ������ �� ������� ����� ������������� ��������
            transform.position = Vector2.MoveTowards(transform.position, WayPoint, Time.deltaTime * monster.Speed);
             
        }
        else 
        {
            WayVector = new Vector2(0, 0); // ������������ ������ ������� ����� ������ �������� Idle ��������
         }
        AnimateMoves.SetDirection(WayVector);
    }

    
    // Update is called once per frame
    void Update()
    {
        switch (state) 
        {
            case State.Wander:
                monster.RandomMove();
                break;
            case State.Chase:
                monster.FindTarget();
                Debug.Log("Started chasing");
               
                break;
            case State.Attack:
                monster.Attack();
                break;
        }
    
        //monster.RandomMove();
        
    }
    //������ ��������� �� ��������� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            state = State.Chase;
            Debug.Log("collision happened");
        }
        /*isIdle = true;
        string monstrTriggerMessage = "������ ������������ �� ��������� ������.";
        
        messageBox.SetActive(true);
        uiController = messageBox.GetComponent<UIController>();
        uiController.SetMonstrTriggerMessage(messageBox, monstrTriggerMessage, 2.0f);


        Debug.Log(monstrTriggerMessage);*/
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            state = State.Wander;
            Debug.Log("collision ended");
        }
        
    }


    private void FindTarget() 
    {
        Debug.Log("We are in Find Position");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * monster.Speed);
        WayVector = player.GetComponent<Rigidbody2D>().position-RigidBody2D.position;
        AnimateMoves.SetDirection(WayVector);
        
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= attackRange)
        {
            state = State.Attack;
        }

    }

    private void Attack() 
    {
        if (attackspeed <= canAttack)
        {
            playerHealthSystem.Damage(10);
            canAttack = 0;
        }
        else 
        {
            canAttack += Time.deltaTime;
        }
    
    }


}
