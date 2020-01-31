using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        coinsText.text = coins.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        Destroy(FindObjectOfType<ScenePersist>().gameObject);
        SceneManager.LoadScene("Main Menu");
        Destroy(gameObject);
    }

    public void AddCoins()
    {
        coins++;
        coinsText.text = coins.ToString();
    }
}
