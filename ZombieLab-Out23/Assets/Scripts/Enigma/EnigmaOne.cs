using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnigmaOne : MonoBehaviour, IEnigma
{
    public int idEnigma;
    // Start is called before the first frame update
    public GameObject canvasEnigma;
    public GameObject desk;
    public Text textCorrect;
    public GameObject key;
    public GameObject door;
    public GameObject buttom;

    public Material correctMaterial;

    public Transform[] positionsFlask;

    public int currentPosition;

    private AudioSource audioSource;
    public AudioClip audioClip;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void CorrectAswerd()
    {
        EnigmaManager.Instance.CompleteEnigm(idEnigma);
        print("EnigmaOne: " +idEnigma);   
        if (GetComponent<Outline>() != null)
        {
            GetComponent<Outline>().OutlineColor = Color.green;
        }

        if (GetComponent<BoxCollider>() != null)
            GetComponent<BoxCollider>().enabled = false;

        if (desk != null)
            desk.GetComponent<Animator>().SetBool("isOpen", true);

        if (key != null)
            key.SetActive(true);

        if (door != null)
        {
            door.GetComponent<Animator>().SetBool("isOpen", true);

            if(audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            //door.SetActive(false);
        }

        if (correctMaterial != null && buttom != null)
        {
            buttom.GetComponent<Renderer>().sharedMaterial = correctMaterial;
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
    }

    public Transform GetFlaskPosition()
    {
        if (currentPosition >= positionsFlask.Length)
            return GetEmptyTransform();

        var position = positionsFlask[currentPosition];
        currentPosition++;

        return position;
    }

    public Transform GetEmptyTransform()
    {
        for (var x = 0; x < positionsFlask.Length; x++)
        {
            var selectable = positionsFlask[x].gameObject.GetComponentInChildren<ISelectable>();
            if (selectable is null)
                return positionsFlask[x];
        }

        return null;
    }

    public Transform GetDropPosition()
    {
        return GetFlaskPosition();
    }

    public void AutoComplete()
    {
        door.GetComponent<Animator>().SetBool("isOpen", true);
    }
}
