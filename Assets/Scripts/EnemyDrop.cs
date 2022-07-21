using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int goldAmount;

    public int GoldAmount
    {
        get 
        {
            return goldAmount;
        }
        set 
        {
            goldAmount = value;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Wallet playerWallet = collision.gameObject.GetComponent<Wallet>();
            playerWallet.AddGold(gameObject.GetComponent<EnemyDrop>().goldAmount);
            Destroy(gameObject);
            
        }
        //collision.gameObject.GetComponent<Wallet>().AddGold(this);
    }
}
