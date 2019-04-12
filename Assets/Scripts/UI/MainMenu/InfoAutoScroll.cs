using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoAutoScroll : MonoBehaviour
{
    public float speed = 1f;
    public RectTransform content;
    RectTransform rectTransform;
    void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
        content.anchoredPosition = Vector2.zero;
    }
	void Update () {
        content.anchoredPosition += Vector2.up* speed;
        if(content.anchoredPosition.y > (rectTransform.rect.height + content.rect.height))
        {
            content.anchoredPosition = Vector2.zero;
        }
    }
}
