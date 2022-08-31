using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    Vector3 originalPosition;
    public Vector3 cheapPosition;
    public Vector3 normalPosition;
    public Vector3 expensivePosition;

    private int DisplayItem;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        DisplayItem = 0;

//        itemInventory = ItemInventory.ItemInventoryInstance;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if item is clicked when on display
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Items");

            if (Physics.Raycast(ray, out hit, 120.0f, mask))
            {
                if (hit.transform.gameObject == gameObject) 
                {
                    if (DisplayItem > 0)
                    {
                        ClearDisplayStatus(DisplayItem);
                        DisplayItem = 0;
                        transform.position = originalPosition;
                    }
                }
            }
        }
    }

    void OnMouseUp()
    {
        switch(DisplayItem)
        {
            case 0:
                transform.position = originalPosition;
                break;
            case 1:
                transform.position = cheapPosition;
                SetDisplayStatus(1);
                break;
            case 2:
                transform.position = normalPosition;
                SetDisplayStatus(2);
                break;
            case 3:
                transform.position = expensivePosition;
                SetDisplayStatus(3);
                break;
            default:
                transform.position = originalPosition;
                break;

        }

    }



    void OnTriggerEnter(Collider collider)
    {

        if (collider.transform.name == "CheapCollider")
        {
            if (CheckDisplayStatus(1)) DisplayItem = 1;
        }

        if (collider.transform.name == "NormalCollider")
        {
            DisplayItem = 2;
        }

        if (collider.transform.name == "ExpensiveCollider")
        {
            DisplayItem = 3;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.transform.name == "CheapCollider" && DisplayItem == 1)
        {
            DisplayItem = 0;
        }

        if (collider.transform.name == "NormalCollider" && DisplayItem == 2)
        {
            DisplayItem = 0;
        }

        if (collider.transform.name == "ExpensiveCollider" && DisplayItem == 3)
        {
            DisplayItem = 0;
        }


    }

    bool CheckDisplayStatus(int display)
    {
        switch (display)
        {
            case 1: if (ItemInventory.Instance.DisplayObject1 == null) return true; else return false; 
            case 2: if (ItemInventory.Instance.DisplayObject2 == null) return true; else return false; 
            case 3: if (ItemInventory.Instance.DisplayObject3 == null) return true; else return false; 
            default: return false;
        }
    }

    void SetDisplayStatus(int display)
    {
        switch (display)
        {
            case 1: ItemInventory.Instance.DisplayObject1 = transform.gameObject; break;
            case 2: ItemInventory.Instance.DisplayObject2 = transform.gameObject; break;
            case 3: ItemInventory.Instance.DisplayObject3 = transform.gameObject; break;
            default: return;
        }
    }

    void ClearDisplayStatus(int display)
    {
        switch (display)
        {
            case 1: ItemInventory.Instance.DisplayObject1 = null; break;
            case 2: ItemInventory.Instance.DisplayObject2 = null; break;
            case 3: ItemInventory.Instance.DisplayObject3 = null; break;
            default: return;
        }
    }

}
