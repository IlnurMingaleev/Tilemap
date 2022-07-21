using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//����� ��� ����������� ������������ �������� �� ������������.

public class AnimateMoves : MonoBehaviour
{
    private string[] idleDirections = { "Idle_N", "Idle_NW", "Idle_W", "Idle_SW", "Idle_S", "Idle_SE", "Idle_E", "Idle_NE" };
    private string[] walkDirections = { "Walk_N", "Walk_NW", "Walk_W", "Walk_SW", "Walk_S", "Walk_SE", "Walk_E", "Walk_NE" };
    private string[] attackDirections = { "Attack1_N", "Attack1_NW", "Attack1_W", "Attack1_SW", "Attack1_S", "Attack1_SE", "Attack1_E", "Attack1_NE" };
    private string[] deathDirections = { "Dead_N", "Dead_NW", "Dead_W", "Dead_SW", "Dead_S", "Dead_SE", "Dead_E", "Dead_NE" };
    //private bool isAttacked;
    private Animator animator;
    public int lastDirection { get; set; }
    public Animator Animator 
    {
        get 
        {
            return animator;
        }
        set 
        {
            animator = value;
        }
    }



    void Start()
    {
        animator = GetComponent<Animator>();
        lastDirection = 4;
        //isAttacked = false;
    }




    // ����� ������������� ����������� �������� �� ����������� ��������.
    // �������� ����� ���������� ��������
    public void SetDirection(Vector2 direction, States state) 
    {
        string[] directionArray = null;
        if (state == States.Move && direction.magnitude < 0.01f) 
        {
            
        }
        switch (state) 
        {
            case States.Move:
                directionArray = walkDirections;
                lastDirection = DirectionIndex(direction);
                break;
            case States.Attack:
                directionArray = attackDirections;
                //isAttacked = true;
                break;
            case States.Dead:
                directionArray = deathDirections;
                break;
            case States.Idle:
                directionArray = idleDirections;
                break;
            
        }
        animator.Play(directionArray[lastDirection]);
        
    }




    // ��������� ������ � ������ �������� ������� �����������.
    public static int DirectionIndex(Vector2 direction)
    { 
        // ���������� ������ ����� ����

        Vector2 normalizedDirection = direction.normalized;
        
        //����� �� ������ ������ ��� ������ ����������� � �������

        float step = 360 / 8;
        
        // �������� ���� �� ������� ����� ����������� ������ �� ������� ����������� �������

        float offset = step / 2;

        //���� ����� ���������

        float angle = Vector2.SignedAngle(Vector2.up, normalizedDirection);

        angle += offset;

        //�������� ������������� ������������� �����

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        //���������� ������ ������� ����� ��� ������ ������� ����

        return Mathf.FloorToInt(stepCount);

    }
}
