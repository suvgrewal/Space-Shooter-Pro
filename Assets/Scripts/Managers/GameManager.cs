using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    public void GameOver()
    {
        _isGameOver = true;
    }

    private void Update()
    {
        if (_isGameOver && GetRestartKeyDown())
        {
            RestartGame();
        }
    }

    private bool GetRestartKeyDown()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
