using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    public DialogueSystem _dialogue;

    private void Start()
    {
        _dialogue = FindObjectOfType<DialogueSystem>();
    }
    
}
