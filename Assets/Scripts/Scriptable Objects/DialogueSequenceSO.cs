using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueSequence", order = 1)]
public class DialogueSequenceSO : ScriptableObject
{
    public string[] DialogueLines;
}
