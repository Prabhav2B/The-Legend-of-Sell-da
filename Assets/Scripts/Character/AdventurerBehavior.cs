using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerBehavior : CharacterBehavior
{
    [SerializeField] private GameObject adventurer;
    
    [SerializeField] private List<CodedDialogue> merchantCodedDialogues;//I fucked up and I'm not gonna risk losing references by renaming
    
    [SerializeField] private GlobalVariableManager _globalVariableManager;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private GameSequenceManager _sequenceManager;
    [SerializeField] private ItemPlacement _itemPlacement;
    
    private Dictionary<string, DialogueSequenceSO> dialogueDictionary;
    private Animator anim; 

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
        _itemPlacement = FindObjectOfType<ItemPlacement>();
        
        dialogueDictionary = DialogueUtility.ConvertToDictionary(merchantCodedDialogues);

        anim = adventurer.GetComponent<Animator>();

        currentMoney = 10;

    }
    
    
    public override void InitiateCharacterSequence()
    {
        _globalVariableManager.CurrentCharacter = Enums.Characters.evilassdood;
        Approach();
    }

    public override void Approach()
    {
        currentEvent = Enums.CharacterEvent.Entry;
        adventurer.SetActive(true);

        StartCoroutine(WaitForAnimationComplete());

    }
    
    public override void ActionsLeft()
    {
        switch (currentEvent)
        {
            case Enums.CharacterEvent.Entry:
                currentEvent = Enums.CharacterEvent.Greeting;
                GreetingDialogue();
                break;
            case Enums.CharacterEvent.Greeting:
                currentEvent = Enums.CharacterEvent.Selecting;
                _itemPlacement.StartItemPlacement();
                break;
            case Enums.CharacterEvent.Selecting:
                currentEvent = Enums.CharacterEvent.Buying;
                ExecuteBuyBehavior();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private void ExecuteBuyBehavior()
    {
        throw new NotImplementedException();
    }

    protected override void GreetingDialogue()
    {
        base.GreetingDialogue();
        switch (_globalVariableManager.CurrentDay)
        {
            case Enums.Days.Day1:
                _dialogueSystem.StartDialogue(dialogueDictionary["HH"], Enums.Characters.adventurer);
                break;
            case Enums.Days.Day2:
                DialogueSequenceSO res;
                if (_globalVariableManager.AdventurerPersonalTally[0])
                {
                    res = dialogueDictionary["HSH"];
                }
                else if(!_globalVariableManager.EvilPersonalTally[0])
                {
                    res = dialogueDictionary["HH"];
                }
                else
                {
                    res = dialogueDictionary["HF"];
                }
                _dialogueSystem.StartDialogue(res, Enums.Characters.adventurer);
                break;
            case Enums.Days.Day3:

                if (_globalVariableManager.AdventurerPersonalTally[0])
                {
                    if(_globalVariableManager.AdventurerPersonalTally[1])
                        res = dialogueDictionary["HSH"];
                    else
                        res = dialogueDictionary["HF"];
                }
                else if(_globalVariableManager.PrincessPersonalTally[0])
                {
                    if (_globalVariableManager.AdventurerPersonalTally[1])
                    {
                        res = dialogueDictionary["HH"];
                    }
                    else
                    {
                        if(!_globalVariableManager.EvilPersonalTally[1])
                            res = dialogueDictionary["HF"];
                        else
                            res = dialogueDictionary["HH"];
                    }
                }
                else
                {
                    if(_globalVariableManager.AdventurerPersonalTally[1])
                        res = dialogueDictionary["HH"];
                    else
                        res = dialogueDictionary["HF"];
                }
                _dialogueSystem.StartDialogue(res, Enums.Characters.adventurer);
                break;
            case Enums.Days.EndDay:
                throw new Exception("No Events for Last Day for Adventurer");
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    IEnumerator WaitForAnimationComplete()
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                ActionsLeft();
                break;
            }

            yield return new WaitForSeconds(1);
        }
        
    }
}
