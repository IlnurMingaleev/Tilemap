using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacterBehavior : NonPlayerCharacter
{
    MonsterCharacterBehavior monster;

    void Start()
    {

        monster = GetComponent<MonsterCharacterBehavior>();
        monster.RigidBody2D = GetComponent<Rigidbody2D>();
        monster.AnimateMoves = GetComponent<AnimateMoves>();
        monster.WayPoint = PolarToWayPoint();
        monster.WPradius = 0.3f;
        monster.IsGenerated = true;
    }
    public override void RandomMove()
    {

        if (!IsGenerated && Vector3.Distance(WayPoint, transform.position) < WPradius)
        {
            WayPoint = PolarToWayPoint();
        }
        else
        {
            IsGenerated = false;
        }
        transform.position = Vector2.MoveTowards( transform.position, WayPoint, Time.deltaTime * monster.Speed);
        WayVector = WayPoint - RigidBody2D.position;
        AnimateMoves.SetDirection(WayVector);

    }

    
    // Update is called once per frame
    void Update()
    {
        monster.RandomMove();
    }
  
    
    
    


}
