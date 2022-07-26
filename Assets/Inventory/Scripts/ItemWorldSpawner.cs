using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is responsible for spawning item from inventory in the game world. 
public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
    private void Awake()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
