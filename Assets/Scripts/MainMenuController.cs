using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject loadingScreenPrefab;
    private GameObject loadingScreen;
    public void Rate()
    {
        Application.OpenURL("market://details?id=com.ArkadiuszLudwikowski.MiniGolf");
    }
    public void NextScene()
    {
        loadingScreen = Instantiate(loadingScreenPrefab);
        StartCoroutine(LoadAsyncScene());
    }
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Destroy(loadingScreen);
    }
}
