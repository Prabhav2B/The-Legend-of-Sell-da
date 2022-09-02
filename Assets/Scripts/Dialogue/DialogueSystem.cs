using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI textComponentDescription;
    private string[] lines;

    [Range(.1f, 100f)] [SerializeField] private float textSpeed;

    private int index;
    private Enums.Characters _character;

    public float TextSpeed => 1f / textSpeed;

    [SerializeField] private MerchantBehavior _merchant;
    [SerializeField] private AdventurerBehavior _adventurer;
    [SerializeField] private PrincessBehavior _princess;
    [SerializeField] private EvilAssDoodBehavior _evilAssDood;

    [SerializeField] private Button sellAtOriginal;
    [SerializeField] private Button sellForHaggle;
    [SerializeField] private Button refuseSale;

    private bool inputActive;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        textComponent.text = String.Empty;
        textComponentDescription.text = String.Empty;
        _character = Enums.Characters._;

        _merchant = FindObjectOfType<MerchantBehavior>();
        _adventurer = FindObjectOfType<AdventurerBehavior>();
        _princess = FindObjectOfType<PrincessBehavior>();
        _evilAssDood = FindObjectOfType<EvilAssDoodBehavior>();

        inputActive = true;
        
        sellAtOriginal.gameObject.SetActive(false);
        sellForHaggle.gameObject.SetActive(false);
        refuseSale.gameObject.SetActive(false);

    }

    public void Activate()
    {
        textComponent.text = String.Empty;
        textComponentDescription.text = String.Empty;
        inputActive = false;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }


    public void OnDialogueAction(InputAction.CallbackContext context)
    {
        if (!context.performed || !inputActive)
            return;

        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    private void StartDialogue()
    {
        StartCoroutine(TypeLine());
        index = 0;
    }

    public void StartDialogue(DialogueSequenceSO dialogueSequence)
    {
        gameObject.SetActive(true);
        textComponent.text = String.Empty;
        lines = dialogueSequence.DialogueLines;
        index = 0;
        inputActive = true;
        StartCoroutine(TypeLine());
        
    }

    public void StartDialogue(DialogueSequenceSO dialogueSequence, Enums.Characters character)
    {
        _character = character;

        gameObject.SetActive(true);
        textComponent.text = String.Empty;
        lines = dialogueSequence.DialogueLines;
        index = 0;
        inputActive = true;
        StartCoroutine(TypeLine());

        SetCharacterFaceExpression(dialogueSequence, character);    // Character face expression function    
    }
    
    public void StartSelectionDialogue(DialogueSequenceSO dialogueSequence, Enums.Characters character)
    {
        _character = character;

        gameObject.SetActive(true);
        textComponent.text = String.Empty;
        lines = dialogueSequence.DialogueLines;
        index = 0;
        inputActive = true;

        StartCoroutine(TypeLine());
    }

    
    public void StartTransactionDialogue(DialogueSequenceSO dialogueSequence, Enums.Characters character, bool allowHaggle)
    {
        _character = character;

        gameObject.SetActive(true);
        textComponent.text = String.Empty;
        lines = dialogueSequence.DialogueLines;
        index = 0;
        inputActive = false;
        
        sellAtOriginal.gameObject.SetActive(true);
        sellForHaggle.gameObject.SetActive(allowHaggle);
        refuseSale.gameObject.SetActive(true);
        
        StartCoroutine(TypeLine());

        SetCharacterFaceExpression(dialogueSequence, character);    // Character face expression function    
    }

    public void SetItemDescription(ItemSO item)
    {
        textComponentDescription.text = item.description;
    }

    public void ClearItemDescription()
    {
        textComponentDescription.text = String.Empty;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            if (_character != Enums.Characters._)
            {
                switch (_character)
                {
                    case Enums.Characters.adventurer:
                        _adventurer.ActionsLeft();
                        break;
                    case Enums.Characters.princess:
                        _princess.ActionsLeft();
                        break;
                    case Enums.Characters.evilassdood:
                        _evilAssDood.ActionsLeft();
                        break;
                    case Enums.Characters.merchant:
                        _merchant.ActionsLeft();
                        break;
                    case Enums.Characters._:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _character = Enums.Characters._;
            }
        }
    }


    // Face expression
    // Most certainly not to the proper way to find the right gameobjects, but hopefully will work
    void SetCharacterFaceExpression(DialogueSequenceSO dialogueSequence, Enums.Characters character)
    {
        GameObject CharacterObject;

        if (character == Enums.Characters.adventurer)
        {
            CharacterObject = GameObject.Find("Hero");
            CharacterExpressions expressionScript = CharacterObject.GetComponentInChildren<CharacterExpressions>();
            expressionScript.UpdateExpression(dialogueSequence);

        }
        if (character == Enums.Characters.princess)
        {
            CharacterObject = GameObject.Find("Princess");
            CharacterExpressions expressionScript = CharacterObject.GetComponentInChildren<CharacterExpressions>();
            expressionScript.UpdateExpression(dialogueSequence);

        }
        if (character == Enums.Characters.evilassdood)
        {
            CharacterObject = GameObject.Find("FalseKing");
            CharacterExpressions expressionScript = CharacterObject.GetComponentInChildren<CharacterExpressions>();
            expressionScript.UpdateExpression(dialogueSequence);

        }

    }
}