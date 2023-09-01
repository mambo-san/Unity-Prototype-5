using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pauseText;

    public Button restartButton;
    public GameObject titleScreen;
    public AudioSource backgroundMusic;

    public bool isGameActive;
    public bool isPaused;
    private int score;
    private float spawnRate = 1.0f;
    private int lives = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: ";
        
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseText.gameObject.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseText.gameObject.SetActive(true);
        }
        
    }

    IEnumerator spawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;

        for (int i=0; i < 20; i++)
        {
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        livesText.text = "Lives: " + lives;

        StartCoroutine(spawnTargets());
        updateScore(0);

        titleScreen.gameObject.SetActive(false);
    }

    public void DecreaseLives()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                GameOver();
            }
        }
        
    }

    
    
}
