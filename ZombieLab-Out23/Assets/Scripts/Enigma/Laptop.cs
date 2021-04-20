using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laptop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelLogin;
    public GameObject panelEnigmaFlags;

    public InputField fieldPassword;

    public string correctAswerd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            QuitLaptopEnigma();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            panelLogin.gameObject.SetActive(true);
        }
    }

    public void CheckLoginUserPass()
    {
        if (fieldPassword.text == correctAswerd)
            panelEnigmaFlags.gameObject.SetActive(true);
    }

    public void QuitLaptopEnigma()
    {
        panelLogin.gameObject.SetActive(false);
        panelEnigmaFlags.gameObject.SetActive(false);
    }
}
