using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera itemPlacementCamera;
    [SerializeField] private DialogueSystem _dialogueSystem;

    private void Awake()
    {
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    public void StartItemPlacement()
    {
        itemPlacementCamera.gameObject.SetActive(true);
        _dialogueSystem.Activate();
    }
}
