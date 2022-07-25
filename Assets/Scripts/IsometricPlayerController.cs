using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private UI_Inventory uiInventory;

    private Rigidbody2D rigidBody2D;
    private CharacterStats stats;
    private Animator anim;
    private Vector3[] rays;
    private Vector3 direction;
    private Inventory inventory;
    private Quest quest;

    public Quest Quest 
    {
        get 
        {
            return quest;
        }

        set 
        {
            quest = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);
    }
    private void UseItem(Item item) 
    {
        switch (item.itemType) 
        {
            case Item.ItemType.HealthPotion:
                Debug.Log("Health Potion is used");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
                Debug.Log("Mana Potion is used");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
            case Item.ItemType.Sword:
                Debug.Log("Sword can only be equipped");
                break;
        }
    }
    public Vector3 GetPosition() 
    {
        return gameObject.transform.position;
    }

    public Vector3 GetDirecton() 
    {
        return direction;
    }
    // ”правл€ю игроком по вводу с джойстика.
    // Update is called once per frame
    void Update()
    {
        float horizontalMove = JoystickInput(joystick.Horizontal);
        float verticalMove = JoystickInput(joystick.Vertical);
        if (Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove) < float.Epsilon) return;

        anim.SetFloat("SpeedX", horizontalMove);
        anim.SetFloat("SpeedY", verticalMove);

        Vector3 movement = new Vector3(
            horizontalMove,
            verticalMove,
            0
            );
        movement *= stats.Speed;
        movement = Vector3.ClampMagnitude(movement, stats.Speed);
        movement *= Time.deltaTime;

        transform.Translate(movement);
        if (rays != null)
        {
            foreach (Vector3 ray in rays)
            {
                Debug.DrawRay(transform.position, ray, Color.red);
            }
        }
    }
    void FixedUpdate()
    {
        float lastInputX = JoystickInput(joystick.Horizontal);
        float lastInputY = JoystickInput(joystick.Vertical);


        if (lastInputX != 0 || lastInputY != 0)
        {
            anim.SetBool("walking", true);

            Utils.LastInput(lastInputX, "LastMoveX", anim);
            Utils.LastInput(lastInputY, "LastMoveY", anim);
            direction = new Vector3(lastInputX, lastInputY, 0);
        }
        else
        {
            anim.SetBool("walking", false);
        }





    }

    private float JoystickInput(float axis)
    {
        if (axis > .2f)
        {
            //state = States.Move;
            return 1;
        }
        else if (axis < -.2f)
        {
            //state = States.Move;
            return -1;
        }
        else
        {
            return 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null) 
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    public void Death()
    {
        anim.SetBool("isDead", true);

    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
        Invoke(nameof(SetAttackFalse), stats.AttackSpeed);
        Raycast2Damage(direction, 20, 5.0f, stats.AttackRange);


    }

    public void SetAttackFalse()
    {
        anim.SetBool("Attack", false);
    }

    public void RayVectors(Vector3 wayVector, int oddQuantity, float angle)
    {
        rays = new Vector3[oddQuantity];
        for (int i = 0; i < oddQuantity; i++)
        {
            rays[i] = wayVector.Rotate(angle * i - 90.0f);

        }
    }
    public void Raycast2Damage(Vector3 wayVector, int oddQuantity, float angle, float distance)
    {
        RaycastHit2D hit;
        GameObject enemy;
        RayVectors(wayVector, oddQuantity, angle);
        for (int i = 0; i < oddQuantity; i++)
        {
            hit = Physics2D.Raycast(transform.position, rays[i], distance);
            if (hit.collider)
            {
                if (hit.collider.gameObject.CompareTag("Enemy")) 
                {
                    enemy = hit.collider.gameObject;
                    HealthSystem enemyHealthSystem = enemy.GetComponent<HealthSystem>();
                    enemyHealthSystem.Damage(stats.Damage);
                    break;

                }
            }
            

                
            
        }
    }
}




