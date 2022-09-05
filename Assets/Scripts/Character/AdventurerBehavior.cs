using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerBehavior : CharacterBehavior
{
    [SerializeField] private GameObject adventurer;

    [SerializeField] private List<CodedDialogue>
        merchantCodedDialogues; //I fucked up and I'm not gonna risk losing references by renaming

    [SerializeField] private GlobalVariableManager _globalVariableManager;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private GameSequenceManager _sequenceManager;
    [SerializeField] private ItemPlacement _itemPlacement;
    [SerializeField] private ItemInventory _itemInventory;

    private Dictionary<string, DialogueSequenceSO> dialogueDictionary;
    private Animator anim;

    private Enums.ItemTypes[] wants;

    private int index = 0;

    private bool locker;
    

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
        _itemInventory = FindObjectOfType<ItemInventory>();

        dialogueDictionary = DialogueUtility.ConvertToDictionary(merchantCodedDialogues);

        anim = adventurer.GetComponent<Animator>();

        wants = new Enums.ItemTypes[]
        {
            Enums.ItemTypes.fairy, Enums.ItemTypes.bomb_arrows, Enums.ItemTypes.forest_dweller_shield,
            Enums.ItemTypes.forest_dweller_bow, Enums.ItemTypes.bomb
        };

        currentMoney = 10;
        locker = false;
    }


    public override void InitiateCharacterSequence()
    {
        _globalVariableManager.CurrentCharacter = Enums.Characters.adventurer;
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
                ExecuteBuyBehaviorOne();
                break;
            case Enums.CharacterEvent.Buying:
                currentEvent = Enums.CharacterEvent.Offering;
                ExecuteBuyBehaviorTwo();
                break;
            case Enums.CharacterEvent.Offering:
                currentEvent = Enums.CharacterEvent.Exiting;
                CharacterExit();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CharacterExit()
    {
        anim.SetBool("done", true);
        
        
        StartCoroutine(WaitForExitAnimationComplete());
    }

    

    private void ExecuteBuyBehaviorTwo()
    {
        int cost = (index + 1) * 2;

        if (currentMoney >= cost)
        {
            _dialogueSystem.StartTransactionDialogue(dialogueDictionary["HH"], Enums.Characters.adventurer, true);
        }
    }

    private void ExecuteBuyBehaviorOne()
    {
        locker = false;
        _itemPlacement.EndItemPlacement();
        index = 0;
        Enums.ItemTypes selectedItem = Enums.ItemTypes.sheik_mask;
        var items = _itemInventory.ItemsOnSale();


        for (int i = 0; i < wants.Length; i++)
        {
            index = 0;
            for (int j = 0; j < items.Length; j++)
            {
                if (wants[i] == items[j] && !locker)
                {
                    locker = true;
                    selectedItem = items[j];
                    print(items[j]);
                    break;
                }

                index++;
            }
        }

        if (selectedItem == Enums.ItemTypes.sheik_mask)
        {
            index = 0;
            selectedItem = items[0];
        }

        _itemInventory.ElevateItem(index);

        _dialogueSystem.StartSelectionDialogue(dialogueDictionary["HC"], Enums.Characters.adventurer);
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
                else if (!_globalVariableManager.EvilPersonalTally[0])
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
                    if (_globalVariableManager.AdventurerPersonalTally[1])
                        res = dialogueDictionary["HSH"];
                    else
                        res = dialogueDictionary["HF"];
                }
                else if (_globalVariableManager.PrincessPersonalTally[0])
                {
                    if (_globalVariableManager.AdventurerPersonalTally[1])
                    {
                        res = dialogueDictionary["HH"];
                    }
                    else
                    {
                        if (!_globalVariableManager.EvilPersonalTally[1])
                            res = dialogueDictionary["HF"];
                        else
                            res = dialogueDictionary["HH"];
                    }
                }
                else
                {
                    if (_globalVariableManager.AdventurerPersonalTally[1])
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
    
    IEnumerator WaitForExitAnimationComplete()
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                _sequenceManager.ExecuteNextWorldEvent();
                break;
            }

            yield return new WaitForSeconds(1);
        }
    }
}