using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameSequenceManager : MonoBehaviour
{

    [SerializeField] private WorldEventSO[] WorldEvents;
    [SerializeField] private DialogueSystem _dialogueSystem;

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
    }

    private void Start()
    {
        worldEventIndex = 0;
        currentWorldEvent = null;
        ExecuteNextWorldEvent();
        
    }

    public void StartFirstDialogue()
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

        switch (currentWorldEvent.WorldEvent)  
        {
            case Enums.WorldEvents.dayStart:
                _screenFadeManager.ScreenFadeOutWorldEvent((currentWorldEvent as DayStartSO)?.dayStartText);
                break;
            case Enums.WorldEvents.characterApproach:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    IEnumerator Waiter(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
    }
}
