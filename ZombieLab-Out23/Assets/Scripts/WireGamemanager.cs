using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireGamemanager : MonoBehaviour
{
    [SerializeField] private int totalWires;
    private int wiresInPosition;
    public enigmasCaurentna enigmaManag;

    public GameObject cam;

    public void CheckWiresPositions()
    {
        wiresInPosition ++;

        if (wiresInPosition >= totalWires)
        {
            Debug.Log("Ganaste el Juego de cables");
            EnigmaManager.Instance.CompleteEnigm(5);//Fer avisa al server
            enigmaManag.checaResultado(true);//prende las luces del tableo  desaciva los enigmas que son canvas, fijate si podes agregar este
            cam.SetActive(false);
        }
        else
            Debug.Log("Segui participando papá");
        
    }
}
