using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonsterManager : MonoBehaviour
{
    public GameObject[] enemy = new GameObject[3];

    public bool bGameStart = false;

    public float spawnPosX = 0;

    public float speed = 4;

    readonly float waitingTime = 2;

    private void Awake()
    {
        StartCoroutine(MonsterSpawn());
    }
    private void Update()
    {
        if (bGameStart)
        {
            if (speed <= 50)
            {
                speed += Time.deltaTime*0.1f;
            }
            else
            {
                speed = 50;
            }
        }
        else
        {
            speed = 4;
        }
    }
    IEnumerator MonsterSpawn()
    {
        float timer = 0;
        while (true)
        {
            if (bGameStart)
            {
                if (timer >= waitingTime)
                {
                    GameObject createEnemy = Instantiate(enemy[Random.Range(0, 3)], new Vector3(0, 6, 0), Quaternion.identity);

                    float pos = 0.15f;
                    if (Random.Range(0, 3) == 1)
                    {
                        pos = 0.5f;
                        spawnPosX = Random.Range(-2.3f, 2.3f);
                        createEnemy.transform.localPosition = new Vector3(spawnPosX, 6, 0);
                        createEnemy.transform.localScale = new Vector3(pos, pos, 1);
                        createEnemy.GetComponent<MonsterControl>().speed = speed;
                        createEnemy.GetComponent<MonsterControl>().heart = 3;
                        yield return new WaitForSeconds(1.3f - (speed / 40));
                    }
                    else
                    {
                        spawnPosX = Random.Range(-3.3f, 3.3f);
                        createEnemy.transform.localPosition = new Vector3(spawnPosX, 6, 0);
                        createEnemy.transform.localScale = new Vector3(pos, pos, 1);
                        createEnemy.GetComponent<MonsterControl>().speed = speed;
                        createEnemy.GetComponent<MonsterControl>().heart = 1;
                        yield return new WaitForSeconds(0.3f - (speed / 150));
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else
            {
                timer = 0;
            }
            yield return null;
        }
    }
}
