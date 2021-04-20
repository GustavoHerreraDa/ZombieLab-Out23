using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;

public class SecuenciaEnigma : MonoBehaviour
{
    public int idEnigm;
    // Start is called before the first frame update
    public GameObject panelSecuence;
    public int actualSecuence;
    public int showSecuence;
    public TextMesh secuenceText;
    public TextMesh countSecuenceText;
    public GameObject[] lights;

    string str_actualSecuence;
    public string str_inputeUserSecuence;
    public int countResolve;

    List<string> inputSecuence;
    public List<string> correctSecuence;

    public int pulse;

    public Color _color = Color.white;
    public GameObject door;


    Coroutine coroutine;

    private AudioSource audioSource;
    public AudioClip audioClip;

    void Awake()
    {

        correctSecuence = new List<string>() {  "Azul;Negro;Negro;Verde;Amarillo;Amarillo;Rosa",
                                                "Marron;Azul;Verde;Azul;Rojo;Rojo;Negro;Rosa;Verde;Negro;Negro",
                                                "Rojo;Rojo;Marron;Azul;Azul;Azul;Verde;Verde;Verde;Marron",
                                                "Rojo;Rojo;Rojo;Rojo;Negro;Negro;Negro;Marron;Amarillo;Rosa",
                                                //"Negro;Rojo;Rosa;Azul;Azul;Amarillo;Verde;Rosa;Verde;Verde;Verde;Negro;Rojo;Azul;Marron;Marron;Amarillo;" +
                                                //"Verde;Azul;Amarillo;Negro;Rosa;Negro;Marron;Rosa;Rosa;Rosa;Amarillo;Negro;Rojo;Rojo;Rojo;Negro;Negro;Verde;Azul;Verde",
                                                "Rojo;Azul;Verde;Rosa;Marron;Azul"};

        inputSecuence = new List<string>() {    "Azul;Negrox2;Verde;Amarillox2;Rosa",
                                                "Marron;Azul;Verde;Azul;Rojox2;Negro;Rosa;Verde;Negrox2",
                                                "Rojox2;Marron;Azulx3;Verde;Verdex2;Marron",
                                                "Rojox4;Negrox2;Negro;Marron;Amarillo;Rosa",
                                                //"Negro;Rojo;Rosa;Azulx2;Amarillo;Verde;Rosa;Verdex3;Negro;Rojo;Azul;Marronx2;Amarillo;" +
                                                //"Verde;Azul;Amarillo;Negro;Rosa;Negro;Marron;Rosax3;Amarillo;Negro;Rojox3;Negrox2;Verde;Azul;Verde",
                                                "Rojo;Azul;Verde;Rosa;Marron;Azul"};

        audioSource = GetComponent<AudioSource>();

        PlaySecuence();
    }

    void PlaySecuence()
    {
        str_actualSecuence = inputSecuence[actualSecuence];

        //var steps = str_actualSecuence.Split(';');

        //for (int x = 0; x < steps.Length; x++)
        //    Debug.Log("PlaySecuence " + x + steps[x]);

        coroutine = StartCoroutine(ShowStepName(0));

        if (actualSecuence > inputSecuence.Count)
            actualSecuence = 0;
    }

