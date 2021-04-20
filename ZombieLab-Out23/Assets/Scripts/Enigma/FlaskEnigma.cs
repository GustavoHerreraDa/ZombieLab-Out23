using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskEnigma : MonoBehaviour
{
    public int idEnigm;
    // Start is called before the first frame update
    [SerializeField]
    private string correctResponse = "";
    public GameObject enigmaOneGameObject;
    public IEnigma enigmaOne;
    public bool isResolve;
    public bool isBoxEnigma;
    public bool isLastEnigma;

    public string actualResponse = "";
    public bool checkResponse;
    public Text displayText;

    public DragEnigma dragEnigma;
    void Start()
    {
        enigmaOne = enigmaOneGameObject.GetComponent<IEnigma>();
    }

    private void Update()
    {
        if (!isResolve && checkResponse)
            CheckEnigma();
    }

    public void CheckEnigma()
    {
        var flaskInChildren = GetComponentsInChildren<Flask>();
        actualResponse = "";

        CloseBox(true);

        for (int cx = 0; cx < flaskInChildren.Length; cx++)
        {
            actualResponse += flaskInChildren[cx].Letter;
        }

        if (actualResponse.Replace(" ", "") == correctResponse.Trim())
        {
            enigmaOne.CorrectAswerd();

            for (int cx = 0; cx < flaskInChildren.Length; cx++)
            {
                flaskInChildren[cx].GetComponent<BoxCollider>().enabled = false;
            }
            isResolve = true;
        }

        if (isBoxEnigma)
        {
            Debug.Log("is Box enigma " + isResolve);
            CloseBox(isResolve);
            //var manager = FindObjectOfType<enigmasCaurentna>();
            //manager.winner();
        }

        if (displayText != null)
        {
            displayText.text = isResolve ? "Orden Correcto" : "Orden Incorrecto";
        }
    }

    public void CheckEnigmaCanvas()
    {
        var flaskInChildren = GetComponentsInChildren<Flask>();
        actualResponse = "";

        for (int cx = 0; cx < flaskInChildren.Length; cx++)
        {
            actualResponse += flaskInChildren[cx].Letter;
        }

        if (actualResponse == correctResponse)
        {
            EnigmaManager.Instance.CompleteEnigm(idEnigm);
            print("Flask: " + idEnigm);


            Debug.Log("RESPUESTA CORRECTA");
            enigmaOne.CorrectAswerd();

            isResolve = true;
        }
    }

    public void CloseBox(bool isClose)
    {
        var boxInChildren = GetComponentsInChildren<BoxDrop>();

        for (int cx = 0; cx < boxInChildren.Length; cx++)
        {
            boxInChildren[cx].OpenCloseBox(isClose);
        }
    }

    public void CloseBox()
    {
        var boxInChildren = GetComponentsInChildren<BoxDrop>();

        for (int cx = 0; cx < boxInChildren.Length; cx++)
        {
            boxInChildren[cx].OpenCloseBox(true);
        }
    }
    
    public void AutoComplete()
    {
        enigmaOne = enigmaOneGameObject.GetComponent<IEnigma>();
        enigmaOne.CorrectAswerd();
        var flaskInChildren = GetComponentsInChildren<Flask>();

        for (int cx = 0; cx < flaskInChildren.Length; cx++)
        {
            flaskInChildren[cx].GetComponent<BoxCollider>().enabled = false;
        }
        isResolve = true;
        dragEnigma.CloseCanvasEnigma();
    }
}
