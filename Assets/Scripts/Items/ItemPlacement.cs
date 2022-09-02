using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera itemPlacementCamera;
    [SerializeField] private DialogueSystem _dialogueSystem;
    private OfferButton _offerButton;

    private void Awake()
    {
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
        _offerButton = FindObjectOfType<OfferButton>();
    }

    public void StartItemPlacement()
    {
        itemPlacementCamera.gameObject.SetActive(true);
        _dialogueSystem.Activate();
        _offerButton.ActivateButton();
    }
}
