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

    public float TextSpeed => 1f / textSpeed;

    private void Awake()
    {
    //    gameObject.SetActive(false);
        textComponent.text = String.Empty;
        textComponentDescription.text = String.Empty;
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
        StartCoroutine(TypeLine());
        index = 0;
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
        }
    }

}
