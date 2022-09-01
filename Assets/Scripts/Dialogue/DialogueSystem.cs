using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textComponent;
    [SerializeField]private TextMeshProUGUI textComponentDescription;
    private string[] lines;
    
    [Range(.1f, 100f)]
    [SerializeField] private float textSpeed;

    private int index;
    private Enums.Characters _character;

    public float TextSpeed => 1f / textSpeed;

    [SerializeField] private MerchantBehavior _merchant;
    [SerializeField] private AdventurerBehavior _adventurer;
    [SerializeField] private PrincessBehavior _princess;
    [SerializeField] private EvilAssDoodBehavior _evilAssDood;
    
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
    }

    private void Start()
    {
        //StartDialogue();
    }


    public void OnDialogueAction(InputAction.CallbackContext context)
    {
        if(!context.performed)
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
        StartCoroutine(TypeLine());
        
    }
    
    public void StartDialogue(DialogueSequenceSO dialogueSequence, Enums.Characters character)
    {
        _character = character;
        
        gameObject.SetActive(true);
        textComponent.text = String.Empty;
        lines = dialogueSequence.DialogueLines;
        index = 0;
        StartCoroutine(TypeLine());
        
    }

    public void SetItemDescription(ItemSO item)
    {
        textComponent.text = item.description;
    }
    
    public void ClearItemDescription()
    {
        textComponent.text = String.Empty;
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

}
