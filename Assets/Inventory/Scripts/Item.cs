using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to introduce items in game
[Serializable]

public class Item {

    public enum ItemType {
        Sword,
        HealthPotion,
        ManaPotion,
    }

    public ItemType itemType;
    public int amount;

    //Here we get sprites for each type of item.
    public Sprite GetSprite() {
        switch (itemType) {
        default:
        case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
        case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
        case ItemType.ManaPotion:   return ItemAssets.Instance.manaPotionSprite;
        }
    }

    // Here we get if item is stackable depending on its type.
    public bool IsStackable() 
    {
        switch (itemType) 
        {
            default:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Sword:
                return false;
        
        }
    }
}
