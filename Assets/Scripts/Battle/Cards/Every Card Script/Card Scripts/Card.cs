using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypes
{
    Manoeuvre,
    Meat,
    Mix,
    Neutral,
    Vegetarian,
    None
}

public class Card : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] Sprite artwork;
    [SerializeField] List<CardEffect> cardEffects = null;
    [SerializeField] string cardName;
    [SerializeField] CardTypes cardType;
    [SerializeField] bool canTarget = false;
    [SerializeField] int energyCost;
    [SerializeField] int nutritionPoints;

    #endregion

    bool deleteOnBattleEnd = false;

    #region Getters/setters

    public bool DeleteOnBattleEnd
    {
        get { return deleteOnBattleEnd; }
        set { deleteOnBattleEnd = value; }
    }

    public Sprite Artwork
    {
        get { return artwork; }
        set { artwork = value; }
    }

    public List<CardEffect> CardEffect
    {
        get { return cardEffects; }
        set { cardEffects = value; }
    }

    public string CardName
    {
        get { return cardName; }
        set { cardName = value; }
    }

    public CardTypes CardType
    {
        get { return cardType; }
        set { cardType = value; }
    }

    public bool CanTarget
    {
        get { return canTarget; }
        set { canTarget = value; }
    }

    public int EnergyCost
    {
        get { return energyCost; }
        set { energyCost = value; }
    }

    public int NutritionPoints
    {
        get { return nutritionPoints; }
        set { nutritionPoints = value; }
    }

    #endregion

    public void GetDataFromBase(CardBaseInfo baseCard)
    {
        artwork = baseCard.Artwork;
        //if (baseCard.CardType == CardTypes.Manoeuvre)
        foreach(var effect in baseCard.CardEffect)
        {
            if (!(effect is FoodBase))
                cardEffects.Add(effect);
        }
        cardType = baseCard.CardType;
        cardName = baseCard.CardName;
        canTarget = baseCard.CanTarget;
        energyCost = baseCard.EnergyCost;
        nutritionPoints = baseCard.NutritionPoints;

        //if(cardEffect != null)
        //{
        //    cardEffect.Amount = baseCard.Amount;
        //    cardEffect.DiscardAmount = baseCard.DiscardAmount;
        //}
    }

    public void TriggerCardEffect(GameManager gm, RaycastHit2D hit)
    {
        foreach(CardEffect effect in cardEffects)
        {
            effect.Effect(gm, hit);
        }
    }

    public int CalculateNP(GameManager gm)
    {
        switch (CardType)
        {
            case CardTypes.Vegetarian:
                return (int) (nutritionPoints * gm.Player.VegetarianFoodMod);
            case CardTypes.Meat:
                return (int) (nutritionPoints * gm.Player.MeatFoodMod);
            case CardTypes.Mix:
                return (int)(nutritionPoints * gm.Player.GeneralFoodMod);
            case CardTypes.Neutral:
                return (int) (nutritionPoints * gm.Player.GeneralFoodMod);
            default:
                return -1;
        }
    }

    public void DeletionCheck(bool overrideVar = false) //we can use overrideVar to forcefully delete card
    {
        if (deleteOnBattleEnd || overrideVar)
        {
            Destroy(gameObject);
        }
    }
}