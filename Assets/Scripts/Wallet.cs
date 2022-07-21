using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int goldAmount;
    [SerializeField] private TextMeshProUGUI goldAmountTextMesh;

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

    public void AddGold(int gold) 
    {
        goldAmount += gold;
        goldAmountTextMesh.text =  string.Format("{0}",goldAmount);
    }
    // Start is called before the first frame update
    void Start()
    {
        goldAmountTextMesh.text = string.Format("{0}", goldAmount); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
