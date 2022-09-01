using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBehavior : CharacterBehavior
{
    [SerializeField] private List<CodedDialogue> merchantCodedDialogues;

    public override void InitiateCharacterSequence()
    {
        base.InitiateCharacterSequence();
        StandardDialogue();
    }

    public override void StandardDialogue()
    {
        base.StandardDialogue();
        switch (hideFlags)
        {
            
        }
        
    }

    void Update()
    {
        
    }
}
