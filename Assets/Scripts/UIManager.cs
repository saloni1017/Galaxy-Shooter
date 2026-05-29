using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle text component
    public Text score;
    public Text gameOver;
    public Text restartLevel;
    [SerializeField]
    private Image _livesDisplay;
    private GameManager _gameManager;
    [SerializeField]
    private Sprite[] _livesSprite;
    // Start is called before the first frame update
    void Start()
    {
        //assign text componen to handle
        score.text = "Score: " + 0.ToString();
        gameOver.gameObject.SetActive(false);
        restartLevel.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerscore)
    {
        score.text = "Score: " + playerscore.ToString();
    }

    public void UpdateLives(int currentLive)
    {
        //display img sprite
        //update img based on current live imdex
        _livesDisplay.sprite = _livesSprite[currentLive];

        if(currentLive == 0) 
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _gameManager.GameOver();
        gameOver.gameObject.SetActive(true);
        restartLevel.gameObject.SetActive(true);
        StartCoroutine(GameOverFlikerRoutine());
    }

    IEnumerator GameOverFlikerRoutine()
    {
        while (true)
        {
            gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
