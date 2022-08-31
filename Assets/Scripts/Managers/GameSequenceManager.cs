using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameSequenceManager : MonoBehaviour
{

    [SerializeField] private DialogueSequenceSO beedleDay1Dialogue;
    [SerializeField] private DialogueSystem _dialogueSystem;

    private void Awake()
    {
        if (_dialogueSystem == null)
            throw new Exception("Reference Missing");
    }

    public void StartFirstDialogue()
    {
        StartCoroutine(Waiter(5f));
        _dialogueSystem.StartDialogue(beedleDay1Dialogue);
    }

    IEnumerator Waiter(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
    }
}
