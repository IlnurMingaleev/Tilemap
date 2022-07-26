using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here is the class for having direct acces to item sprites and to the prefab of the item that should be located in the world.
public class ItemAssets : MonoBehaviour 
{ 
    

    public static ItemAssets Instance { get; private set; } //Instace of Item assets class

    private void Awake() {
        Instance = this; //Initialization
    }

    //Planning to make this private and make access through public properties
    public Transform pfItemWorld;

    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;

}
