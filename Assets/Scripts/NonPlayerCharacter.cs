using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private Vector2 spawnLocation;
    [SerializeField] private float allowedRange;
    [SerializeField] private float _speed;
    public float Speed 
    {
        get 
        {
            return _speed;
        }
    }

    public Vector2 WayVector { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }
    public AnimateMoves AnimateMoves { get; set; }
    public Vector2 WayPoint { get; set; }
    public bool IsGenerated { get; set; }
    public float WPradius { get; set; }


    private void Start()
    {
         
    }
    public Vector2 PolarToWayPoint() 
    {
        float theta = UnityEngine.Random.Range(.0f, (float) Math.PI * 2);
        float radius = UnityEngine.Random.Range(1.0f, allowedRange);
        Vector2 randomPolarPoint = new Vector2((float) (radius * Math.Cos(theta)), (float) (radius * Math.Sin(theta)));
        return spawnLocation + randomPolarPoint;
    }

    public abstract void RandomMove();
    


}
