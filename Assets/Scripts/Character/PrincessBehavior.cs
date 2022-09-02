using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessBehavior : CharacterBehavior
{
    [SerializeField] private GameObject _princess;
    
    [SerializeField] private List<CodedDialogue> merchantCodedDialogues;
    [SerializeField] private GlobalVariableManager _globalVariableManager;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private GameSequenceManager _sequenceManager;
    [SerializeField] private ItemPlacement _itemPlacement;
    
    private Dictionary<string, DialogueSequenceSO> dialogueDictionary;
    private Animator anim;

    private Enums.ItemTypes[] wants;

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

        anim = _princess.GetComponent<Animator>();
        
        
        wants = new Enums.ItemTypes[] {Enums.ItemTypes.ocarina, Enums.ItemTypes.bomb_arrows, Enums.ItemTypes.forest_dweller_shield, Enums.ItemTypes.forest_dweller_bow, Enums.ItemTypes.bomb};

    }
    
    
    public override void InitiateCharacterSequence()
    {
        _globalVariableManager.CurrentCharacter = Enums.Characters.princess;
        Approach();
    }

    public override void Approach()
    {
        currentEvent = Enums.CharacterEvent.Entry;
        _princess.SetActive(true);

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
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
    protected override void GreetingDialogue()
    {
        base.GreetingDialogue();
        switch (_globalVariableManager.CurrentDay)
        {
            case Enums.Days.Day1:
                _dialogueSystem.StartDialogue(dialogueDictionary["PD1GR"], Enums.Characters.princess);
                break;
            case Enums.Days.Day2:
                DialogueSequenceSO res;
                if (_globalVariableManager.PrincessPersonalTally[0])
                {
                    res = dialogueDictionary["PD2GGR"];
                }
                else if(!_globalVariableManager.EvilPersonalTally[0])
                {
                    res = dialogueDictionary["PD2NDR"];
                }
                else
                {
                    res = dialogueDictionary["PD2BGR"];
                }
                _dialogueSystem.StartDialogue(res, Enums.Characters.princess);
                break;
            case Enums.Days.Day3:
                if (_globalVariableManager.PrincessPersonalTally[0])
                {
                    if(_globalVariableManager.PrincessPersonalTally[1])
                        res = dialogueDictionary["PD3GR++"];
                    else
                        res = dialogueDictionary["PD3GR+-"];
                }
                else if(_globalVariableManager.AdventurerPersonalTally[0])
                {
                    if (_globalVariableManager.PrincessPersonalTally[1])
                    {
                        res = dialogueDictionary["PD3GR-+"];
                    }
                    else
                    {
                        if(!_globalVariableManager.EvilPersonalTally[1])
                            res = dialogueDictionary["PD3GR+-"];
                        else
                            res = dialogueDictionary["PD3GR++"];
                    }
                }
                else
                {
                    if(_globalVariableManager.PrincessPersonalTally[1])
                        res = dialogueDictionary["PD3GR-+"];
                    else
                        res = dialogueDictionary["PD3GR--"];
                }
                _dialogueSystem.StartDialogue(res, Enums.Characters.princess);
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
