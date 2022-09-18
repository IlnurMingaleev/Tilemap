using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static void LastInput(float lastInput, string nameOfParamater, Animator anim)
    {
        if (lastInput > 0)
        {
            anim.SetFloat(nameOfParamater, 1f);
        }
        else if (lastInput < 0)
        {
            anim.SetFloat(nameOfParamater, -1f);
        }
        else
        {
            anim.SetFloat(nameOfParamater, 0f);
        }

    }
}
