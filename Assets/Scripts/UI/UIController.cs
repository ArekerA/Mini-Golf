using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    public Image progressBarForegroundImage;

    
    public void SetFillAmount(float x)
    {
        progressBarForegroundImage.fillAmount = x;
    }
}