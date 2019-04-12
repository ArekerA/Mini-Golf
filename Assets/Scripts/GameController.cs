using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform ball;
    public Transform ballController;
    public Transform[] levels;
    public Camera mainCamera;
    public Camera[] cameras;
    public int[] pars;
    public Vector3 offset = new Vector3(0, 1.5f, 0);
    public Text TextPar;
    public Text TextCounter;
    public UITableController tableController;
    int counter = 0;
    int currentLvl = 0;
    public GameObject uiGame;
    public GameObject uiPause;
    public GameObject uiEnd;
    public Slider cameraSensitivitySlider;
    public SaveFileManager saveFileManager;

    private string[,] uiTableTexts;


    void Start () {
        foreach (Camera c in cameras)
            c.enabled = false;
        uiPause.SetActive(false);
        StaticUI.isPauseMenu = false;
        if (levels.Length > 0)
        {
            ball.position = levels[0].position + offset;
            ballController.rotation = levels[0].rotation;
            TextPar.text = pars[0].ToString();
            TextCounter.text = "0";

        }
        uiTableTexts = new string[3, levels.Length+1];
        uiTableTexts[0, 0] = "Hole";
        uiTableTexts[1, 0] = "Par";
        uiTableTexts[2, 0] = "Player1";
        for (int i = 0; i < levels.Length; i++)
        {
            uiTableTexts[0, i + 1] = (i + 1).ToString();
            uiTableTexts[1, i + 1] = pars[i].ToString();
            uiTableTexts[2, i + 1] = " ";
        }
        tableController.Texts = uiTableTexts;
        tableController.CreateTable();
        uiEnd.SetActive(false);
        saveFileManager.Load();
        cameraSensitivitySlider.value = StaticUI.cameraSensitivity;

    }

    public void EndLvl()
    {
        uiGame.SetActive(false);
        uiEnd.SetActive(true);
        uiTableTexts[2, currentLvl + 1] = counter.ToString();
        tableController.Texts = uiTableTexts;
        tableController.UpdateTable();
    }
    public void Next()
    {
        uiGame.SetActive(true);
        uiEnd.SetActive(false);
        mainCamera.enabled = true;
        cameras[currentLvl].enabled = false;
        ++currentLvl;
        if (levels.Length > currentLvl)
        {
            ball.position = levels[currentLvl].position + offset;
            ballController.rotation = levels[currentLvl].rotation;
            mainCamera.transform.rotation = Quaternion.Euler(25, 0, 0);
            mainCamera.transform.position = ballController.transform.position - (mainCamera.transform.rotation * mainCamera.GetComponent<CameraController>().offset);
            mainCamera.transform.LookAt(ballController.transform);
            TextPar.text = pars[currentLvl].ToString();
            TextCounter.text = "0";
            counter = 0;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void ResetLevel()
    {
        ball.position = levels[currentLvl].position + offset;
        ballController.rotation = levels[currentLvl].rotation;
        mainCamera.transform.rotation = Quaternion.Euler(25, 0, 0);
        mainCamera.transform.position = ballController.transform.position - (mainCamera.transform.rotation * mainCamera.GetComponent<CameraController>().offset);
        mainCamera.transform.LookAt(ballController.transform);
    }
    public void AddCounter()
    {
        ++counter;
        TextCounter.text = counter.ToString();
    }
    public void ClickPause()
    {
        StaticUI.isPauseMenu = true;
        uiGame.SetActive(false);
        uiPause.SetActive(true);
        Time.timeScale = 0;

    }
    public void ClickPlay()
    {
        StaticUI.isPauseMenu = false;
        uiGame.SetActive(true);
        uiPause.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetSensitivity(float a)
    {
        StaticUI.cameraSensitivity = a;
        saveFileManager.Save();
    }
    public void ChangeCamera()
    {
        if(mainCamera.enabled)
        {
            mainCamera.enabled = false;
            cameras[currentLvl].enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            cameras[currentLvl].enabled = false;
        }
    }

}
