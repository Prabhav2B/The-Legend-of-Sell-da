using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    protected int currentMoney;
    protected Enums.CharacterEvent currentEvent;

    void Start()
    {
        currentMoney = 0;
    }

    public virtual void InitiateCharacterSequence()
    {
        
    }

    public virtual void Approach()
    {
        
    }
    
    public virtual void ActionsLeft()
    {
        
    }

    protected virtual void GreetingDialogue()
    {
        
    }

    public virtual void TransactionDialogue()
    {
        
    }
    
    
    


}
