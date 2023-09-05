using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private Image _LifeImg;
    [SerializeField]
    private Text _gameOver_Text;
    [SerializeField]
    private Text _restart_text;

    private GameManager _gameManager;

    
    
    // Start is called before the first frame update
    void Start()
    {
        GameOverSequence();
    }

    void GameOverSequence()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver_Text.gameObject.SetActive(false);
        _restart_text.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    public void UpdateScore(int playerscore)
    {
        _scoreText.text = "score: " + playerscore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LifeImg.sprite = _lifeSprites[currentLives];
        if (currentLives == 0)
        {
            _gameOver_Text.gameObject.SetActive(true);
            _restart_text.gameObject.SetActive(true);
            _gameManager.GameOver();

            StartCoroutine(GameOverFlickRoutin());
            
        }

    }

    IEnumerator GameOverFlickRoutin()
    {
        while(true)
        {
            _gameOver_Text.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver_Text.text = "";
            yield return new WaitForSeconds(0.5f);

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
