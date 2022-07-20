using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacterBehavior : NonPlayerCharacter
{
    [SerializeField] private GameObject messageBox;
    [SerializeField] private GameObject player;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackspeed;
    [SerializeField] private int expAmount;


    private MonsterCharacterBehavior monster;
    private CircleCollider2D circleCollider2D;
    private UIController uiController;
    private bool isIdle;
    private State state;
    private HealthSystem playerHealthSystem;
    private ExperienceSystem playerExperienceSystem;
    private float canAttack;
    private Animator animator;

    
    public enum State 
    {
        Wander,
        Chase,
        Attack,
        Dead
    }
    //Инициализируем поля
    void Start()
    {
        playerExperienceSystem = player.GetComponent<ExperienceSystem>();
        RigidBody2D = GetComponent<Rigidbody2D>();
        monster = GetComponent<MonsterCharacterBehavior>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        monster.WayPoint = PolarToWayPoint();
        monster.WPradius = 0.3f;
        monster.IsGenerated = true;
        state = State.Wander;
        playerHealthSystem = player.GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
    }
    // Метод осуществляет случайное перемещение по вейпоинтам
    // IsIdle осуществляет переключение анимации при появлении сообщения, 
    // IsGenerated использую чтобы вейпоинт генерировался по очереди, а не кучей друг за другом.
    // WPradius дистанция от вейпоинта при которой генерируется новый вейпоинт.
    public override void RandomMove()
    {
       if (!IsGenerated && Vector3.Distance(WayPoint, transform.position) < WPradius)
       {
           WayPoint = PolarToWayPoint();
       }
       else
       {
            IsGenerated = false;
       }
       
       WayVector = WayPoint - transform.position;
        float movementX = Vector3.Project(WayVector,Vector3.right).x;
        float movementY = Vector3.Project(WayVector, Vector3.up).y;
        if (movementX != 0 || movementY != 0)
        {
            animator.SetBool("walking", true);
            Utils.LastInput(movementX, "movementX", animator);
            Utils.LastInput(movementY, "movementY", animator);
        }
        else
        {
            animator.SetBool("walking", false);
        }
        // Определяю вектор на который нужно ориентировать анимацию
       transform.position = Vector2.MoveTowards(transform.position, WayPoint, Time.deltaTime * monster.Speed);
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
                //Debug.Log("Started chasing");
                break;
            case State.Attack:
                monster.Attack();
                break;
            case State.Dead:
                break;
        }
    
        //monster.RandomMove();
        
    }
    //Монстр реагирует на появление игрока
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && state != State.Dead)
        {
            state = State.Chase;
            //Debug.Log("collision happened");
        }
        /*isIdle = true;
        string monstrTriggerMessage = "Монстр отреагировал на появление игрока.";
        
        messageBox.SetActive(true);
        uiController = messageBox.GetComponent<UIController>();
        uiController.SetMonstrTriggerMessage(messageBox, monstrTriggerMessage, 2.0f);


        Debug.Log(monstrTriggerMessage);*/
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && state != State.Dead)
        {
            state = State.Wander;
            //Debug.Log("collision ended");
        }
        
    }


    private void FindTarget() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * monster.Speed);
        WayVector = player.transform.position-transform.position;
        float movementX = Vector3.Project(WayVector, Vector3.right).x;
        float movementY = Vector3.Project(WayVector, Vector3.up).y;
        if (movementX != 0 || movementY != 0)
        {
            animator.SetBool("walking", true);
            Utils.LastInput(movementX, "movementX", animator);
            Utils.LastInput(movementY, "movementY", animator);
        }
        else
        {
            animator.SetBool("walking", false);
        }
        
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= attackRange)
        {
            state = State.Attack;
        }

    }

    private void Attack() 
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > attackRange)
        {
            state = State.Chase;
        }
        else 
        {
            animator.SetBool("attacking", true);
            Invoke("SetAttackFalse", 0.9f);
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
    public void SetAttackFalse()
    {
        animator.SetBool("attacking", false);
    }

    public void DeactivateEnemy() 
    {
        gameObject.SetActive(false);
    }
    public void Death() 
    {
        animator.SetBool("isDead", true);
        state = State.Dead;
        Invoke("DeactivateEnemy", 2.0f);

        //isDead = true;
    }

}
