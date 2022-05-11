using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DiscardACard", menuName = "Cards/Discard a card")]
public class DiscardACard : Card, ISpendEnergy
{
    [SerializeField] private int amountEnergyGained;
    public override void CardEffect(GameManager gm, RaycastHit2D hit)
    {
        Discard(gm);
    }

    public void SpendEnergy(GameManager gm, int amount)
    {
        gm.SpendEnergy(amount);
    }

    private async void Discard(GameManager gm)
    {
        await gm.StartDiscard();
        SpendEnergy(gm, -amountEnergyGained);
    }
}
