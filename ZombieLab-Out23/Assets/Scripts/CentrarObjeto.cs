using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrarObjeto : MonoBehaviour
{
    public bool ocupado;

    public Vector3 centro;
    

    void Start()
    {
        centro = this.transform.position;
        ocupado = false;
    }

    
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    ocupado = true;

    //    if (Input.GetMouseButton(1))
    //    {
    //        other.transform.position = centro;
    //        Debug.Log("listo calisto");


    //    }


    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    ocupado = false;




    //}






}