    IEnumerator ShowStepName(int position)
    {
        var steps = str_actualSecuence.Split(';');
        var _colorText = Color.white;


        if (position == steps.Length)
        {
            //Debug.LogError("Cambio de secuencia " + position + " " + steps.Length + " " + actualSecuence + " " + inputSecuence.Count);

            if (actualSecuence == inputSecuence.Count - 1)
                actualSecuence = 0;
            else
                actualSecuence++;

            str_actualSecuence = inputSecuence[actualSecuence];
            steps = str_actualSecuence.Split(';');
            position = 0;
        }
        var color = "";

        if (steps[position].Contains("x"))
            color = steps[position].Substring(0, steps[position].Length - 2);
        else
            color = steps[position];

        switch (color)
        {
            case "Azul":
                _color = Color.blue;
                break;
            case "Negro":
                _color = Color.black;
                break;
            case "Amarillo":
                _color = Color.yellow;
                _colorText = Color.black;
                break;
            case "Verde":
                _color = Color.green;
                _colorText = Color.black;
                break;
            case "Marron":
                _color = new Color(0.6886792f, 0.3791021f, 0, 1);
                break;
            case "Rosa":
                _color = new Color(255, 0, 243, 255);
                break;
            case "Rojo":
                _color = Color.red;
                break;
            default:
                Debug.Log("default step " + color);
                break;
        }

        countSecuenceText.text = "";

        if (steps[position].Contains("x2"))
            countSecuenceText.text = "X2";

        if (steps[position].Contains("x3"))
            countSecuenceText.text = "X3";

        if (steps[position].Contains("x4"))
            countSecuenceText.text = "X4";

        countSecuenceText.color = _colorText;

        //Debug.Log("Show Color Name " + steps[position] + actualSecuence);

        panelSecuence.GetComponent<Renderer>().sharedMaterial.color = _color;
        yield return new WaitForSeconds(2f);

        position = position + 1;

        StartCoroutine(ShowStepName(position));

    }

    public void ChangeSecuence(int secuencia)
    {
        if (secuencia < 0)
            actualSecuence--;
        else
            actualSecuence++;

        if (actualSecuence >= inputSecuence.Count)
            actualSecuence = 0;

        if (actualSecuence < 0)
            actualSecuence = 0;



        StopCouroutine();
        PlaySecuence();
    }

    public void AddSecuence(string value)
    {
        var notCorrectSecuence = false;
        str_inputeUserSecuence = str_inputeUserSecuence + value + ";";
        pulse++;

        if (pulse == 50)
        {
            str_inputeUserSecuence = "";
            pulse = 0;
        }

        if (correctSecuence.Count == 0)
            return;

        for (int x = 0; x < correctSecuence.Count; x++)
        {
            //Debug.Log(str_inputeUserSecuence.Substring(0, str_inputeUserSecuence.Length - 1));
            //Debug.Log(correctSecuence[x]);
            if (str_inputeUserSecuence.Substring(0, str_inputeUserSecuence.Length - 1) == correctSecuence[x])
            {
                Debug.Log("Correct Secuence");
                ISFSObject sfso = new SFSObject();
                sfso.PutNull("tele");
                SmartFoxConnection.SFS.Send(new ExtensionRequest("trigger",sfso));
                str_inputeUserSecuence = "";
                lights[countResolve].SetActive(true);
                countResolve++;
                correctSecuence.RemoveAt(x);
                pulse = 0;
                return;
            }
        }

        for (int x = 0; x < correctSecuence.Count; x++)
        {
            if (correctSecuence[x].Contains(str_inputeUserSecuence))
            {
                notCorrectSecuence = true;
            }
        }

        if (!notCorrectSecuence)
        {
            str_inputeUserSecuence = "";
            Debug.Log(" Not Correct Secuence");
        }
    }
    public void StopCouroutine()
    {
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {
        secuenceText.text = (actualSecuence + 1).ToString();

        if (countResolve == inputSecuence.Count)
        {
            EnigmaManager.Instance.CompleteEnigm(idEnigm);
            door.GetComponent<Animator>().SetBool("isOpen", true);
            //door.SetActive(false);
            if (audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
    }

    public void AutoComplete()
    {
        door.GetComponent<Animator>().SetBool("isOpen", true);
    }

    public void ParcialComplete()
    {
        Debug.Log("Correct Secuence");
        str_inputeUserSecuence = "";
        lights[countResolve].SetActive(true);
        countResolve++;
        //correctSecuence.RemoveAt(x); // LALO LUCAS
        pulse = 0;
    }
}
