using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 5;
    
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime* speed);

        if (transform.position.y > 5)
        {
            Destroy(gameObject);
        }
    }
}
