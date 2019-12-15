using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    public int heart = 3;

    float damageColor = 1;
    public float speed = 4;

    ScoreSystem script_ScoreSystem;
    GmaeManager script_GmaeManager;
    AudioSource mosterAudio;

    public GameObject explosionAsteroid_VFX;
    private void Awake()
    {
        mosterAudio = GetComponent<AudioSource>();
        script_ScoreSystem = GameObject.Find("GameManager").GetComponent<ScoreSystem>();
        script_GmaeManager = script_ScoreSystem.GetComponent<GmaeManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }

        if (script_GmaeManager.bGameOver)
        {
            speed = 20;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player_Attack")
        {
            Destroy(col.gameObject);
            heart--;
            if (heart <= 0)
            {
                if (transform.localScale.x > 0.3f)
                {
                    script_ScoreSystem.currentScore += 50f;
                }
                else
                {
                    script_ScoreSystem.currentScore += 15f;
                }
                mosterAudio.time = 0.1f;
                mosterAudio.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                GameObject vfx= Instantiate(explosionAsteroid_VFX, transform.position, Quaternion.identity);
                Destroy(vfx, 1f);
                Destroy(gameObject, 1f);
            }
            else
            {
                damageColor -= 0.5f;
                GetComponent<SpriteRenderer>().color = new Color(1, damageColor, damageColor, 1);
            }
        }
        else if (col.tag == "Player")
        {
            mosterAudio.time = 0.1f;
            mosterAudio.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1f);
            GameObject vfx = Instantiate(explosionAsteroid_VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
        }
    }
}
