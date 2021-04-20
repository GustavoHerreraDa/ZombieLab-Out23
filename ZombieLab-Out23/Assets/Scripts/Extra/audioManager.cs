using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip a1, a2;
    public AudioClip soundSilence;
    public AudioSource asrc;
    
    private bool isSilence;
    
    void Start()
    {
        playAudio1();
    }


    public void playAudio1()
    {
        asrc.clip = a1;
        asrc.Play();
        //        Debug.Log("calling audio1");
    }

    public void playAudio2()
    {
        asrc.Pause();
        asrc.clip = a2;
        asrc.Play();
    }

    public void Silence()
    {
        isSilence = !isSilence;

        if (isSilence) {
            //asrc.clip = soundSilence;
            //asrc.Play();
            //StartCoroutine(WaitSilence());
            asrc.volume = 0;
        }
        else
            asrc.volume = 1;
    }

    //IEnumerator WaitSilence()
    //{
    //    yield return new WaitForSeconds(soundSilence.length);
    //    asrc.volume = 0;

    //}
}
