using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс для правильного анимирования движения по направлениям.

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




    // Метод устанавливает направление анимации по направлению движения.
    // Выбирает самую подходящую анимацию
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




    // Переводит вектор в индекс анимации нужного направления.
    public static int DirectionIndex(Vector2 direction)
    { 
        // Возвращает вектор длины один

        Vector2 normalizedDirection = direction.normalized;
        
        //Делим на восемь потому что восемь направлений в компасе

        float step = 360 / 8;
        
        // Получаем угол на который может отклониться вектор от данного направления компаса

        float offset = step / 2;

        //Угол между векторами

        float angle = Vector2.SignedAngle(Vector2.up, normalizedDirection);

        angle += offset;

        //Избегаем использование отрицательных углов

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        //Возвращает индекс который равен или меньше данного угла

        return Mathf.FloorToInt(stepCount);

    }
}
