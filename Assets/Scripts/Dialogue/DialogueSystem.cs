using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    
    [Range(.1f, 100f)]
    [SerializeField] private float textSpeed;

    private int index;

    public float TextSpeed => 1f / textSpeed;
    
    private void Start()
    {
        textComponent.text = String.Empty;
        StartDialogue();
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
