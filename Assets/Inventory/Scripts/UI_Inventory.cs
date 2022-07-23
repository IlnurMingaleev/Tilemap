using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Inventory : MonoBehaviour {

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private IsometricPlayerController player;

    private void Awake() {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemTemplate");
    }
    public void SetPlayer(IsometricPlayerController player)
    {
        this.player = player;
    }

    
    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        Vector2 offset = new Vector2(-120, -300);
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 40f;
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            Button useButton = itemSlotRectTransform.Find("Image").GetComponent<Button>();
            useButton.onClick.AddListener(delegate { OnUseButtonClickedUse(item);  });
            Button closeButton = itemSlotRectTransform.Find("Close Button").GetComponent<Button>();
            closeButton.onClick.AddListener(delegate { OnCloseButtonClickedRemove(item); });
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize) + offset;
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
            x++;
            if (x >= 7) {
                x = 0;
                y++;
            }
        }
    }

    public void OnCloseButtonClickedRemove(Item item)
    {
        Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
        inventory.RemoveItem(item);
        ItemWorld.DropItem(player.GetPosition(), player.GetDirecton(), duplicateItem);
    }

    public void OnUseButtonClickedUse(Item item) 
    {
        inventory.UseItem(item);
    }

}
