using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private int _numScoreLetters = 5;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _livesDisplayImg;

    [SerializeField]
    private float _flickerDelay = 1f;

    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();

        _livesDisplayImg = GameObject.Find("LivesDisplayImg").GetComponent<Image>();

        _gameOverText = GameObject.Find("GameOverText");

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (!_player)
        {
            Debug.LogError("UIManager: Player is NULL");
        }

        if (!_scoreText)
        {
            Debug.LogError("UIManager: Score Text is NULL");
        }

        if (!_livesDisplayImg)
        {
            Debug.LogError("UIManager: Lives Display Image is NULL");
        }

        if (!_gameOverText)
        {
            Debug.LogError("UIManager: Game Over Text is NULL");
        }

        if (!_gameManager)
        {
            Debug.LogError("UIManager: Game Manager is NULL");
        }

        for (int i = 0; i < _liveSprites.Length; i++)
        {
            if (!_liveSprites[i])
            {
                Debug.LogError("UIManager: Live Sprite at index " + i + " is NULL");
            }
        }

        _gameOverText.SetActive(false);

        UpdateScore();
    }

    void Update()
    {

    }

    public void UpdateScore()
    {
        if (_player)
        {
            _scoreText.text = "Score: " + padScore(_player.Score);
        }
    }

    public void UpdateLivesDisplay()
    {
        if (_livesDisplayImg)
        {
            {
                Debug.Log("Player Lives value: " + _player.Lives);
                _livesDisplayImg.sprite = _liveSprites[_player.Lives];
            }
        }

        if (_player.Lives < 1)
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _gameOverText.SetActive(true);

        _gameManager.GameOver();

        StartCoroutine(GameOverFlickerCoroutine());
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    string padScore(int _scoreToPad)
    { 
        string scoreString = _scoreToPad.ToString();

        while (scoreString.Length < _numScoreLetters)
        {
            scoreString = "0" + scoreString;
        }

        return scoreString;
    }

    IEnumerator GameOverFlickerCoroutine()
    {
        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(_flickerDelay);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(_flickerDelay);
        }
    }
}
