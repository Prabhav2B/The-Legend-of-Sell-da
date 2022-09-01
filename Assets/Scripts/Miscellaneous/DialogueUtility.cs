using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueUtility 
{
    
    public static Dictionary<string, DialogueSequenceSO> ConvertToDictionary(List<CodedDialogue> codedDialogues)
    {
        var library = new Dictionary<string, DialogueSequenceSO>();

        foreach (var dialogue in codedDialogues)
        {
            library.Add(dialogue.code, dialogue.dialogueSequence);
        }

        return library;

    }
}
