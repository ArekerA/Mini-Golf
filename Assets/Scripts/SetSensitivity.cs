using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSensitivity : MonoBehaviour
{
    public SaveFileManager saveFileManager;
    public Slider cameraSensitivitySlider;
    private void Start()
    {
        saveFileManager.Load();
        cameraSensitivitySlider.value = StaticUI.cameraSensitivity;
    }
    public void SetSensitivityFloat(float a)
    {
        StaticUI.cameraSensitivity = a;
        saveFileManager.Save();
    }
}
