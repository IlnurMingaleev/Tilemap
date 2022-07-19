using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    [SerializeField] private float _speed;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    private AnimateMoves animateMoves;
    private States state;
    Vector2 moveVector;
    public delegate void ClickAction();
    public static event ClickAction onClicked;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animateMoves = gameObject.GetComponent<AnimateMoves>();
        state = States.Idle;
        onClicked += animateAttack;
        anim = GetComponent<Animator>();
    }
    // ”правл€ю игроком по вводу с джойстика.
    // Update is called once per frame
    void Update()
    {
        float horizontalMove = JoystickInput(joystick.Horizontal);
        float verticalMove = JoystickInput(joystick.Vertical);

        anim.SetFloat("SpeedX", horizontalMove);
        anim.SetFloat("SpeedY", verticalMove);

        Vector3 movement = new Vector3(
            horizontalMove,
            verticalMove,
            0
            );
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement *= Time.deltaTime;
        
        transform.Translate(movement);
    }
    void FixedUpdate()
    {
        float lastInputX = JoystickInput(joystick.Horizontal);
        float lastInputY = JoystickInput(joystick.Vertical);


        if (lastInputX != 0 || lastInputY != 0)
        {
            anim.SetBool("walking", true);
            if (lastInputX > 0)
            {
                anim.SetFloat("LastMoveX", 1f);
            }
            else if (lastInputX < 0)
            {
                anim.SetFloat("LastMoveX", -1f);
            }
            else 
            {
                anim.SetFloat("LastMoveX", 0f);
            }



            if (lastInputY > 0)
            {
                anim.SetFloat("LastMoveY", 1f);
            }
            else if (lastInputY < 0)
            {
                anim.SetFloat("LastMoveY", -1f);
            }
            else
            {
                anim.SetFloat("LastMoveY", 0f);
            }
        }
        else
        {
            anim.SetBool("walking", false);
        }





    }
    private float JoystickInput(float axis) 
    {
        if (axis > .2f)
        {
            //state = States.Move;
            return 1;
        }
        else if (axis < -.2f)
        {
            //state = States.Move;
            return -1;
        }
        else 
        {
            return 0;
        }
    }
    public void Death() 
    {
        anim.SetBool("isDead", true);
        
    }
    public void Move() 
    {
        
        Vector2 currentPos = rigidBody2D.position;

        float horizontalMove = JoystickInput(joystick.Horizontal);
        float verticalMove = JoystickInput(joystick.Vertical);

        anim.SetFloat("SpeedX", horizontalMove);
        anim.SetFloat("SpeedY", verticalMove);

        
        moveVector = new Vector2(horizontalMove, verticalMove);
        
        //устанавливаю нужную анимацию по направлению движени€.
        animateMoves.SetDirection(moveVector, States.Move);
        
        moveVector = Vector2.ClampMagnitude(moveVector, 1);
        // ѕривожу к вектору длины один стобы при движении подиагонали скорость была такой же. 


        Vector2 newPos = currentPos + moveVector;
        rigidBody2D.MovePosition(newPos);

    }

    public void Attack() 
    {
        anim.SetBool("Attack",true);
        Invoke("SetAttackFalse", 0.9f);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, attackRange);
        //GameObject enemy;
        /*if (hit.transform.gameObject.tag == "Enemy" && hit.transform != null) 
        {
            enemy = hit.transform.gameObject;
            HealthSystem enemyHealthSystem = enemy.GetComponent<HealthSystem>();
            enemyHealthSystem.Damage(attackDamage);
        }*/
        
    }

    public void SetAttackFalse() 
    {
        anim.SetBool("Attack", false);
    }
    public void animateAttack() 
    {
        animateMoves.SetDirection(moveVector, States.Attack);
    }
}
