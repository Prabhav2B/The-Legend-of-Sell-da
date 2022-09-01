using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WorldEvent/CharacterApproach", order = 1)]
public class CharacterSO : WorldEventSO
{
    public Enums.Characters character;
}
