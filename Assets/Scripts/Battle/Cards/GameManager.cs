using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { PLAYERTURN, ENEMYTURN, WON, LOST }
public class GameManager : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] Player player;
    [SerializeField] List<Customer> customers = new List<Customer>();
    [SerializeField] CardSlot[] cardSlots;
    [SerializeField] bool[] availableCardSlots;
    [SerializeField] TextMeshProUGUI energyUI;
    [SerializeField] TextMeshProUGUI repUI;
    [SerializeField] DiscardController discardController;
    [SerializeField] CombineController combineController;
    [SerializeField] List<Card> deck;
    [SerializeField] List<Card> discardPile = new List<Card>();
    [SerializeField] List<Card> hand = new List<Card>();
    [SerializeField] List<Card> exhaustPile = new List<Card>();
    [SerializeField] List<IBuffable> buffs = new List<IBuffable>();
    #endregion

    private int numOfCards = 0;

    #region Public Vars
    public bool combinePhase;
    public bool discardPhase;
    public int count;
    public BattleState battleState;
    public Player Player => player;      //getter
    #endregion

    private void Start()
    {
        if (customers.Count == 0)
        {
            //if player is not dead then you won
            battleState = BattleState.WON;
            Debug.Log("YAY YOU WON!");
            //else you lose and its end of game
        }
            
        else
        {
            deck = new List<Card>(player.Deck);
            for (int i = 0; i < cardSlots.Length; i++)
            {
                cardSlots[i].Hide(true);
            }
            DrawCards(5);
            AddEnergy(player.MaxEnergy);
            repUI.text = $"{player.Rep}";
            combinePhase = false;
            discardPhase = false;

            battleState = BattleState.PLAYERTURN;
        }
    }

    #region Player Functions
    public void AddEnergy(int amount)
    {
        player.Energy += amount;
        energyUI.text = $"{player.Energy}/{player.MaxEnergy}";
    }

    public bool SpendEnergy(int amount)
    {
        if(player.Energy - amount < 0)
        {
            return false;
        }
        else
        {
            player.Energy -= amount;
            energyUI.text = $"{player.Energy}/{player.MaxEnergy}";
            return true;
        }
    }

    public void HurtPlayer(int amount)
    {
        player.TakeDamage(amount);
        repUI.text = $"{player.Rep}";
    }

    public void BuffPlayer(IBuffable buff, bool applyNow = false)
    {
        buff.SetTarget(player);
        buffs.Add(buff);
        if (applyNow)
            ApplyBuff(buff);
    }

    //run this at the start of Player's turn so all the buffs are added properly
    public void ApplyBuff(IBuffable buff)
    {
        if (buff.Finished)
            buffs.Remove(buff);
        else
        {
            Debug.Log("apply");
            buff.Apply();
        }
        UpdateUI();
    }

    //run this at the start of Player's turn so all the buffs are added properly
    public void ApplyBuffables()
    {
        player.GeneralFoodModBonus = 0;
        player.MeatFoodModBonus = 0;
        player.VegetarianFoodModBonus = 0;
        List<IBuffable> delBuffs = new List<IBuffable>();
        foreach (var buff in buffs)
        {
            if (buff.Finished)
                delBuffs.Add(buff);
            else
            {
                buff.Apply();
            }
        }
        //TODO: Find a better way to do this
        foreach (var buff in delBuffs)
        {
            buffs.Remove(buff);
            buff.EndEffect();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        repUI.text = $"{player.Rep}";
        energyUI.text = $"{player.Energy}/{player.MaxEnergy}";
        foreach(var card in cardSlots)
        {
            card.UpdateNP();
        }
    }
    #endregion

    #region Turn Base Functions
    public void EndPlayersTurn()
    {
        battleState = BattleState.ENEMYTURN;
        Debug.Log("ITS ENEMIES TURN");

        DiscardHand();
        count = customers.Count-1;

        foreach (var customer in customers)
        {
            customer.StartTurn();
        }

        //hand.Clear();
        SpendEnergy(player.Energy);
        AddEnergy(player.MaxEnergy);

        EndEnemyTurn();
        ApplyBuffables();
    }

    public void EndEnemyTurn()
    {
        battleState = BattleState.PLAYERTURN;
        Debug.Log("ITS PLAYERS TURN");

        DrawCards(5);

        foreach (var customer in customers)
        {
            customer.EndTurn();
            customer.RandomizeDebuffs();
        }
    }
    public void customerListDelete(Customer customer)
    {
        customers.Remove(customer);
    }
    #endregion

    #region Card Functions
    private bool DrawCard()
    {
        Card randCard = deck[Random.Range(0, deck.Count)];

        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                //randCard.gameObject.SetActive(true);
                //randCard.transform.position = cardSlots[i].transform.position;
                cardSlots[i].SetHasBeenPlayed(false);
                HideCardSlot(i, false);
                cardSlots[i].HandIndex = i;
                cardSlots[i].SetCard(randCard);
                hand.Add(randCard);
                availableCardSlots[i] = false;
                deck.Remove(randCard);
                numOfCards++;
                return true;
            }
        }
        return false;
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count >= 1)
            {
                if (!DrawCard())
                {
                    //Debug.Log("Couldn't draw a card. The hand is full!");
                    return;
                }
            }
            else
            {
                //Debug.Log("Couldn't draw a card. The deck is empty!"); 
                Shuffle();
                i--;
            }
        }
        //Debug.Log("I DID IT! :)");
    }

    public void DeselectAllCards()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].Deselect();
        }
    }

    public void DiscardHand()
    {
        foreach (var card in cardSlots)
        {
            //card.MoveToDiscardPile(true);
            if(card.enabled)
                SendToDiscard(card.HandIndex, true);//ARTURITO
        }
        hand.Clear();
    }

    public void HideCardSlot(int idx, bool isHidden)
    {
        cardSlots[idx].Hide(isHidden);
    }

    //Called when pointer hovers over a card to make it more visible
    //TODO: Find a way to move a card to the front. Z-order and Child indexes don't work
    public void MoveNeighbours(int idx, bool isReturning)
    {
        return;
        if (isReturning)
        {
            if (idx != 0)
                cardSlots[idx - 1].ResetPos();
            if (idx != (cardSlots.Length - 1))
                cardSlots[idx + 1].ResetPos();
            //for (int i = 0; i < cardSlots.Length; i++)
            //{
            //    cardSlots[i].ResetPos();
            //}
        }
        else
        {
            float moveAmount = 0.25f + (0.1f * (numOfCards - 5));
            Debug.Log(moveAmount);
            if (idx != 0)
                cardSlots[idx - 1].MoveLeft(moveAmount);
            if (idx != (cardSlots.Length - 1))
                cardSlots[idx + 1].MoveRight(moveAmount);
            //Old code, reuse it!
            //int pow = 1;
            //float movementAmount;
            //for (int i = idx - 1; i >= 0; i--)
            //{
            //    movementAmount = (float)1 / Mathf.Pow(2, pow);
            //    cardSlots[i].MoveLeft(movementAmount);
            //    pow++;
            //}
            //pow = 1;
            //for (int i = idx + 1; i < cardSlots.Length; i++)
            //{
            //    movementAmount = (float)1 / Mathf.Pow(2, pow);
            //    cardSlots[i].MoveRight(movementAmount);
            //    pow++;
            //}
        }
    }

    public void SendToDiscard(int idx, bool isEndTurn)
    {
        Debug.Log("idx:"+idx);
        Card card = cardSlots[idx].GetCard();
        availableCardSlots[idx] = true;
        //card.HandIndex = -1;
        //card.SetHasBeenPlayed(false);
        HideCardSlot(idx, true);
        //if (!isEndTurn){
        hand.Remove(card);
        //}
        discardPile.Add(card);
        numOfCards--;
    }

    public void SendToExhaust(int idx)
    {
        Card card = cardSlots[idx].GetCard();
        availableCardSlots[idx] = true;//ARTURITOX
        //card.HandIndex = -1;
        //card.SetHasBeenPlayed(false);
        HideCardSlot(idx, true);
        hand.Remove(card);
        exhaustPile.Add(card);
    }

    private void Shuffle()
    {
        deck = new List<Card>(discardPile);
        discardPile.Clear();
    }
    #endregion

    #region Discard Phase Functions
    public async Task StartDiscard()
    {
        Debug.Log("Start Discard");
        discardController.ToggleDiscard();
        discardPhase = true;
        DeselectAllCards();
        while (discardPhase)
        {
            //Debug.Log("I'm yealding ova here! " + discardPhase.ToString());
            await Task.Yield();
        }
        Debug.Log("I'm done mate!");

        //StopDiscard();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopDiscard();
            //HurtPlayer(1000);
            //cardSlots[2].CreateCanvas();
        }
    }

    public void StopDiscard()
    {
        Debug.Log("Stop Discard");
        discardController.ToggleDiscard();
        discardPhase = false;
    }

    public void SetDiscardFilter(string[] filter)
    {
        discardController.Filter = filter;
    }

    public bool SetCard(CardSlot card)
    {
        if (discardPhase)
        {
            //Debug.Log("SET");
            return discardController.SelectCard(card);
        }
        return false;
    }
    #endregion

    #region Combine Phase Functions
    public void ToggleCombine()
    {
        if (combinePhase == true)
        {
            StopCombine();
        }
        else
        {
            StartCombine();
        }
    }

    private void StartCombine()
    {
        //Debug.Log("Start Combine");
        combineController.ToggleCombine();
        combinePhase = true;
        DeselectAllCards();
    }

    private void StopCombine()
    {
        //Debug.Log("Stop Combine");
        combineController.ToggleCombine();
        combinePhase = false;
        DeselectAllCards();
    }

    public void FindCombineTarget()
    {
        List<string> find = new List<string>();
        List<string> types = new List<string>();
        Card card;

        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].Selected)
            {
                card = cardSlots[i].GetCard();
                //if(card.CardType != "Manoeuvre")
                //{
                find.Add(card.CardName);
                types.Add(card.CardType);
                //}
            }
        }
        combineController.FindCard(find.ToArray(), types.ToArray());
    }

    public void CombineCards(Card target)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].Selected)
            {
                cardSlots[i].Deselect();
                SendToExhaust(i);
            }
        }
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                cardSlots[i].SetHasBeenPlayed(false);
                cardSlots[i].gameObject.SetActive(true);
                cardSlots[i].SetCard(target);
                cardSlots[i].HandIndex = i;
                hand.Add(target);
                availableCardSlots[i] = false;
                break;
            }
        }
        ToggleCombine();
    }

    public int GetComboNP(bool isPile)
    {
        int result = 0;
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].Selected)
            {
                result += cardSlots[i].GetCard().NutritionPoints;
            }
        }
        if (isPile)
        {
            return result + (int)(result * 0.05f);
        }
        else
        {
            return result + (int)(result * 0.5f);
        }
    }

    public int GetNumOfSelected()
    {
        int selected = 0;
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].Selected)
            {
                selected++;
            }
        }
        return selected;
    }
    #endregion
}
