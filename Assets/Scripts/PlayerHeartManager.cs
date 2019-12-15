using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartManager : MonoBehaviour
{
    public Transform heartImage;
    public PlayerManager script_PlayerManager;
    void Update()
    {
        if(script_PlayerManager.heart< heartImage.childCount)
            heartImage.GetChild(script_PlayerManager.heart).gameObject.SetActive(false);

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
