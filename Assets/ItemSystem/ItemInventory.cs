using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public GameObject DisplayObject1;
    public GameObject DisplayObject2;
    public GameObject DisplayObject3;  

    public static ItemInventory Instance;

    public GameObject[] UnderTheShelfPreFabs;
    public Vector3[] UnderTheSelfLocations;

    public GameObject[] FullObjectInventory;

    public int SpwanAmount = 8;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn items under the shelf
    void SpawnShopItems()
    {
//        if (UnderTheSelfLocations.Length < SpawnAmount)
        {
            print("Setup locations for items!");
            return;
        }

//        for (int i = 0; i < SpawnAmount; i++ )
//        {
//            UnderTheShelfPreFabs[i] = FullObjectInventory[i];
//            UnderTheShelfPreFabs[i].transform = UnderTheSelfLocations[i];
//        }
    }

    public bool InventoryFull()
    {
        return DisplayObject1!=null && DisplayObject2!=null && DisplayObject3!=null;
    }
}
