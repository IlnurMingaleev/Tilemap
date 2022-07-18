using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    [SerializeField] private float _speed;
    [SerializeField] private Joystick joystick;
    private AnimateMoves animateMoves;
    private States state;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animateMoves = gameObject.GetComponent<AnimateMoves>();
        state = States.Idle;
    }
    // ”правл€ю игроком по вводу с джойстика.
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        /*switch (state)
        {
            case States.Idle:
                break;
            case States.Attack:
                break;
            case States.Move:
                Move();
                break;
            case States.Dead:
                break;
        }*/
        
    }
    private float JoystickInput(float axis) 
    {
        if (axis > .2f)
        {
            //state = States.Move;
            return _speed;
        }
        else if (axis < -.2f)
        {
            //state = States.Move;
            return -_speed;
        }
        else 
        {
            state = States.Idle;
            return 0;
        }
    }
    public void Death() 
    {
        
    }
    public void Move() 
    {
        
        Vector2 currentPos = rigidBody2D.position;

        float horizontalMove = JoystickInput(joystick.Horizontal);
        float verticalMove = JoystickInput(joystick.Vertical);

        Vector2 moveVector = new Vector2(horizontalMove, verticalMove);
        moveVector = Vector2.ClampMagnitude(moveVector, 1); // ѕривожу к вектору длины один стобы при движении подиагонали скорость была такой же. 
        animateMoves.SetDirection(moveVector, States.Move);//устанавливаю нужную анимацию по направлению движени€.

        Vector2 newPos = currentPos + moveVector;
        rigidBody2D.MovePosition(newPos);
    }
}
