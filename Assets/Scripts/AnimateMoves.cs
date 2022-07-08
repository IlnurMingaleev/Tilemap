using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMoves : MonoBehaviour
{
    private string[] IdleDirections = { "Idle_N", "Idle_NW", "Idle_W", "Idle_SW", "Idle_S", "Idle_SE", "Idle_E", "Idle_NE" };
    private string[] WalkDirections = { "Walk_N", "Walk_NW", "Walk_W", "Walk_SW", "Walk_S", "Walk_SE", "Walk_E", "Walk_NE" };

    private Animator animator;
    public int lastDirection { get; set; }
   // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        lastDirection = 4;
    }

    public void SetDirection(Vector2 direction) 
    {
        string[] directionArray = null;


        if (direction.magnitude < .01f) //Character is static.
        {
            directionArray = IdleDirections;
        }
        else 
        {
            directionArray = WalkDirections;

            lastDirection = DirectionIndex(direction); // Get the index of the slcie from the direction vector
        }
        animator.Play(directionArray[lastDirection]);
    }

    // Converts Vector2 direction to an index which we use to detemine animation.
    public static int DirectionIndex(Vector2 direction)
    { 
        //Returns this vector with a magnitude of 1 (Read Only).
        //When normalized, a vector keeps the same direction but its length is 1.0.

        Vector2 normalizedDirection = direction.normalized;
        
        // We are dividing to 8 because we have 8 directions in compass

        float step = 360 / 8;
        // By this calculation we achieve offset from each side of compass direction

        float offset = step / 2;

        //Angle between vectors

        float angle = Vector2.SignedAngle(Vector2.up, normalizedDirection);

        angle += offset;

        //Here we avoid using negative angles

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        //Returns the largest integer smaller to or equal to

        return Mathf.FloorToInt(stepCount);

    }
}
