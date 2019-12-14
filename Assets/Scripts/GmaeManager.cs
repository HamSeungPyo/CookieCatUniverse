using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GmaeManager : MonoBehaviour
{
    public AudioSource BackgroundSound;
    bool bGameStart = false;

    //게임 메인화면
    public GameObject mainViewTexts;
    public Text gameStart_Text;
    float gameStart_TextColor = 1;
    bool textColorChange = true;
    public AudioSource test;

    //게임 플레이
    public ScoreSystem script_ScoreSystem;
    public PlayerHeartManager script_PlayerHeartManager;
    public GameObject gameOver_Texts;
    private void Awake()
    {
        script_PlayerHeartManager = GetComponent<PlayerHeartManager>();
        script_ScoreSystem = GetComponent<ScoreSystem>();
        BackgroundSound = GetComponent<AudioSource>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!bGameStart)
            {
                test.time = 0.1f;
                test.Play();
            }
            bGameStart = !bGameStart;
        }
        
        if (bGameStart)
        {
            script_PlayerHeartManager.SetActive(true);
            mainViewTexts.SetActive(false);
        }
        else
        {
            script_PlayerHeartManager.SetActive(false);
            script_ScoreSystem.currentScore = 0;
            mainViewTexts.SetActive(true);
            GameStartTextControl();
        }
    }
    void GameStartTextControl()
    {
        gameStart_Text.color = new Color(1, 1, 1, gameStart_TextColor);

        if (gameStart_TextColor <= 0.2f)
        {
            textColorChange = false;
        }
        else if (gameStart_TextColor >= 1)
        {
            textColorChange = true;
        }
        if (textColorChange)
        {
            gameStart_TextColor -= Time.deltaTime;
        }
        else
        {
            gameStart_TextColor += Time.deltaTime;
        }
    }
}
