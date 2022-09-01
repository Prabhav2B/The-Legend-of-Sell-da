using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MerchantBehavior : CharacterBehavior
{
    [SerializeField] private List<CodedDialogue> merchantCodedDialogues;

    [SerializeField] private GlobalVariableManager _globalVariableManager;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private GameSequenceManager _sequenceManager;
    
    private Dictionary<string, DialogueSequenceSO> dialogueDictionary;

    private bool day1Win, day2Win, day3Win;
    
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

        _sequenceManager = FindObjectOfType<GameSequenceManager>();

        dialogueDictionary = DialogueUtility.ConvertToDictionary(merchantCodedDialogues);
        day1Win = false;
        day2Win = false;
        day3Win = false;

    }

    public override void InitiateCharacterSequence()
    {
        base.InitiateCharacterSequence();
        GreetingDialogue();
    }

    public override void ActionsLeft()
    {
        _sequenceManager.WaitForNextEvent();
    }


    protected override void GreetingDialogue()
    {
        base.GreetingDialogue();
        switch (_globalVariableManager.CurrentDay)
        {
            case Enums.Days.Day1:
                _dialogueSystem.StartDialogue(dialogueDictionary["ML1"], Enums.Characters.merchant);
                break;
            case Enums.Days.Day2:
                DialogueSequenceSO res;
                if (_globalVariableManager.AdventurerWinTally[0] && _globalVariableManager.PrincessWinTally[0])
                {
                    res = dialogueDictionary["ML2G"];
                }
                else
                {
                    res = dialogueDictionary["ML2B"];
                }
                _dialogueSystem.StartDialogue(res,  Enums.Characters.merchant);
                break;
            case Enums.Days.Day3:

                day1Win = _globalVariableManager.AdventurerWinTally[0] &&
                                   _globalVariableManager.PrincessWinTally[0];
                
                day2Win = _globalVariableManager.AdventurerWinTally[1] &&
                                   _globalVariableManager.PrincessWinTally[1];
                
                if (day1Win && day2Win)
                {
                    res = dialogueDictionary["ML3G"];
                }
                else
                {
                    if (!day1Win && !day2Win)
                    {
                        res = dialogueDictionary["ML3B"];
                    }

                    else
                    {
                        res = dialogueDictionary["ML3N"];
                    }
                }
                _dialogueSystem.StartDialogue(res, Enums.Characters.merchant);
                break;
            case Enums.Days.EndDay:
                
                day1Win = _globalVariableManager.AdventurerWinTally[0] &&
                               _globalVariableManager.PrincessWinTally[0];
                
                day2Win = _globalVariableManager.AdventurerWinTally[1] &&
                               _globalVariableManager.PrincessWinTally[1];
                
                day3Win = _globalVariableManager.AdventurerWinTally[2] &&
                          _globalVariableManager.PrincessWinTally[2];
                
                
                if (day1Win && day2Win && day3Win)
                {
                    res = dialogueDictionary["BE"];
                }
                else 
                {
                    if (_globalVariableManager.PrincessPoints >= _globalVariableManager.EvilDoodPoints)
                        res = dialogueDictionary["PWE"];
                    else
                    {
                        if (_globalVariableManager.AdventurerPoints >= _globalVariableManager.EvilDoodPoints)
                            res = dialogueDictionary["HWE"];
                        else
                        {
                            res = dialogueDictionary["FKWE"];
                        }
                    }
                }
                _dialogueSystem.StartDialogue(res, Enums.Characters.merchant);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    void Update()
    {
        
    }
}
