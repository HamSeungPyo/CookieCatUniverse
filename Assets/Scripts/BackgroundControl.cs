using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundControl : MonoBehaviour
{
    public RectTransform background;
    float backgroundMoveSpeed = 1;
    private void Awake()
    {
        background = GetComponent<RectTransform>();
        StartCoroutine(BackgroundMove());
    }
    IEnumerator BackgroundMove()
    {
        while (true)
        {
            background.Translate(Vector3.down * backgroundMoveSpeed * Time.deltaTime);

            if (background.localPosition.y <= -800)
            {
                background.localPosition = Vector3.zero;
            }
            yield return null;
        }
    }
}
