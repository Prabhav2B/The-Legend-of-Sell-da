using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableManager : MonoBehaviour
{
    private Enums.Days _currentDay;

    private int _adventurerPoints;
    private int _princessPoints;
    private int _evilDoodPoints;

    public int AdventurerPoints => _adventurerPoints;
    public int PrincessPoints => _princessPoints;
    public int EvilDoodPoints => _evilDoodPoints;

    public Enums.Days CurrentDay => _currentDay;

    void Start()
    {
        _adventurerPoints = 0;
        _princessPoints = 0;
        _evilDoodPoints = 0;
        _currentDay = Enums.Days.Day1;
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

    public void IncrementDay()
    {
        switch (_currentDay)
        {
            case Enums.Days.Day1:
                _currentDay = Enums.Days.Day2;
                break;
            case Enums.Days.Day2:
                _currentDay = Enums.Days.Day3;
                break;
            case Enums.Days.Day3:
                _currentDay = Enums.Days.EndDay;
                break;
            case Enums.Days.EndDay:
                throw new Exception("Already on Last Day!");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}