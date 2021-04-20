using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class panel_ctl : MonoBehaviour
{
    //public Text pantalla;
    public TextMeshProUGUI pantalla;
    public enigmasCaurentna enigma;
    private string pantallaPq;
    private int layer_mask;
    public Camera camaraAUsar;
    public enigmasCaurentna enig;

    void Start()
    {
        layer_mask = LayerMask.GetMask("candadosUI");
        //Debug.Log("el layer usado es "+layer_mask);
    }

    void Update(){
        
        if (Input.GetKeyDown("space"))
        {
            //print("space key was pressed");
            //StartCoroutine(GetRequest(fileName));
            //Debug.Log(actual  +" el indice de engima actual");
            enig.escondeEnigmas();
            enig.escondeResultadoCanvas();
            //pf3.canMove = true; //desativa script en camara #2
            
        }
        
        if (Input.GetMouseButtonDown(0)){
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = camaraAUsar.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            
            if (Physics.Raycast(ray, out hit, 150, layer_mask))
            {
                string nombreTecla = hit.collider.gameObject.name;
                if (nombreTecla == "clear")
                    limpiaPantalla();
                else if (nombreTecla == "enter")
                    revisa();
                else
                {
                 //   Debug.Log("click en "+nombreTecla);
                letra(nombreTecla);
                }
            }
        }
    }
    
    void letra(string letr)
    {
        pantallaPq += letr;
        //max 9 chars..
        if (pantallaPq.Length > 8)
        {
            pantallaPq=pantallaPq.Substring(0, 8); 
        }
        pantalla.text = pantallaPq;
    }

    void limpiaPantalla()
    {
        pantallaPq = "";
        pantalla.text = pantallaPq;
    }

    void revisa()
    {
         pantallaPq = pantallaPq.ToLower();
         Debug.Log("llamando a revisa..." + pantallaPq);
        if (pantallaPq == "satanas")
            enigma.final();
        /*else 
            enigma.checaResultado();*/
    }
}