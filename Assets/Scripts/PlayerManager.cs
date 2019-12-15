using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //목숨
    public int heart = 3;
    public bool bGameEnd = false;
    public AudioSource playerDeadAudio;
    bool bInvincible = false;
    //공격
    public GameObject bullet;
    bool bPlayerAttack = false;
    float playerAttackTimer = 0;
    public AudioSource playerAudio;

    //이동
    public float moveSpeed=1;
    float movementValue_X = 0;
    float movementValue_Y = 0;

    //시작 연출
    public bool bWaitingGame = true;
    public bool bGameStart = false;


    private void Awake()
    {
    }

    void Update()
    {
        if (bGameStart)
        {
            if (!bWaitingGame)
            {
                if (bGameEnd)
                {
                    StopCoroutine();
                    heart = 3;
                    movementValue_Y = -6;
                    GetComponent<SpriteRenderer>().enabled = false;
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        bWaitingGame = true;
                    }
                }
                else
                {
                    PlayerControl();
                }
            }
            else
            {
                movementValue_X = 0;
                StopCoroutine();
                bGameEnd = false;
                GetComponent<SpriteRenderer>().enabled = true;
                movementValue_Y += Time.deltaTime*1.5f;
                if (movementValue_Y >= -4)
                {
                    movementValue_Y = -4;
                    bWaitingGame = false;
                }
                transform.position = new Vector3(0, movementValue_Y, 0);
            }
        }
        else
        {
            StopCoroutine();
            bGameEnd = false;
            heart = 3;
            transform.position = new Vector3(0, -6, 0);
            GetComponent<SpriteRenderer>().enabled = false;
            bWaitingGame = true;
            movementValue_Y = -6;
            movementValue_X = 0;
        }
    }
    void PlayerControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            bPlayerAttack = true;
        }
        else
        {
            playerAttackTimer = 1;
            bPlayerAttack = false;
        }

        if (bPlayerAttack)
        {
            if (playerAttackTimer >= 0.1f)
            {
                playerAudio.Play();
                Instantiate(bullet, transform.position, Quaternion.identity);
                playerAttackTimer = 0;
            }
            playerAttackTimer += Time.deltaTime;
        }
        //Vertical
        movementValue_X += Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        movementValue_Y += Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;

        movementValue_X = Mathf.Clamp(movementValue_X, -3.2f, 3.2f);
        movementValue_Y = Mathf.Clamp(movementValue_Y, -4.6f, 4.6f);

        transform.position = new Vector3(movementValue_X, movementValue_Y, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !bGameEnd)
        {
            if (bInvincible)
                return;

            StartCoroutine("Invincible");
            heart--;

            if (heart <= 0)
            {
                playerDeadAudio.time = 5f;
                playerDeadAudio.Play();
                bGameEnd = true;
            }
        }
    }
    IEnumerator Invincible()
    {
        bInvincible = true;
        float spriteColor = 1;
        bool textColorChange = true;


        float timer = 0;
        while (true)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, spriteColor, spriteColor, spriteColor);
            if (spriteColor <= 0.5f)
            {
                textColorChange = false;
            }
            else if (spriteColor >= 1)
            {
                textColorChange = true;
            }

            if (textColorChange)
            {
                spriteColor -= Time.deltaTime * 5;
            }
            else
            {
                spriteColor += Time.deltaTime * 5;
            }
            timer += Time.deltaTime;

            if (timer > 2)
            {
                break;
            }
            yield return null;
        }
        StopCoroutine();
    }

    void StopCoroutine()
    {
        StopCoroutine("Invincible");
        bInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
