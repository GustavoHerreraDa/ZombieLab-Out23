using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelMan : MonoBehaviour
{
    public float tmpTime;

    int minutes, seconds;
    //public Text timerText;
    public TextMeshProUGUI timerText;
    public GameObject panelTiempoFuera;

    public int valorMonedas;
    public int acertijoActual = 0;
    public GameObject panelTiempo;
    public playerFps pf, pf2, pf3;//3 CAMARAS
    private string lesSeconds;
    public int roomNumber;
    public bool hasKey;

    public void incrementaMonedas(int numero)
    {
        //va quitando y/o poniendo baules...
        //baules[acertijoActual].SetActive(false);//desaparece actual
        acertijoActual += 1;
        //baules[acertijoActual].SetActive(true);//pone el nuevo

        valorMonedas += numero;
        if (valorMonedas == 100)
        {
            //fin del juego
        } //la suma de todas // fin del juego, ganaste. mostrar certificado.
    }

    private void Awake()
    {
        roomNumber = 1;
    }

    void Start()
    {
        //tmpTime = 3600;

    }

    void Update()
    {
        tmpTime = tmpTime - Time.deltaTime;
        minutes = (int)tmpTime / 60;
        seconds = (int)tmpTime % 60;
        timerText.text = minutes + ":" + seconds;

        if (seconds < 10)
        {
            lesSeconds = "0" + seconds;
            timerText.text = minutes + ":" + lesSeconds;
        }

        //if (minutes == 0 && seconds <= 0)
        if (seconds <= 0 && minutes <= 0)
        {
            panelTiempo.SetActive(false);
            timerText.text = "";
            pf.canMove = false;
            if (pf2 != null)
                pf2.canMove = false; //camara 2 por si el tiempo se acabo en isla
            tiempoAgotado();
        }
    }

    void tiempoAgotado()
    {
        panelTiempoFuera.SetActive(true);
    }
}
