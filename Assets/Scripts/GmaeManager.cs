using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GmaeManager : MonoBehaviour
{
    public PlayerManager scrip_PlayerManager;
    public AudioSource BackgroundSound;
    public bool bGameStart = false;

    //게임 메인화면
    public GameObject mainViewTexts;
    public Text gameStart_Text;
    float gameStart_TextColor = 1;
    bool textColorChange = true;
    public AudioSource test;

    //게임 플레이
    public CreateMonsterManager script_CreateMonsterManager;
    public ScoreSystem script_ScoreSystem;
    public PlayerHeartManager script_PlayerHeartManager;
    public GameObject gameOver_Texts;
    public bool bGameOver = false;

    private void Awake()
    {
        script_PlayerHeartManager = GetComponent<PlayerHeartManager>();
        script_ScoreSystem = GetComponent<ScoreSystem>();
        BackgroundSound = GetComponent<AudioSource>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bGameStart = false;
        }
        else if (Input.anyKeyDown && !(Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            if (!bGameStart)
            {
                test.time = 0.1f;
                test.Play();

                bGameStart = true;
            }
        }

        scrip_PlayerManager.bGameStart = bGameStart;
        gameOver_Texts.SetActive(scrip_PlayerManager.bGameEnd);
        if (bGameStart)
        {
            if (scrip_PlayerManager.bGameEnd)
            {
                bGameOver = true;
                script_CreateMonsterManager.bGameStart = false;
                script_PlayerHeartManager.SetActive(false);
            }
            else
            {
                if(bGameOver)
                    script_ScoreSystem.currentScore = 0;

                bGameOver = false;
                script_CreateMonsterManager.bGameStart = true;
                script_PlayerHeartManager.SetActive(true);
                mainViewTexts.SetActive(false);
            }
        }
        else
        {
            bGameOver = true;
            script_CreateMonsterManager.bGameStart = false;
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
