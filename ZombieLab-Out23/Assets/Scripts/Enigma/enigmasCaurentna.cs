using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;


public class enigmasCaurentna : MonoBehaviour
{
    public playerFps pf, pf2, pf3; //los distintos jugadoes, para teletransportas en caso de ser necesario
    public GameObject[] enigma;
    public GameObject[] cajas;
    public int actual = 0;
    public levelMan lm;
    public GameObject winnerCert, resultadoAcertijo;
    public GameObject c1, c2, c3;//las camaras que cambian dependiendo acertijo
    public Text bloqueTexto;
    //para validar resultados
    private int valor;
    private string premio, respuesta;
    private string qJson, fileName;
    public InputField resultadoUsuario;

    //audio amanger
    public audioManager audioMan;
    bool cambieAudio = false;

    public Camera cameraEnigma;

    void Start()
    {
        //qJson = "barbanegra.json";
        qJson = "barbanegra";
        fileName = Path.Combine(Application.streamingAssetsPath, qJson);
        //Debug.Log(fileName);

        //escondeCajas();
        if (cajas[actual] != null)
            cajas[actual].SetActive(true);
    }

    void escondeCajas()
    {
        //Debug.Log("esconde cajas");
        for (int cx = 0; cx < cajas.Length; cx++)
        {
            cajas[cx].SetActive(false);//esconde todos
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    //print("space key was pressed");
        //    //StartCoroutine(GetRequest(fileName));
        //    //Debug.Log(actual  +" el indice de engima actual");

        //    pf.canMove = true; //desativa script
        //    if (pf2 != null)
        //        pf2.canMove = true; //desativa script en camara #2
        //                            //if (pf3 != null)
        //    if (pf3 != null)
        //        pf3.canMove = true; //desativa script en camara #2

        //    //para moverse de reversa
        //    if (actual >= 8 && pf3 != null)
        //        pf3.deReversa();
        //    if (actual > 8 && pf2 != null)
        //        pf2.deReversa();

        //    if (actual < 4)
        //        pf.deReversa();

        //    //escondeEnigmas();
        //    escondeResultadoCanvas();

        //}

        if (Input.GetKeyDown(KeyCode.X))
        {
            enigma[actual].SetActive(false);
            pf.canMove = true;
        }
    }

    public void soloCamara(int camaraActiva)
    {
        if (c1 != null) c1.gameObject.SetActive(false);
        if (c2 != null) c2.gameObject.SetActive(false);
        if (c3 != null) c3.gameObject.SetActive(false);

        if (camaraActiva == 1)
        {
            c1.gameObject.SetActive(true);
        }
        if (camaraActiva == 2)
        {
            c2.gameObject.SetActive(true);
        }
        if (camaraActiva == 3)
        {
            c3.gameObject.SetActive(true);
        }
    }

    public void callMeEnigma(int enigmaNumber)
    {
        actual = enigmaNumber;
        //Invoke("muestraEnigma",0.2f);
        StartCoroutine(GetRequest(fileName));
    }

    public void final()
    {
        escondeEnigmas();
        winner();//muestra certificado
    }

    public void checaResultado(bool correcto)
    {
        if (correcto == true)
        {
            escondeEnigmas();
            //resultadoAcertijo.gameObject.SetActive(true);
            //bloqueTexto.text = "¡Muy bien hecho! \n\r" + premio;
            CorrectBaulEnigma();
            actual += 1;
            //escondeCajas();
            //cajas[actual].SetActive(true);
            //Debug.Log("voy a moestrar la caja" + actual);
        }
        else
        {
            //resultadoAcertijo.gameObject.SetActive(true);
            bloqueTexto.text = "¡Vuelvelo a intentar! \n\r";
        }

        escondeResultadoCanvas();
    }

    public void checaResultado(int idEnigm)
    {
        EnigmaManager.Instance.CompleteEnigm(idEnigm);

        //InputField resultadoUsuario = FindObjectOfType<InputField>();
        resultadoUsuario = FindObjectOfType<InputField>();
        string usuarioDice = resultadoUsuario.text;
        usuarioDice = usuarioDice.ToLower();
        usuarioDice = String.Concat(usuarioDice.Where(c => !Char.IsWhiteSpace(c)));
        //Debug.Log(usuarioDice +" escribio el usuario");
        escondeEnigmas();
        if (usuarioDice == respuesta)
        {
            resultadoAcertijo.gameObject.SetActive(true);
            bloqueTexto.text = "¡Muy bien hecho! \n\r" + premio;
            CorrectBaulEnigma();
            actual += 1;
            //escondeCajas();
            //cajas[actual].SetActive(true);

            //cambia sonido
            if (actual == 4 && cambieAudio == false)
            {
                audioMan.playAudio2(); //el audio #2}
                cambieAudio = true;
            }
        }
        else
        {
            resultadoAcertijo.gameObject.SetActive(true);
            bloqueTexto.text = "¡Vuelvelo a intentar! \n\r";
        }

        //Debug.Log("voy a moestrar la caja"+actual);

    }

    public void winner()
    {
        //fin del juego
        lm.tmpTime = 14400; //aumenta el tiempo a 4 horas
        winnerCert.gameObject.SetActive(true);
    }

    public void escondeEnigmas()
    {
        //esconde todos los enigmas
        for (int cx = 0; cx < enigma.Length; cx++)
        {
            enigma[cx].SetActive(false);//esconde todos
        }
    }

    public void escondeResultadoCanvas()
    {
        //para que muestre la camara que toca...
        //Debug.Log("para camaras "+actual);
        //if (actual >= 8)
        //    soloCamara(3);
        //if (actual < 8)
        //    soloCamara(2);
        //if (actual < 4)
        //    soloCamara(1);

        playerFps camActiva = GameObject.FindWithTag("Player").GetComponent<playerFps>();
        //camActiva.deReversa();//se regres unos pasos..

        resultadoAcertijo.gameObject.SetActive(false);
        pf.canMove = true; //desativa script

        if (pf2 != null)
            pf2.canMove = true; //desativa script en camara #2

        if (pf3 != null)
            pf3.canMove = true; //desativa script en camara #2
    }


    void disableCam()
    {
        if (pf != null) pf.canMove = false;
        if (pf2 != null) pf2.canMove = false;
        if (pf3 != null) pf3.canMove = false;
    }

    void showOnly(int mostrando)
    {
        //Debug.Log("mostrare "+actual);
        escondeEnigmas();
        disableCam();
        enigma[mostrando].SetActive(true);
        //if (actual == 9)
        if (actual == 9)
        {
            //actual = 0;
            enigma[9].SetActive(true);

        }
        cameraEnigma.enabled = true;
        // else actual += 1;
    }

    IEnumerator GetRequest(string uri)
    {
        yield return new WaitForSeconds(.4f); //ques e espere .2 sgs.
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string json = webRequest.downloadHandler.text;
            ListEnigmas q = JsonUtility.FromJson<ListEnigmas>(json);
            respuesta = q.Enigmas[actual].Clave;
            premio = q.Enigmas[actual].Premio;
            valor = q.Enigmas[actual].Valor;

            //Debug.Log("voya  bsucar el enigma en: "+actual);

        }
        showOnly(actual);
    }


    private void CorrectBaulEnigma()
    {
        cajas[actual].GetComponent<BoxCollider>().enabled = false;
        Debug.Log("CorrectBaulEnigma ");
        if (cajas[actual].GetComponent<baul>().panelLights != null)
        {
            Debug.Log("panelLights ");

            cajas[actual].GetComponent<baul>().panelLights.EnableLight();
        }
    }

    public void CompleteEnigma(int idEnigm)
    {
        cajas[idEnigm].GetComponent<BoxCollider>().enabled = false;
//        cajas[idEnigm].GetComponent<Outline>().OutlineColor = Color.green;
        Debug.Log("CorrectBaulEnigma ");
        if (cajas[idEnigm].GetComponent<baul>().panelLights != null)
        {
            Debug.Log("panelLights ");

            cajas[idEnigm].GetComponent<baul>().panelLights.EnableLight();
        }
    }
}





[Serializable]
public class ListEnigmas
{
    public Enigmas[] Enigmas;
}

[Serializable]
public class Enigmas
{
    public string Titulo;
    public string Acertijo;
    public string Premio;
    public string Clave;
    public int Valor;
    public int TipoAcertijo;
}