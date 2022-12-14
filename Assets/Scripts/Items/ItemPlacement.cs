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
        _dialogueSystem = FindObjectOfType<DialogueSystem>(true);
        _offerButton = FindObjectOfType<OfferButton>(true);
    }

    public void StartItemPlacement()
    {
        itemPlacementCamera.gameObject.SetActive(true);
        _dialogueSystem.Activate();
        _offerButton.ActivateButton();
    }

    public void EndItemPlacement()
    {
        itemPlacementCamera.gameObject.SetActive(false);
    }
}
