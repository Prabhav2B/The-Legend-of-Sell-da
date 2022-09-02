using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public Enums.ItemTypes itemType;

    public int valueForAdventurer;
    public int valueForPrincess;
    public int valueForEvil;
}
