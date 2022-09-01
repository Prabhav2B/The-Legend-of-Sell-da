using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums 
{
    public enum ItemTypes
    {
        bomb_arrows,
        forest_dweller_shield,
        forest_dweller_bow,
        forest_dweller_sword,
        fairy,
        hylian_shield,
        phantom_cape,
        phantom_helmet,
        sheik_mask,
        ocarina,
        majora_mask
    }

    public enum Characters
    {
        adventurer,
        princess,
        evilassdood,
        merchant
    }

    public enum WorldEvents
    {
        dayStart,
        characterApproach,
        dayEnd
    }
    
    public enum Days
    {
        Day1,
        Day2,
        Day3,
        EndDay
    }

    public enum Expressions
    {
        curious,
        frown,
        happy,
        super_frown,
        super_happy,
        furious,
        smile
        
    }


}
