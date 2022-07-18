using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ��� ����������� ������������ �������� �� ������������.

public class AnimateMoves : MonoBehaviour
{
    private string[] idleDirections = { "Idle_N", "Idle_NW", "Idle_W", "Idle_SW", "Idle_S", "Idle_SE", "Idle_E", "Idle_NE" };
    private string[] walkDirections = { "Walk_N", "Walk_NW", "Walk_W", "Walk_SW", "Walk_S", "Walk_SE", "Walk_E", "Walk_NE" };
    private string[] attackDirections = { "Attack_N", "Attack_NW", "Attack_W", "Attack_SW", "Attack_S", "Attack_SE", "Attack_E", "Attack_NE" };
    private string[] deathDirections = { "Dead_N", "Dead_NW", "Dead_W", "Dead_SW", "Dead_S", "Dead_SE", "Dead_E", "Dead_NE" };

    private Animator animator;
    public int lastDirection { get; set; }



    void Start()
    {
        animator = GetComponent<Animator>();
        lastDirection = 4;
    }




    // ����� ������������� ����������� �������� �� ����������� ��������.
    // �������� ����� ���������� ��������
    public void SetDirection(Vector2 direction, States state) 
    {
        string[] directionArray = null;
        switch (state) 
        {
            case States.Move:
                directionArray = walkDirections;
                lastDirection = DirectionIndex(direction);
                break;
            case States.Attack:
                directionArray = attackDirections;
                break;
            case States.Dead:
                directionArray = deathDirections;
                break;
            case States.Idle:
                if(direction.magnitude < 0.1f) 
                {
                    directionArray = idleDirections;
                }
                
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
