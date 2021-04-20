using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    public Vector3 startPosition;



    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = false;
    public bool movible = true;
    public List<GameObject> Pinzas = new List<GameObject>();
    public Camera cameraEnigma;

    public LayerMask hitLayers;

    private void Start()
    {

        startPosition = transform.position;
        //usado = false;
        movible = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position == startPosition)
        {
            movible = true;
        }

        if (isBeingHeld == true)
        {
            Debug.Log("isBeingHeld");
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cameraEnigma.ScreenPointToRay(mouse);
            RaycastHit hitInfo;
            var getTarget = ReturnClickedObject(out hitInfo);

            transform.localPosition = new Vector2(hitInfo.point.x, hitInfo.point.y);

            movible = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isBeingHeld = false;
            //usado = true;
        }

    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Vector3 mouse = Input.mousePosition;
        Ray ray = cameraEnigma.ScreenPointToRay(mouse);
        //
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            target = hit.collider.gameObject;
        }
        Debug.Log(target.name);
        return target;
    }


    private void OnMouseDown()
    {
        Debug.Log(this.name + " OnMouseDown");

        if (movible == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 MousePos;
                MousePos = Input.mousePosition;
                MousePos = cameraEnigma.ScreenToWorldPoint(MousePos);

                isBeingHeld = true;


                startPosX = MousePos.x - this.transform.localPosition.x;
                startPosY = MousePos.y - this.transform.localPosition.y;

            }
        }


    }
    private void OnMouseUp()
    {
        Debug.Log(this.name + " On Mouse Up");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (this.transform.position == collision.transform.position)
        {

            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                this.transform.position = startPosition;
            }
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (this.transform.position == collision.transform.position)
        {

            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                this.transform.position = startPosition;
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CentrarObjeto>().ocupado != true)
        {
            Debug.Log("esta free");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CentrarObjeto>().ocupado != true)
        {
            Debug.Log("esta free");
        }
    }

}
