using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectAvatar : MonoBehaviour
{
    public Transform target;
    public float rotateTime = 3.0f;
    public float rotateDegrees = 90.0f;
    private bool rotating = false;
    private int derecha = 0;
    private int izquierda = 0;
    public static int whichCharacter;
    private float rotgradosY = 0f;
    public GameObject ruleta;
    public int mySelectedCharacter;

    private int grados;
    public Text MyText;
    private int gradosint;
    private string minombre;
    public TMP_InputField Minombre;
    public TMP_InputField myCode;
    public GameObject CanvasMessage;

    
    public CodeRequest codeRequest;
    void Start()
    {
        rotgradosY = ruleta.transform.rotation.eulerAngles.y;

        if (gradosint == 40)
        {
            MyText.text = "JUAN";
        }
        else if (gradosint == 310)
        {
            MyText.text = "PEDRO";
        }
        else if (gradosint == 220)
        {
            MyText.text = "SARA";
        }
        else if (gradosint == 130)
        {
            MyText.text = "ANA";
        }
        else
        {
            MyText.text = gradosint.ToString();
        }
    }

    void Update()
    {

        if (derecha == 1 && !rotating)
        {
            StartCoroutine(Rotate(transform, target, Vector3.up, -rotateDegrees, rotateTime));
        }

        if (izquierda == 1 && !rotating)
        {
            StartCoroutine(Rotate(transform, target, Vector3.up, rotateDegrees, rotateTime));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !rotating)
        {
            StartCoroutine(Rotate(transform, target, Vector3.up, -rotateDegrees, rotateTime));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rotating)
        {
            StartCoroutine(Rotate(transform, target, Vector3.up, rotateDegrees, rotateTime));
        }

        gradosint = (int)rotgradosY;

        if (gradosint == 40)
        {
            MyText.text = "DOCTOR";
            mySelectedCharacter = 0;
        }
        else if (gradosint == 310)
        {
            MyText.text = "MILITAR";
            mySelectedCharacter = 1;
        }
        else if (gradosint == 220)
        {
            MyText.text = "ENFERMERA";
            mySelectedCharacter = 3;
        }
        else if (gradosint == 130)
        {
            MyText.text = "DIRECTOR";
            mySelectedCharacter = 2;
        }
        else
        {
            MyText.text = gradosint.ToString();
        }
    }

    public void OnderechaClicked()
    {
        derecha = 1;
    }
    public void OnizquierdaClicked()
    {
        izquierda = 1;
    }
    public void OnInicioClicked()
    {
        minombre = Minombre.text;
        var code = myCode.text;
        PlayerPrefs.SetInt("Mycharacter", mySelectedCharacter);
        PlayerPrefs.SetString("Mynombre", minombre);

        var response = true;//codeRequest.CheckCode(code);

        if (response)
            SceneManager.LoadScene(1);
        else
            ShowMessage();
    }

    private void ShowMessage()
    {
        CanvasMessage.gameObject.SetActive(true);
    }

    private IEnumerator Rotate(Transform camTransform, Transform targetTransform, Vector3 rotateAxis, float degrees, float totalTime)
    {
        if (rotating)
            yield return null;
        rotating = true;

        Quaternion startRotation = camTransform.rotation;
        Vector3 startPosition = camTransform.position;
        // Get end position;
        transform.RotateAround(targetTransform.position, rotateAxis, degrees);
        Quaternion endRotation = camTransform.rotation;
        Vector3 endPosition = camTransform.position;
        camTransform.rotation = startRotation;
        camTransform.position = startPosition;

        float rate = degrees / totalTime;
        //Start Rotate
        for (float i = 0.0f; Mathf.Abs(i) < Mathf.Abs(degrees); i += Time.deltaTime * rate)
        {
            camTransform.RotateAround(targetTransform.position, rotateAxis, Time.deltaTime * rate);
            yield return null;
        }

        camTransform.rotation = endRotation;
        camTransform.position = endPosition;
        rotating = false;

        derecha = 0;
        izquierda = 0;

        rotgradosY = ruleta.transform.rotation.eulerAngles.y;
    }
}
