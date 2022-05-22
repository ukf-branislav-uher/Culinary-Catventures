using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardController : MonoBehaviour
{
    [SerializeField] private ManouverTargetController targetController;
    [SerializeField] private Text header;
    [SerializeField] private Transform cardSlot;
    [SerializeField] private GameManager gm;
    [SerializeField] private Button discardBttn;
    private string[] filter;
    public string[] Filter
    {
        set
        {
            filter = value;
        }
    }

    private CardSlot selectedCard;

    public void DiscardCard()
    {
        if(selectedCard != null)
        {
            gm.SendToDiscard(selectedCard.GetCard().HandIndex, false);
            gm.StopDiscard();
        }
    }
    
    public void ToggleDiscard()
    {
        if (!gm.discardPhase)
        {
            this.gameObject.SetActive(true);
            targetController.SetPos(false);
            discardBttn.gameObject.SetActive(true);
            discardBttn.interactable = false;
        }
        else
        {
            targetController.SetPos(true);
            discardBttn.interactable = false;
            discardBttn.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void SelectCard(CardSlot otherSelectedCard)
    {
        if (IsinFilter(otherSelectedCard.GetCard().CardType))
        {
            if (this.selectedCard != null)
            {
                this.selectedCard.Deselect();
            }
            this.selectedCard = otherSelectedCard;
            this.selectedCard.transform.position = new Vector3(this.cardSlot.position.x, this.cardSlot.position.y, this.selectedCard.transform.position.z);
            discardBttn.interactable = true;
        }
    }

    private bool IsinFilter(string selectedCardType)
    {
        for(int i = 0; i < filter.Length; i++)
        {
            if(selectedCardType == filter[i])
            {
                return true;
            }
        }
        return false;
    }
}
