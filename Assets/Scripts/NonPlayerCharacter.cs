using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Родительский класс для всех не игровых персонажей.

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
    // Объявил свойства так как использую их в классе наследнике.
    // Стараюсь не использовать публичные поля
    // Но пока не знаю как получать доступ к приватным полям кроме как использую геттеры и сеттеры.

    public Vector3 WayVector { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }

    //public AnimateMoves AnimateMoves { get; set; }
    public Vector3 WayPoint { get; set; }
    public bool IsGenerated { get; set; }
    public float WPradius { get; set; }


    //
    // Создаем случайную точку в полярных координатах и переводим точку в декартову систему координат
    //

    public Vector3 PolarToWayPoint() 
    {
        float theta = UnityEngine.Random.Range(.0f, (float) Math.PI * 2);
        float radius = UnityEngine.Random.Range(1.0f, allowedRange);
        Vector3 randomPolarPoint = new Vector3((float) (radius * Math.Cos(theta)), (float) (radius * Math.Sin(theta)), 0.0f);
        return spawnLocation + randomPolarPoint;
    }

    //
    // По хорошему наверно стоит реализовать этот метод в родителе чтобы я мог его исползовать для любого наследника без повторения кода.
    // но пока реализовал только в наследнике из за того что часть объектов метода требуют доступа к экземляру класса, а у абстрактного класса экземпляров быть не может 

    public abstract void RandomMove();
    


}
