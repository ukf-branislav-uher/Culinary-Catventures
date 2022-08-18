using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MerchantWindowControl : WindowControl
{
    [SerializeField] private List<GameObject> merchantCards;

    //DONT USE AWAKE CAUSE IT WILL OVERRIDE FROM PARENT CLASS


    private void AssignMerchantCards()
    {
        foreach (GameObject card in merchantCards)
        {

            Image artwork = card.GetComponent<Image>();
            Text nutritionalValue = card.GetComponentInChildren<Text>();
            TextMeshProUGUI energyCost = card.transform.Find("Energy").gameObject.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI price = card.transform.Find("Coin").gameObject.GetComponentInChildren<TextMeshProUGUI>();

            Button button = card.GetComponent<Button>();

            button.onClick.RemoveAllListeners();
            artwork.color = Color.white;

            CardBaseInfo randomCard = GetRandomIngredient();
            artwork.sprite = randomCard.Artwork;
            nutritionalValue.text = $"{randomCard.NutritionPoints}";
            energyCost.text = $"{randomCard.EnergyCost}";
            //CARD PRICE
            if (int.Parse(nutritionalValue.text) > 8)
            {
                price.text = $"{Random.Range(10, 16)}";
            }
            else
            {
                price.text = $"{Random.Range(5, 11)}";
            }
            //BUY CARD
            button.onClick.AddListener(delegate {
                if (player.HaveMoney(int.Parse(price.text)))
                {
                    uiLayer.ChangeMoney(-int.Parse(price.text));
                    player.Deck.Add(randomCard);
                    artwork.color = Color.gray;
                    button.onClick.RemoveAllListeners();
                }
                else
                {
                    uiLayer.ShowCoinsNotification();
                }

            });


        }
    }



    public void StartWindow()
    {
        ShowWindow();
        AssignMerchantCards();
    }



    //IN INSPECTOR 

}
