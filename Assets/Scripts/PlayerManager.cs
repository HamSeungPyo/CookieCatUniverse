using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
    //float 

    public bool bGameStart = false;

    private void Awake()
    {
    }

    void Update()
    {
        if (bGameStart)
        {

        }
        else
        {
            PlayerControl();
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
}
