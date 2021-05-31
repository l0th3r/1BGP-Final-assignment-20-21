using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameModeManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject quitBtn;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private float timerValue = 60f;

    private int score = 0;
    private float timerCurrent = 0f;
    private bool countdown = false;

    private void Start()
    {
        player.GetComponent<playerManager>().canMove = false;
        startBtn.SetActive(true);
        quitBtn.SetActive(true);
        restartBtn.SetActive(false);
        gameOverUI.SetActive(false);
        scoreUI.transform.gameObject.SetActive(false);
        timerUI.transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (countdown)
        {
            if (timerCurrent > 0)
                timerCurrent -= Time.deltaTime;
            else
            {
                timerCurrent = 0;
                player.GetComponent<playerManager>().canMove = false;
                timerUI.transform.gameObject.SetActive(false);
                restartBtn.SetActive(true);
                quitBtn.SetActive(true);
                gameOverUI.SetActive(true);
            }
        }
        updateTimerUI();
    }

    public void startGame()
    {
        score = 0;
        countdown = true;
        timerCurrent = timerValue + 1;

        updateScoreUI();
        player.GetComponent<playerManager>().canMove = true;
        player.GetComponent<playerManager>().resetPosition();
        scoreUI.transform.gameObject.SetActive(true);
        timerUI.transform.gameObject.SetActive(true);
        startBtn.SetActive(false);
        quitBtn.SetActive(false);
        restartBtn.SetActive(false);
        gameOverUI.SetActive(false);
        ground.GetComponent<GroundController>().defineStartPos();
    }


    public void addToScore(int value)
    {
        score += value;
        updateScoreUI();
    }

    private void updateScoreUI()
    {
        scoreUI.text = "Score: " + score;
    }
    private void updateTimerUI()
    {
        if (timerCurrent < 0)
            timerCurrent = 0;
        float min = Mathf.FloorToInt(timerCurrent / 60);
        float sec = Mathf.FloorToInt(timerCurrent % 60);

        timerUI.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
