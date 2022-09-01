using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameSequenceManager : MonoBehaviour
{

    [SerializeField] private WorldEventSO[] WorldEvents;
    [SerializeField] private DialogueSystem _dialogueSystem;

    [SerializeField] private MerchantBehavior _merchant;
    [SerializeField] private AdventurerBehavior _adventurer;
    [SerializeField] private PrincessBehavior _princess;
    [SerializeField] private EvilAssDoodBehavior _evilAssDood;

    private int worldEventIndex;
    private WorldEventSO currentWorldEvent;

    [SerializeField] private ScreenFadeManager _screenFadeManager;
    
    private void Awake()
    {
        if (_dialogueSystem == null)
            throw new Exception("Reference Missing");

        if (_screenFadeManager == null)
        {
            throw new Exception("Reference to Screen Fade Manager missing in Game Sequence Manager");
        }
        
        _merchant = FindObjectOfType<MerchantBehavior>();
        _adventurer = FindObjectOfType<AdventurerBehavior>();
        _princess = FindObjectOfType<PrincessBehavior>();
        _evilAssDood = FindObjectOfType<EvilAssDoodBehavior>();

    }

    private void Start()
    {
        worldEventIndex = 0;
        currentWorldEvent = null;
        ExecuteNextWorldEvent();
        
    }

    public void WaitForNextEvent()
    {
        StartCoroutine(Waiter(7f));
    }

    public void ExecuteNextWorldEvent()
    {
        if (worldEventIndex >= WorldEvents.Length - 1)
        {
            Debug.Log("Game Over");
        }

        currentWorldEvent = WorldEvents[worldEventIndex];
        worldEventIndex++;
        
        Debug.Log(worldEventIndex);

        switch (currentWorldEvent.WorldEvent)  
        {
            case Enums.WorldEvents.dayStart:
                _screenFadeManager.ScreenFadeOutWorldEvent((currentWorldEvent as DayStartSO)?.dayStartText);
                break;
            case Enums.WorldEvents.characterApproach:
                StartCoroutine(Waiter(7f));
                switch ((currentWorldEvent as CharacterSO)?.character)
                {
                    case Enums.Characters.adventurer:
                        _adventurer.InitiateCharacterSequence();
                        break;
                    case Enums.Characters.princess:
                        _princess.InitiateCharacterSequence();
                        break;
                    case Enums.Characters.evilassdood:
                        _evilAssDood.InitiateCharacterSequence();
                        break;
                    case Enums.Characters.merchant:
                        _merchant.InitiateCharacterSequence();
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    IEnumerator Waiter(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        ExecuteNextWorldEvent();
    }
}
