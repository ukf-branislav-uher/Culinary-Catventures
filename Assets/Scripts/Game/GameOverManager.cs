using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private Button continueBtn;
    [SerializeField] private TextMeshProUGUI gameOverMsg;
    [SerializeField] private Image artwork;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI[] score;
    [SerializeField] private GameObject scoreBoardParent;
    [SerializeField] private GameObject gameOverParent;
    [SerializeField] private Player player;
    private float expTillNextLvl;
    private int finalScore;
    // Start is called before the first frame update
    void Start()
    {
        continueBtn.onClick.AddListener(GoToScore);
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        gameOverParent.SetActive(true);
        scoreBoardParent.SetActive(false);
    }

    private void GoToScore()
    {
        //Hide game over text and make score appear
        gameOverParent.SetActive(false);
        scoreBoardParent.SetActive(true);

        //Set up scoreboard
        score[5].text = $"{player.NextLevel - player.CurrentExp}";//exp to next level
        score[0].text = $"{player.Deck.Count}"; 
        score[1].text = $"{player.Money}";
        score[2].text = $"{0}";
        score[3].text = $"{0}";
        score[4].text = $"{50}";
        expTillNextLvl = player.CurrentExp;
        progressBar.fillAmount = (float)expTillNextLvl / player.NextLevel;
        finalScore = (player.Deck.Count + player.Money + 50)*10;

        StartCoroutine(FillBar(1));

        //Make the continue button send you to main menu
        continueBtn.onClick.RemoveListener(GoToScore);
    }

    IEnumerator FillBar(int expToFill)
    {
        int remainingExp = expToFill;
        while(remainingExp > 0)
        {
            Debug.Log("While "+ remainingExp);
            if(remainingExp - 5 > 0)
            {
                Debug.Log("While2 " + remainingExp);
                expTillNextLvl += 5;
                remainingExp -= 5;
            }
            else
            {
                Debug.Log("While2.5 " + remainingExp);
                expTillNextLvl += remainingExp;
                remainingExp -= remainingExp;
            }
            if(expTillNextLvl> player.NextLevel)
            {
                Debug.Log("While3 " + remainingExp);
                //TODO: add level up logic for player
                expTillNextLvl -= player.NextLevel;
            }
            Debug.Log("While4 " + remainingExp);
            progressBar.fillAmount = (float)expTillNextLvl / player.NextLevel;
            Debug.Log("While5 " + remainingExp);
            score[5].text = $"{player.NextLevel - expTillNextLvl}";
            Debug.Log("While6 " + remainingExp);
            if(remainingExp <= 0) { break; } //for some reason the code gets stuck here
            yield return new WaitForSeconds(0.025f);
        }
        Debug.Log("I'm done");
        player.CurrentExp = (int)expTillNextLvl;
        continueBtn.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        //TODO: The game should save any cards player unlocks here
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
