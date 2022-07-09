using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    [SerializeField] private float _speed;
    [SerializeField] private Joystick joystick;
    private AnimateMoves animateMoves;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animateMoves = gameObject.GetComponent<AnimateMoves>();
    }
    // ”правл€ю игроком по вводу с джойстика.
    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 currentPos = rigidBody2D.position;
        
        float horizontalMove = JoystickInput(joystick.Horizontal);
        float verticalMove = JoystickInput(joystick.Vertical);

        Vector2 moveVector = new Vector2(horizontalMove, verticalMove);
        moveVector = Vector2.ClampMagnitude(moveVector, 1); // ѕривожу к вектору длины один стобы при движении подиагонали скорость была такой же. 
        animateMoves.SetDirection(moveVector);//устанавливаю нужную анимацию по направлению движени€.

        Vector2 newPos = currentPos + moveVector;
        rigidBody2D.MovePosition(newPos);
    }
    private float JoystickInput(float axis) 
    {
        if (axis > .2f)
        {
            return _speed;
        }
        else if (axis < -.2f)
        {
            return -_speed;
        }
        else 
        {
            return 0;
        }
    }
}
