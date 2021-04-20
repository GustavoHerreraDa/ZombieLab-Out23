using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSound : MonoBehaviour
{
    public AudioClip[] clipIntro;
    private AudioSource audioSource;
    private int audioCount;
    public GameObject _3Dnumbers;
    public bool IsIntroSound;
    public bool CanPlay;
    public audioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioCount = 0;
        audioSource.clip = clipIntro[audioCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanPlay)
            return;

        if (!audioSource.isPlaying)
        {
            if (audioCount == clipIntro.Length - 1)
            {
                CanPlay = false;
                if (audioManager != null)
                    audioManager.asrc.volume = 1f;

                if (IsIntroSound)
                {
                    _3Dnumbers.SetActive(true);
                    Invoke("NextScene", 15f);
                }
                return;

            }

            audioCount++;
            audioSource.clip = clipIntro[audioCount];
            audioSource.Play();
        }
    }

    public void RepeatIntroSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        if (audioManager != null)
            audioManager.asrc.volume = 0.2f;

        audioCount = 0;
        CanPlay = true;
        audioSource.clip = clipIntro[audioCount];
        audioSource.Play();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(2);
    }
}
