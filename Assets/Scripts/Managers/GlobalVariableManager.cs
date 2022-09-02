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

    private bool[] adventurerWinTally;
    private bool[] princessWinTally;
    private bool[] evilDoodWinTally;
    
    private bool[] adventurerPersonalTally;
    private bool[] princessPersonalTally;
    private bool[] evilDoodPersonalTally;

    public int AdventurerPoints => _adventurerPoints;
    public int PrincessPoints => _princessPoints;
    public int EvilDoodPoints => _evilDoodPoints;

    public bool[] AdventurerWinTally => adventurerWinTally;
    public bool[] PrincessWinTally => princessWinTally;
    public bool[] EvilWinTally => evilDoodWinTally;
    

    public bool[] AdventurerPersonalTally => adventurerPersonalTally;
    public bool[] PrincessPersonalTally => princessPersonalTally;
    public bool[] EvilPersonalTally => evilDoodPersonalTally;
    
    
    public Enums.Days CurrentDay => _currentDay;

    public Enums.Characters CurrentCharacter { get; set; }
    
    [SerializeField] private MerchantBehavior _merchant;
    [SerializeField] private AdventurerBehavior _adventurer;
    [SerializeField] private PrincessBehavior _princess;
    [SerializeField] private EvilAssDoodBehavior _evilAssDood;

    void Start()
    {
        _adventurerPoints = 0;
        _princessPoints = 0;
        _evilDoodPoints = 0;
        _currentDay = Enums.Days.Day1;

        adventurerWinTally = new bool[3];
        princessWinTally = new bool[3];
        evilDoodWinTally = new bool[3];
        
        adventurerPersonalTally = new bool[3];
        princessPersonalTally = new bool[3];
        evilDoodPersonalTally = new bool[3];

        CurrentCharacter = Enums.Characters._;
        
        _merchant = FindObjectOfType<MerchantBehavior>();
        _adventurer = FindObjectOfType<AdventurerBehavior>();
        _princess = FindObjectOfType<PrincessBehavior>();
        _evilAssDood = FindObjectOfType<EvilAssDoodBehavior>();
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

    public void CalculateDayWin()
    {
        switch (_currentDay)
        {
            case Enums.Days.Day1:
                adventurerWinTally[0] = _adventurerPoints + _princessPoints >= _evilDoodPoints;
                princessWinTally[0] = _adventurerPoints + _princessPoints >= _evilDoodPoints;
                evilDoodWinTally[0] = _adventurerPoints + _princessPoints < _evilDoodPoints;
                break;
            case Enums.Days.Day2:
                
                adventurerWinTally[1] = _adventurerPoints + _princessPoints >= _evilDoodPoints;
                princessWinTally[1] = _adventurerPoints + _princessPoints >= _evilDoodPoints;
                evilDoodWinTally[1] = _adventurerPoints + _princessPoints < _evilDoodPoints;
                break;
            case Enums.Days.Day3:
                adventurerWinTally[2] = _adventurerPoints >= _evilDoodPoints;
                princessWinTally[2] = _princessPoints >= _evilDoodPoints;
                evilDoodWinTally[2] = _adventurerPoints + _princessPoints < _evilDoodPoints;
                break;
            case Enums.Days.EndDay:
                throw new Exception("Already on Last Day!");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void CalculatePersonalTally()
    {
        switch (_currentDay)
        {
            case Enums.Days.Day1:
                adventurerPersonalTally[0] = _adventurerPoints >= _evilDoodPoints;
                princessPersonalTally[0] =  _princessPoints >= _evilDoodPoints;
                evilDoodPersonalTally[0] = _adventurerPoints + _princessPoints < _evilDoodPoints;
                break;
            case Enums.Days.Day2:
                
                adventurerPersonalTally[1] = _adventurerPoints  >= _evilDoodPoints;
                princessPersonalTally[1] =  _princessPoints >= _evilDoodPoints;
                evilDoodPersonalTally[1] = _adventurerPoints + _princessPoints < _evilDoodPoints;
                break;
            case Enums.Days.Day3:
                adventurerPersonalTally[2] = _adventurerPoints >= _evilDoodPoints;
                princessPersonalTally[2] = _princessPoints >= _evilDoodPoints;
                evilDoodPersonalTally[2] = _adventurerPoints + _princessPoints < _evilDoodPoints;
                break;
            case Enums.Days.EndDay:
                throw new Exception("Already on Last Day!");
            default:
                throw new ArgumentOutOfRangeException();
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

    public void NextCharacterAction()
    {
        switch (CurrentCharacter)
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

        
    }
}