using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ��� ����������� ������������ �������� �� ������������.

public class AnimateMoves : MonoBehaviour
{
    private string[] IdleDirections = { "Idle_N", "Idle_NW", "Idle_W", "Idle_SW", "Idle_S", "Idle_SE", "Idle_E", "Idle_NE" };
    private string[] WalkDirections = { "Walk_N", "Walk_NW", "Walk_W", "Walk_SW", "Walk_S", "Walk_SE", "Walk_E", "Walk_NE" };

    private Animator animator;
    public int lastDirection { get; set; }



    void Start()
    {
        animator = GetComponent<Animator>();
        lastDirection = 4;
    }




    // ����� ������������� ����������� �������� �� ����������� ��������.
    // �������� ����� ���������� ��������
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
