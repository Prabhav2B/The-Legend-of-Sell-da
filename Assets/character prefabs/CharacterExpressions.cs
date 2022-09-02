using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpressions : MonoBehaviour
{
    //    public static CharacterExpressions Instance;

    private UnityEngine.U2D.Animation.SpriteResolver SpriteResolver;

    //    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private string SpriteCategory;
    //private string SpriteCategory; 

    public string Label_Curious;
    public string Label_Frown;
    public string Label_Happy;
    public string Label_SuperFrown;
    public string Label_SuperHappy;
    public string Label_Furious;
    public string Label_Smile;


    // Start is called before the first frame update
    void Start()
    {
        SpriteResolver = GetComponent<UnityEngine.U2D.Animation.SpriteResolver>();
        SpriteCategory = SpriteResolver.GetCategory();
    }

    // Update the Face Sprite
    public void UpdateExpression(DialogueSequenceSO dialogueSequence)
    {
        string SpriteLabel = Label_Curious;

        if (dialogueSequence.expression == Enums.Expressions.curious) // curious
        {
            SpriteLabel = Label_Curious;
        }
        if (dialogueSequence.expression == Enums.Expressions.frown) // frown
        {
            SpriteLabel = Label_Frown;
        }
        if (dialogueSequence.expression == Enums.Expressions.happy) // happy
        {
            SpriteLabel = Label_Happy;
        }
        if (dialogueSequence.expression == Enums.Expressions.super_frown) //super_frown
        {
            SpriteLabel = Label_SuperFrown;
        }
        if (dialogueSequence.expression == Enums.Expressions.super_happy) // super_happy
        {
            SpriteLabel = Label_SuperHappy;
        }
        if (dialogueSequence.expression == Enums.Expressions.furious) // furious
        {
            SpriteLabel = Label_Furious;
        }
        if (dialogueSequence.expression == Enums.Expressions.smile) // smile
        {
            SpriteLabel = Label_Smile;
        }

        SpriteResolver.SetCategoryAndLabel(SpriteCategory, SpriteLabel);
        SpriteResolver.ResolveSpriteToSpriteRenderer();
    }
}



