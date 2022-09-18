using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    [SerializeField] private List<GameObject> spriteDir;
    [SerializeField] private List<Animator> animList;

    public Animator ActivateDir(int index) 
    { 
        spriteDir[index].SetActive(true);
        return animList[index];

    }
    public void DeactivateDir(int index) 
    {
        spriteDir[index].SetActive(false);
    }
}
