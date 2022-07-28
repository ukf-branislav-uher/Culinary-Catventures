using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckButton : MonoBehaviour
{
    //[SerializeField] private Button button;
    [SerializeField] private Deck deckPrefab;
    [SerializeField] TextMeshProUGUI number;
    [SerializeField] private GameManager gameManager;
    //private Player player;


    private void Awake()
    {
        //GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }
    private void Update()
    {
        UpdateNumber();
    }

    public void ShowPlayerDeckInIsland()
    {
        Debug.Log("Show Deck!");
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UILayer uiLayer = GameObject.FindGameObjectWithTag("UILayer").GetComponent<UILayer>();

        Deck deckGameObject = Instantiate(deckPrefab,uiLayer.transform);

        deckGameObject.GenerateDeck(player.Deck);
        Adjust();
    }
    //TODO PRIDAT LISTY PODLA KTORYCH SA MA GENEROVAT DECK
    public void ShowPlayerDeckInBattle()
    {
        Debug.Log("Show Deck!");

        //GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GameObject toolTipLayer = GameObject.Find("Card Layer");
        Deck deckGameObject = Instantiate(deckPrefab,toolTipLayer.transform);

        deckGameObject.GenerateDeckInBattle(gameManager.deck);
        Adjust();
    }
    
    public void ShowPlayerDiscardInBattle()
    {        
        Debug.Log("Show Discard!");

        //GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GameObject toolTipLayer = GameObject.Find("Card Layer");

        Deck deckGameObject = Instantiate(deckPrefab,toolTipLayer.transform);

        deckGameObject.GenerateDeckInBattle(gameManager.discardPile);
        Adjust();
    }

    private void Adjust()
    {
        Time.timeScale = 0;
        EventManager.IsInEvent = true;
    }

    private void UpdateNumber()
    {
        //GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameObject.name == "DeckButton")
        {
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            
            if(text != null) text.text = $"{gameManager.deck.Count}";
            
        }
        if (gameObject.name == "DiscardButton")
        {
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{gameManager.discardPile.Count}";
        }
    }
}
