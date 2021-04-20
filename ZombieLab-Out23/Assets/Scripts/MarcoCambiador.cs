using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarcoCambiador : MonoBehaviour
{
    public GameObject Marco;
    public Sprite Marco1;
    public Sprite Marco2;
    public Sprite Marco3;
    public Sprite Marco4;
    public Sprite Marco5;
    public Sprite Marco6;
    public Sprite Marco7;
    public Sprite Marco8;
    public Sprite MarcoImg;

    public AudioClip AudioMarco1;
    public AudioClip AudioMarco2;
    public AudioClip AudioMarco3;
    public AudioClip AudioMarco4;
    public AudioClip AudioMarco5;
    public AudioClip AudioMarco6;
    public AudioClip AudioMarco7;
    public AudioClip AudioMarco8;

    private AudioSource audioSource;
    public int NumeroMark;

   void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Marco.GetComponent<Image>().sprite = Marco1;
        NumeroMark = 1;
    }

    void Update()
    {
       Marco.GetComponent<Image>().sprite = MarcoImg;
    }
   public void CambioMarco1()
    {
        //Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco1 != null)
            audioSource.clip = AudioMarco1;
        audioSource.Play();

        MarcoImg = Marco1;
        NumeroMark = 1;
    }
    public void CambioMarco2()
    {
        // Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco2 != null)
            audioSource.clip = AudioMarco2;
        audioSource.Play();

        MarcoImg = Marco2;
        NumeroMark = 2;
    }
    public void CambioMarco3()
    {
        //Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco3 != null)
            audioSource.clip = AudioMarco3;
        audioSource.Play();
        MarcoImg = Marco3;
        NumeroMark = 3;
    }
    public void CambioMarco4()
    {
        //Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco4 != null)
            audioSource.clip = AudioMarco4;
        audioSource.Play();

        MarcoImg = Marco4;
        NumeroMark = 4;
    }
    public void CambioMarco5()
    {
        //Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco5 != null)
            audioSource.clip = AudioMarco5;
        audioSource.Play();

        MarcoImg = Marco5;
        NumeroMark = 5;
    }
    public void CambioMarco6()
    {
        // Sprite MarcoImg = Marco.GetComponent<Image>().sprite;

        if (AudioMarco6 != null)
            audioSource.clip = AudioMarco6;
        audioSource.Play();

        MarcoImg = Marco6;
        NumeroMark = 6;
    }
    public void CambioMarco7()
    {
        //Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco7 != null)
            audioSource.clip = AudioMarco7;
        audioSource.Play();

        MarcoImg = Marco7;
        NumeroMark = 7;
    }
    public void CambioMarco8()
    {
        //Sprite MarcoImg = Marco.GetComponent<Image>().sprite;
        if (AudioMarco8 != null)
            audioSource.clip = AudioMarco8;
        audioSource.Play();

        MarcoImg = Marco8;
        NumeroMark = 8;
    }

}
