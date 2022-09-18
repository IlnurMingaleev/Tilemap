using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Key
{
    Pressed,
    NotPressed
}

public enum Movement 
{
    Instant,
    Gradual
}
public enum Direction 
{
    N,
    NW,
    W,
    SW,
    S,
    SE,
    E,
    NE

}
public class KimmiController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rigidbody;

    [Header("Directions")]

    
    [SerializeField, Range(0f, 10f)] [Tooltip("Maximum movement speed")] private float maxSpeed;
    [SerializeField, Range(0f, 50f)] [Tooltip("How fast to reach max speed")] private float maxAcceleration;
    [SerializeField, Range(0f, 50f)] [Tooltip("How fast to stop after letting go")] private float maxDeceleration;
    [SerializeField, Range(0f, 50f)] [Tooltip("How fast to stop when changing direction")] private float maxTurnSpeed;
    [SerializeField] private ObjectPool spriteDir;

    private Key key;
    private Movement movement;
    private Direction currentDirection;
    private Direction previousDirection;
    private float maxSpeedChange;
    private Vector2 moveVector;
    private int sliceCount = 8;
    private Vector2 targetVelocity;
    private Vector2 velocity;
    private float acceleration;
    private float deceleration;
    private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        previousDirection = Direction.S;
        currentDirection = Direction.S;
        movement = Movement.Gradual;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        velocity = rigidbody.velocity;

        if (movement == Movement.Gradual)
        {
            RunWithAcceleration();
        }
        else
        {            
           RunWithoutAcceleration();
        }
    }

    private void Move() 
    {
        // Input from KeyBoard
        //TO DO Input from JoyStick and also use New Input System
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveVector = new Vector2(
                horizontal,
                vertical
                );

        if (moveVector.magnitude < 0.01f) 
        {
            animator.SetBool("Walking", false);
            key = Key.NotPressed;
        } 
        else 
        {
            key = Key.Pressed;
            animator.SetBool("Walking", true);

            moveVector = Vector2.ClampMagnitude(moveVector, 1f);

            SetAnimatorDirection();

            ChangeDirection();
        }
        
         
        targetVelocity = moveVector * maxSpeed;
    }

    private void RunWithAcceleration()
    {
        acceleration = maxAcceleration;
        turnSpeed = maxTurnSpeed;
        deceleration = maxDeceleration;


        if (key == Key.Pressed)
        {
            if (DirectionUtility.DirectionIndex(moveVector, sliceCount) !=
                DirectionUtility.DirectionIndex(velocity, sliceCount))
            {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else
            {
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            maxSpeedChange = deceleration * Time.deltaTime;
        }
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, targetVelocity.y, maxSpeedChange);

        rigidbody.velocity = velocity;
        //animator.SetFloat("PlayerSpeed", Mathf.Abs(velocity.x));
    }

    private void RunWithoutAcceleration()
    {
        velocity.x = targetVelocity.x;
        velocity.y = targetVelocity.y;
        //animator.SetFloat("PlayerSpeed", Mathf.Abs(velocity.x));
        rigidbody.velocity = velocity;

    }
    private void SetAnimatorDirection() 
    {
        animator.SetFloat("movementX", moveVector.x);
        animator.SetFloat("movementY", moveVector.y);
    }
    

    private void Flip() 
    {
        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    private void ChangeDirection() 
    {
        previousDirection = currentDirection;
        spriteDir.DeactivateDir((int)previousDirection);
        currentDirection = (Direction)DirectionUtility.DirectionIndex(moveVector, sliceCount);
        if (transform.localScale.x > 0)
        {
            if (currentDirection == Direction.NW ||
                currentDirection == Direction.W ||
                currentDirection == Direction.SW) 
                Flip();
        }
        else if (transform.localScale.x < 0)
        {
            if (currentDirection == Direction.N ||
                currentDirection == Direction.S ||
                currentDirection == Direction.SE ||
                currentDirection == Direction.E ||
                currentDirection == Direction.NE)
                Flip();
        }
        animator = spriteDir.ActivateDir((int)currentDirection);
    }
}
