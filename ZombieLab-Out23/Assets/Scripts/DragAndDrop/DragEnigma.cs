using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragEnigma : MonoBehaviour, IEnigma
{
    public Text textCorrect;

    public GameObject canvasEnigma;
    public GameObject desk;
    public GameObject key;
    public GameObject door;
    public PanelLights panelLights;
    public GameObject nextCanvas;
    private AudioSource audioSource;
    public audioManager audioManager;
    public AudioClip audioClip;

    public List<DragHandler> dragHanlers;
    public void CorrectAswerd()
    {
        if (desk != null)
            desk.GetComponent<BoxCollider>().enabled = false;

        if (canvasEnigma != null)
            canvasEnigma.SetActive(false);

        if (key != null)
            key.SetActive(true);

        if (door != null)
            door.SetActive(false);

        if (panelLights != null)
            panelLights.EnableLight();

        if (nextCanvas != null) {
            canvasEnigma.SetActive(false);
            nextCanvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            CloseCanvasEnigma();
        }
        //textCorrect.gameObject.SetActive(true);
    }

    public void OpenCanvasEnigma()
    {
        canvasEnigma.SetActive(true);
    }

    public void CloseCanvasEnigma()
    {
        canvasEnigma.SetActive(false);
        nextCanvas.SetActive(false);
    }

    public Transform GetDropPosition()
    {
        Debug.LogError("GetDropPosition return null in DragEnigma");
        return null;
    }

    public void ResetPosition()
    {
        foreach(DragHandler drag in dragHanlers)
        {
            drag.ReturnToOriginalPosition();
        }
    }
}
