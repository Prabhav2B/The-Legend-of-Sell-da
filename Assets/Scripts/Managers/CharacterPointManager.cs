using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPointManager : MonoBehaviour
{

    private int _adventurerPoints;
    private int _princessPoints;
    private int _evilDoodPoints;

    public int AdventurerPoints => _adventurerPoints;
    public int PrincessPoints => _princessPoints;
    public int EvilDoodPoints => _evilDoodPoints;
    
    void Start()
    {
        _adventurerPoints = 0;
        _princessPoints = 0;
        _evilDoodPoints = 0;
    }

    public void UpdatePoints(Enums.Characters character, int points)
    {
        switch (character)
        {
            case Enums.Characters.adventurer:
                _adventurerPoints += points;
                break;
            case Enums.Characters.princess:
                _princessPoints += points;
                break;
            case Enums.Characters.evilassdood:
                _evilDoodPoints += points;
                break;
            case Enums.Characters.merchant:
                throw new Exception("Merchant Should not have Character points");
                //break;
            default:
                throw new ArgumentOutOfRangeException(nameof(character), character, null);
        }
    }


}
