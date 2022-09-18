using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionUtility
{
    //this method converts a Vector2 direction to an index of slice around circle
    public static int DirectionIndex(Vector2 direction, int sliceCount)
    {
        
        //get the normalized direction
        Vector2 normalizedDirection = direction.normalized;

        //Calculate how many degrees one slice is
        float step = 360 / sliceCount;
        
        //Calculate how many degrees is half of slice is 
        float offset = step / 2;

        //Get the angle  from -180 to 180  of the direction vector relative to Up vecy
        float angle = Vector2.SignedAngle(Vector2.up, normalizedDirection);

        //add the half of slice
        angle += offset;

        //if angle is  negative then add 360 degrees to make angle positive
        if (angle < 0)
        {
            angle += 360;
        }

        //Calculate the amount of steps required to reach this angle
        float stepCount = angle / step;

        //Round the answer to make it integer
        return Mathf.FloorToInt(stepCount);

    }
}
