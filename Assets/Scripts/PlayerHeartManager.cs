using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartManager : MonoBehaviour
{
    public Transform heartImage;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            heartImage.GetChild(2).gameObject.SetActive(false);
        }
    }
    public void SetActive(bool set)
    {
        heartImage.gameObject.SetActive(set);
        if (!set)
        {
            for (int i = 0; i < heartImage.childCount; i++)
            {
                heartImage.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
