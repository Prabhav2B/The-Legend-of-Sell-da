using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfferButton : MonoBehaviour
{

    private ItemInventory _itemInventory;
    private bool btnLock;

    private Button btn;
    private Image img;
    private TMP_Text _text;

    void Start()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
        
        btn.interactable = false;
        _itemInventory = FindObjectOfType<ItemInventory>();
        btnLock = true;
        
        DeactivateButton();
    }

    void Update()
    {
        
        if(btnLock)
            return;
        
        if (_itemInventory.InventoryFull())
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }

    public void DeactivateButton()
    {
        img.enabled = false;
        _text.enabled = false;
        btn.interactable = false;
        btnLock = true;
    }

    public void ActivateButton()
    {
        img.enabled = true;
        _text.enabled = true;
        btnLock = false;
    }
}
