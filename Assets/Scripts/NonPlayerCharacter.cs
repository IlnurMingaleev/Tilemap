using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//������������ ����� ��� ���� �� ������� ����������.

public abstract class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private float allowedRange;
    [SerializeField] private float _speed;
    public float Speed 
    {
        get 
        {
            return _speed;
        }
    }
    // ������� �������� ��� ��� ��������� �� � ������ ����������.
    // �������� �� ������������ ��������� ����
    // �� ���� �� ���� ��� �������� ������ � ��������� ����� ����� ��� ��������� ������� � �������.

    public Vector3 WayVector { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }

    //public AnimateMoves AnimateMoves { get; set; }
    public Vector3 WayPoint { get; set; }
    public bool IsGenerated { get; set; }
    public float WPradius { get; set; }


    //
    // ������� ��������� ����� � �������� ����������� � ��������� ����� � ��������� ������� ���������
    //

    public Vector3 PolarToWayPoint() 
    {
        float theta = UnityEngine.Random.Range(.0f, (float) Math.PI * 2);
        float radius = UnityEngine.Random.Range(1.0f, allowedRange);
        Vector3 randomPolarPoint = new Vector3((float) (radius * Math.Cos(theta)), (float) (radius * Math.Sin(theta)), 0.0f);
        return spawnLocation + randomPolarPoint;
    }

    //
    // �� �������� ������� ����� ����������� ���� ����� � �������� ����� � ��� ��� ����������� ��� ������ ���������� ��� ���������� ����.
    // �� ���� ���������� ������ � ���������� �� �� ���� ��� ����� �������� ������ ������� ������� � ��������� ������, � � ������������ ������ ����������� ���� �� ����� 

    public abstract void RandomMove();
    


}
