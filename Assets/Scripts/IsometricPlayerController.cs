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
    private States state;
    Vector2 moveVector;
    private Animator anim;
    private Vector3[] rays;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        state = States.Idle;

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
        movement *= _speed;
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement *= Time.deltaTime;

        transform.Translate(movement);
        if (rays != null)
        {
            foreach (Vector3 ray in rays)
            {
                Debug.DrawRay(transform.position, ray, Color.red);
            }
        }
    }
    void FixedUpdate()
    {
        float lastInputX = JoystickInput(joystick.Horizontal);
        float lastInputY = JoystickInput(joystick.Vertical);


        if (lastInputX != 0 || lastInputY != 0)
        {
            anim.SetBool("walking", true);

            Utils.LastInput(lastInputX, "LastMoveX", anim);
            Utils.LastInput(lastInputY, "LastMoveY", anim);
            direction = new Vector3(lastInputX, lastInputY, 0);
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

        //устанавливаю нужную анимацию по направлению движени€

        moveVector = Vector2.ClampMagnitude(moveVector, 1);
        // ѕривожу к вектору длины один стобы при движении подиагонали скорость была такой же. 


        Vector2 newPos = currentPos + moveVector;
        rigidBody2D.MovePosition(newPos);

    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
        Invoke("SetAttackFalse", 0.9f);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, attackRange);
        //GameObject enemy;
        /*if (hit.transform.gameObject.tag == "Enemy" && hit.transform != null) 
        {
            enemy = hit.transform.gameObject;
            HealthSystem enemyHealthSystem = enemy.GetComponent<HealthSystem>();
            enemyHealthSystem.Damage(attackDamage);
        }*/
        GetEnemy(direction, 20, 5.0f, 5.0f);


    }

    public void SetAttackFalse()
    {
        anim.SetBool("Attack", false);
    }

    public void RayVectors(Vector3 wayVector, int oddQuantity, float angle)
    {
        rays = new Vector3[oddQuantity];
        for (int i = 0; i < oddQuantity; i++)
        {
            rays[i] = wayVector.Rotate(angle * i - 90.0f);

        }
    }
    public void GetEnemy(Vector3 wayVector, int oddQuantity, float angle, float distance)
    {
        RaycastHit2D hit;
        GameObject enemy;
        RayVectors(wayVector, oddQuantity, angle);
        for (int i = 0; i < oddQuantity; i++)
        {
            hit = Physics2D.Raycast(transform.position, rays[i], distance);
            if (hit.collider)
            {
                if (hit.collider.gameObject.CompareTag("Enemy")) 
                {
                    enemy = hit.collider.gameObject;
                    HealthSystem enemyHealthSystem = enemy.GetComponent<HealthSystem>();
                    enemyHealthSystem.Damage(attackDamage);
                    break;

                }
            }
            

                
            
        }
    }
}




