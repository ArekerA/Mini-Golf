using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    public AudioClip clipMenu;
    public AudioClip[] clips;
    private float volume = 0;
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        audioSource.Play();
    }
    public AudioClip NewClip()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            return clipMenu;
        }
        else
        {
            return clips[(int)(Random.value * clips.Length)];
        }
    }
    public void ChangeAudio()
    {
        audioSource.volume = 1.0f;
        StartCoroutine(PlayAudioFade(NewClip()));
    }
    IEnumerator ChangeAudioClip(float oldClipLenght, AudioClip newClip)
    {
        yield return new WaitForSecondsRealtime(oldClipLenght-1);
        StartCoroutine(PlayAudioFade(newClip));

    }
    IEnumerator PlayAudioFade(AudioClip clip)
    {
        while (volume > 0)
        {
            volume -= 0.05f;
            audioSource.volume = volume < 0 ? 0 : volume;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(ChangeAudioClip(clip.length, NewClip()));
        while (volume < 1)
        {
            volume += 0.05f;
            audioSource.volume = volume > 1 ? 1 : volume;
            yield return new WaitForSecondsRealtime(0.05f);
        }

    }
}
