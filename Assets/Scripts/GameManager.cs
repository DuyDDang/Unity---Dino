using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float gameSpeed = 5f;
    [SerializeField] private float speedIncrease = 0.15f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject scoreTextobject;
    [SerializeField] private GameObject gameStartMess;
    [SerializeField] private GameObject gameOverMess;

    private bool isGameOver = false;
    private float score = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public float GetGameSpeed()
    {
        return gameSpeed;
    }
    void Start()
    {
        StartGame();
    }
    void Update()
    {
        UpdateGameSpeed();
        HandleStartGameInput();
        UpdateScore();
        if (!isGameOver)
        {
            UpdateGameSpeed();
            UpdateScore();
        }
    }
    private void UpdateGameSpeed()
    {
        gameSpeed += Time.deltaTime * speedIncrease;
    }
    private void UpdateScore()
    {
        score += Time.deltaTime * 10;
        scoreText.text = "Score:" + Mathf.FloorToInt(score);
    }
    private void StartGame()
    {
        Time.timeScale = 0;
        scoreTextobject.SetActive(false);
        gameStartMess.SetActive(true);
        gameOverMess.SetActive(false);
    }
    private void HandleStartGameInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Test trong Editor / PC: click chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            scoreTextobject.SetActive(true);
            gameStartMess.SetActive(false);
        }
#elif UNITY_ANDROID || UNITY_IOS
    // Mobile: chạm bất kỳ đâu trên màn hình
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        Time.timeScale = 1;
        scoreTextobject.SetActive(true);
        gameStartMess.SetActive(false);
    }
#endif
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverMess.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(ReLoadScene());
    }
    private IEnumerator ReLoadScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
