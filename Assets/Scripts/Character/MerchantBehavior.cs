using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBehavior : CharacterBehavior
{
    [SerializeField] private List<CodedDialogue> merchantCodedDialogues;

    [SerializeField] private GlobalVariableManager _globalVariableManager;
    [SerializeField] private DialogueSystem _dialogueSystem;

    private Dictionary<string, DialogueSequenceSO> dialogueDictionary;
    
    private void Awake()
    {
        if (_globalVariableManager == null)
        {
            _globalVariableManager = FindObjectOfType<GlobalVariableManager>();
        }

        if (_dialogueSystem == null)
        {
            _dialogueSystem = FindObjectOfType<DialogueSystem>();
        }

        dialogueDictionary = DialogueUtility.ConvertToDictionary(merchantCodedDialogues);

    }

    public override void InitiateCharacterSequence()
    {
        base.InitiateCharacterSequence();
        StandardDialogue();
    }

    public override void StandardDialogue()
    {
        base.StandardDialogue();
        switch (_globalVariableManager.CurrentDay)
        {
            case Enums.Days.Day1:
                _dialogueSystem.StartDialogue(dialogueDictionary["ML1"]);
                break;
            case Enums.Days.Day2:
                break;
            case Enums.Days.Day3:
                break;
            case Enums.Days.EndDay:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    void Update()
    {
        
    }
}
