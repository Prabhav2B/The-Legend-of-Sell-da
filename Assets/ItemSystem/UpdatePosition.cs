using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 dragScale;

    public Vector3 cheapPosition = new Vector3(-1.628f, 1.195f, -8.985f);
    public Vector3 normalPosition = new Vector3(-0.538f, 1.1195f, -8.985f);
    public Vector3 expensivePosition = new Vector3(0.499f, 1.195f, -8.985f);

    public int soundID = 2;

    // Where the item is currently moving to
    public int DisplayPosition;

    // Reaction time for UI (doubleclick etc.)
    public float ButtonReactionTime = 0.1f;

    // Where does time item hover on
    private bool Display1Hover = false;
    private bool Display2Hover = false;
    private bool Display3Hover = false;

    private bool aboutToMove = false;
    private bool aboutToClear = false;

    private bool ClearTimer = false;
    private float ClearDelay;

    private bool mouseButtonGoingOn = false;

    private float MouseTimer;
 
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        DisplayPosition = 0;

        originalScale = transform.localScale;
        dragScale = originalScale + new Vector3(0.1f, 0.1f, 0.1f);

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
                    mouseButtonGoingOn = true;
                    MouseTimer = 0;
                }
            }
        }

        // Update Mouse timer
        if (mouseButtonGoingOn)
        {
            MouseTimer += Time.deltaTime;
        }

        if (mouseButtonGoingOn && MouseTimer > ButtonReactionTime)
        {
            transform.localScale = dragScale;
        }

        // If mouse Down is followed by mouse Up
        if (mouseButtonGoingOn && Input.GetMouseButtonUp(0))
        {
            SortDisplayPosition();
            // print(DisplayPosition);
            if (DisplayPosition == 0)
            {
                aboutToClear = true;
            }

            // If it was a fast click, return the item position
            if (MouseTimer <= ButtonReactionTime)
            {
                aboutToClear = true;
            }


            aboutToMove = true;
            mouseButtonGoingOn = false;

            
        }

        // Clear, set etc. - we do it here because frame update and physics don't run on the same cycle
        if (aboutToMove)
        {
            if (aboutToClear)
            {
                // print("On a clearing mission!");
                transform.position = originalPosition;
                DisplayPosition = 0;
                ClearDisplayStatus();
                aboutToClear = false;
                Display1Hover = false;
                Display2Hover = false;
                Display3Hover = false;
            }
            else
            {
                SetDisplayStatus(DisplayPosition);
                PlaceItemOnDisplay(DisplayPosition);
                ShopAudioEngine.Instance.PlaySoundFX(soundID);
            }

            aboutToMove = false;

            ClearTimer = true;
            ClearDelay = 0;
        }

        if (ClearTimer)
        {
            transform.localScale = originalScale;

            ClearDelay += Time.deltaTime;

            if (ClearDelay > ButtonReactionTime)
            {
                ClearTimer = false;
            }
        }
    }

    void PlaceItemOnDisplay(int DisplayPosition)
    {
        switch (DisplayPosition)
        {
            case 0:
                transform.position = originalPosition;
                break;
            case 1:
                transform.position = cheapPosition;
                SetDisplayStatus(1);
                Display1Hover = true;
                Display2Hover = false;
                Display3Hover = false;
                break;
            case 2:
                transform.position = normalPosition;
                SetDisplayStatus(2);
                Display1Hover = false;
                Display2Hover = true;
                Display3Hover = false;
                break;
            case 3:
                transform.position = expensivePosition;
                SetDisplayStatus(3);
                Display1Hover = false;
                Display2Hover = false;
                Display3Hover = true;
                break;
            default:
                transform.position = originalPosition;
                break;
        }
        ClearDisplayStatus();
    }


    void SortDisplayPosition()
    {
        DisplayPosition = 0;

        if (Display1Hover) DisplayPosition = 1;
        if (Display3Hover) DisplayPosition = 3;
        if (Display2Hover) DisplayPosition = 2;

    }

    // Don't do physics if we are about do an action
    bool CheckIfWeCanDoPhysics()
    {
        return !ClearTimer;
    }


    void HoverItemOverDisplay(Collider collider)
    {
        if (CheckIfWeCanDoPhysics())
        {
            if (collider.transform.name == "CheapCollider")
            {
                if (CheckDisplayStatus(1)) Display1Hover = true; // DisplayPosition = 1;
            }

            if (collider.transform.name == "NormalCollider")
            {
                if (CheckDisplayStatus(2)) Display2Hover = true; // DisplayPosition = 2;
            }

            if (collider.transform.name == "ExpensiveCollider")
            {
                if (CheckDisplayStatus(3)) Display3Hover = true; // DisplayPosition = 3;
            }
        }
    }



    void OnTriggerEnter(Collider collider)
    {
        HoverItemOverDisplay(collider);
    }

    void OnTriggerExit(Collider collider)
    {
        // Don't do physics if we are about do an action
        if (CheckIfWeCanDoPhysics())
        {

            if (collider.transform.name == "CheapCollider") // && DisplayPosition == 1)
            {
                Display1Hover = false;
                //DisplayPosition = 0;
            }

            if (collider.transform.name == "NormalCollider") // && DisplayPosition == 2)
            {
                Display2Hover = false;
                //DisplayPosition = 0;
            }

            if (collider.transform.name == "ExpensiveCollider") // && DisplayPosition == 3)
            {
                Display3Hover = false;
                //DisplayPosition = 0;
            }
        }

    }

    void OnTriggerStay(Collider collider)
    {
//        if (DisplayPosition == 0)
//        {
            HoverItemOverDisplay(collider);
//        }
    }

    bool CheckDisplayStatus(int DisplayPosition)
    {
        switch (DisplayPosition)
        {
            case 1: if (ItemInventory.Instance.DisplayObject1 == null || ItemInventory.Instance.DisplayObject1 == transform.gameObject) return true; else return false; 
            case 2: if (ItemInventory.Instance.DisplayObject2 == null || ItemInventory.Instance.DisplayObject2 == transform.gameObject) return true; else return false; 
            case 3: if (ItemInventory.Instance.DisplayObject3 == null || ItemInventory.Instance.DisplayObject3 == transform.gameObject) return true; else return false; 
            default: return false;
        }
    }

    void SetDisplayStatus(int DisplayPosition)
    {
        // print(DisplayPosition);

        switch (DisplayPosition)
        {
            case 1: ItemInventory.Instance.DisplayObject1 = transform.gameObject; break;
            case 2: ItemInventory.Instance.DisplayObject2 = transform.gameObject; break;
            case 3: ItemInventory.Instance.DisplayObject3 = transform.gameObject; break;
            default: return;
        }
    }

    void ClearDisplayStatus()
    {
        // If it was placed on any of the displays, remove from there

        if (DisplayPosition != 1 && ItemInventory.Instance.DisplayObject1 == transform.gameObject) ItemInventory.Instance.DisplayObject1 = null;
        if (DisplayPosition != 2 && ItemInventory.Instance.DisplayObject2 == transform.gameObject) ItemInventory.Instance.DisplayObject2 = null;
        if (DisplayPosition != 3 && ItemInventory.Instance.DisplayObject3 == transform.gameObject) ItemInventory.Instance.DisplayObject3 = null;

    }

}
