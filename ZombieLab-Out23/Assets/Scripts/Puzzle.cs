using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    public int idEnigm;

    public GameObject[] Claves;
    public GameObject[] Pinzas;
    public int LlavePuzle1 = 0;
    public int LlavePuzle2 = 0;
    public int LlavePuzle3 = 0;
    public int Llavestotales = 1;
    public int LlaveGeneral;
    public GameObject Win;
    public GameObject Lose;
    public GameObject tableroAlicate;
    public PanelLights panelLights;

    public GameObject canvasEnigma; 
    public enigmasCaurentna enigmaManag;
    

    Vector3 Pinza1pos, Pinza2pos, Pinza3pos;

    // Start is called before the first frame update
    void Start()
    {
        LlaveGeneral = 0;
        LlavePuzle1 = 0;
        LlavePuzle2 = 0;
        LlavePuzle3 = 0;
        Llavestotales = 1;
        
        Pinza1pos = Pinzas[0].transform.position;
        Pinza2pos = Pinzas[1].transform.position;
        Pinza3pos = Pinzas[2].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool caja1 = Claves[0].GetComponent<CentrarObjeto>().ocupado;
        if (caja1 == true)
        {
            if (Pinzas[1].transform.position == Claves[0].transform.position)
            {
                if (LlavePuzle2 == 0)
                {

                    Debug.Log("oe oe jajax2");
                    LlavePuzle2 = LlavePuzle2 + 1;
                }
            }
        }




        bool caja4 = Claves[3].GetComponent<CentrarObjeto>().ocupado;
        if (caja4 == true)
        {
            if (Pinzas[0].transform.position == Claves[3].transform.position)
            {
                if (LlavePuzle1 == 0)
                {

                    Debug.Log("oe oe jaja");
                    LlavePuzle1 = LlavePuzle1 + 1;
                }


            }
        }

        bool caja5 = Claves[4].GetComponent<CentrarObjeto>().ocupado;
        if (caja5 == true)
        {
            if (Pinzas[2].transform.position == Claves[4].transform.position)
            {
                if (LlavePuzle3 == 0)
                {

                    Debug.Log("oe oe jajax3");
                    LlavePuzle3 = LlavePuzle3 + 1;
                }
            }
        }

        if (LlavePuzle1 + LlavePuzle2 + LlavePuzle3 == 3)
        {
            LlaveGeneral = 3;
            Win.SetActive(true);

             StartCoroutine(WinPuzzle()); //Fer cuando gana 
        }

        if (Llavestotales == 0)
        {
            if (Win.activeSelf == true)
            {
                Lose.SetActive(false);
            }
            else
            {
                Lose.SetActive(true);

            }
            
            StartCoroutine(ReturnPinzasToStartPosition());
            //SceneManager.LoadScene("SampleScene");
        }

        for (int i = 0; i < Pinzas.Length; i++)
        {
            if (Pinzas[0].GetComponent<DragHandler>().canMove == false)
            {
                if (Pinzas[1].GetComponent<DragHandler>().canMove == false)
                {
                    if (Pinzas[2].GetComponent<DragHandler>().canMove == false)
                    {
                        //if (Pinzas[2].transform.position == Claves[i].transform.position)
                        //  {
                        foreach (GameObject Circulo in Claves)
                        {
                            if (Pinzas[2].transform.position == Circulo.transform.position)
                            {
                                Llavestotales = 0;
                            }
                        }

                        //}
                    }
                }
            }
        }
    }

    IEnumerator ReturnPinzasToStartPosition()
    {
        yield return new WaitForSeconds(1.5f);
        Llavestotales = 1;
        Lose.SetActive(false);
        Pinzas[0].transform.position = Pinza1pos;
        Pinzas[1].transform.position = Pinza2pos;
        Pinzas[2].transform.position = Pinza3pos;

        Pinzas[0].GetComponent<DragHandler>().enabled = true;
        Pinzas[0].GetComponent<DragHandler>().canMove = true;

        foreach (var aux in Pinzas)
        {
            aux.transform.SetParent(tableroAlicate.transform);
            aux.GetComponent<DragHandler>().enabled = true;
            aux.GetComponent<DragHandler>().canMove = true;
            aux.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    IEnumerator WinPuzzle()
    {

        yield return new WaitForSeconds(1.5f);
        //panelLights.EnableLight();
        EnigmaManager.Instance.CompleteEnigm(idEnigm);//Fer avisa al server
        enigmaManag.checaResultado(true);//prende las luces del tableo  desaciva los enigmas que son canvas, fijate si podes agregar este
    }

    public void CloseCanvasEnigma()
    {
        canvasEnigma.SetActive(false);
    }

}
