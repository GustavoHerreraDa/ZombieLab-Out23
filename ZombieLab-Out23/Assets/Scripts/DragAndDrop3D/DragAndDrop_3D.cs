using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragAndDrop_3D : MonoBehaviour
{
    //Initialize Variables
    GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    public GameObject selected;
    private GameObject carryObject;
    public Transform objectInPosition;
    public Quaternion rotationObjectInPosition;
    public Transform objectToSeePosition;
    public bool isCarryObject;
    public Text textDescripcion;
    public GameObject Arrows;
    private playerFps player;
    public bool isPause;

    public Camera cameraMain;
    // Use this for initialization
    void Start()
    {
        rotationObjectInPosition = objectInPosition.rotation;
        rotationObjectInPosition.x = 0;
        rotationObjectInPosition.y = 0;
        rotationObjectInPosition.z = 0;
        rotationObjectInPosition.w = 0;
        Arrows.SetActive(false);
        player = GetComponent<playerFps>();

    }

    void Update()
    {
        if (isPause)
            return;

        RaycastHit hitInfo;
        getTarget = ReturnClickedObject(out hitInfo);

        if (getTarget is null)
            return;

        //Debug.Log("get target " + getTarget.name);

        if (textDescripcion != null)
            textDescripcion.text = "";

        if (selected != null)
        {
            if (getTarget != selected && selected.gameObject.tag == "Draggable"
                || selected.gameObject.tag == "ItemVisible"
                || selected.gameObject.tag == "Keyboard"
                || selected.gameObject.tag == "ItemToSee"
                )
                selected.GetComponent<ISelectable>().Deselected();
            //if (getTarget != selected && selected.gameObject.tag == "Droppeable") selected.GetComponent<ISelectable>().Deselected();

        }

        selected = getTarget;

        OnMouseOnObject();

        if (isCarryObject)
            DropSelecteableObject();
    }

    private void OnMouseOnObject()
    {
        if (selected.gameObject.tag == "Draggable")
        {
            selected.GetComponent<ISelectable>().OnSelected();
            selected = getTarget;
            if (textDescripcion != null)
            {
                var flask = selected.GetComponent<Flask>();
                textDescripcion.text = flask ? flask.Letter : "";
            }
            GetSelecteableObject();
            return;
        }

        if (selected.gameObject.tag == "ItemVisible")
        {
            selected.GetComponent<ISelectable>().OnSelected();
            selected = getTarget;
            if (textDescripcion != null)
            {
                var flask = selected.GetComponent<Flask>();
                textDescripcion.text = flask ? flask.Letter : "";
            }
        }
        if (selected.gameObject.tag == "Keyboard")
        {
            selected.GetComponent<ISelectable>().OnSelected();
            PressKeyboardKey();

            if (textDescripcion != null)
            {
                var flask = selected.GetComponent<Flask>();
                textDescripcion.text = flask ? flask.Letter : "";
            }
        }

        if (selected.gameObject.tag == "ItemToSee")
        {
            selected.GetComponent<ISelectable>().OnSelected();
            GetSelecteableObject();
        }
        //StartCoroutine("DeselectSelectedObject");
    }

    private void GetSelecteableObject()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isCarryObject) { ReplaceSelecteableObject(); return; }

            carryObject = selected;
            carryObject.transform.position = objectInPosition.position;
            carryObject.transform.rotation = objectInPosition.rotation;
            carryObject.transform.SetParent(objectInPosition);

            carryObject.GetComponent<ISelectable>().Deselected();
            isCarryObject = true;
            //GET IN
            selected = null;

            if (carryObject.tag == "ItemToSee")
            {
                carryObject.GetComponent<BoxCollider>().enabled = false;

                var rotationVector = objectInPosition.transform.rotation.eulerAngles;
                rotationVector.y = 180;
                rotationVector.x = 90;
                objectInPosition.transform.Rotate(rotationVector, Space.Self);
                Arrows.SetActive(true);
                player.ChangeCameraToFPS();
                player.canMove = false;
            }
        }
    }

    private void PressKeyboardKey()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var safe = selected.GetComponentInParent<HotelSafeHandler>();

            if (safe != null) { safe.HandleKeyPress(selected.GetComponent<ButtonHandler>().keyCode); return; }

            var secuentialButton = selected.GetComponentInParent<SecuentialButton>();

            if (secuentialButton != null)
                secuentialButton.ButtonUP();
        }
    }

    private void ReplaceSelecteableObject()
    {
        var auxSelected = carryObject;

        carryObject = selected;

        auxSelected.transform.position = selected.transform.position;
        auxSelected.transform.SetParent(selected.transform.parent);

        carryObject.transform.position = objectInPosition.position;
        carryObject.transform.SetParent(objectInPosition);
        carryObject.GetComponent<ISelectable>().Deselected();

        isCarryObject = true;
         //Get Change
        selected = null;
    }

    private void DropSelecteableObject()
    {
        RaycastHit hitInfo;
        getTarget = ReturnClickedObject(out hitInfo);


        if (Input.GetMouseButtonUp(1))
        {
            if (carryObject.gameObject.tag == "ItemToSee" && carryObject != null)
            {
                var positionPlace = carryObject.GetComponent<ItemToSee>().GetDropPosition();
                if (positionPlace != null)
                {
                    carryObject.transform.SetParent(null);
                    carryObject.GetComponent<BoxCollider>().enabled = true;
                    carryObject.transform.position = positionPlace.position;
                    carryObject.transform.rotation = positionPlace.rotation;
                    carryObject = null;
                    isCarryObject = false;
                    print("isCarryObject = false");
                    Arrows.SetActive(false);
                    player.ChangeCameraToShoulder();
                    player.canMove = true;

                    objectInPosition.transform.rotation = rotationObjectInPosition;
                }
            }

            if (getTarget.tag == "Droppeable" && carryObject != null)
            {
                var positionPlace = getTarget.GetComponent<IEnigma>().GetDropPosition();

                if (positionPlace != null)
                {
                    carryObject.transform.position = positionPlace.position;
                    carryObject.transform.rotation = positionPlace.rotation;

                    print("NNNNNNNNNNNNNNNNAAAAMEEEEEEE: " + carryObject.name);

                    if (!carryObject.name.Contains("Flask") && !carryObject.name.Contains("cube"))
                    {
                        Spawner.Instance.SendPositionObject(carryObject.transform, getTarget.transform);
                    }

                    carryObject.transform.SetParent(positionPlace);
                    isCarryObject = false;
                    //GET OUT

                }
            }

            if (getTarget.tag == "Lock" && carryObject != null)
            {

                var key = carryObject.GetComponent<Key>();
                var lockO = getTarget.GetComponent<Lock>();

                if (key != null && lockO != null)
                    lockO.CheckAnswerd(key.KeyLock);
                isCarryObject = false;
                print("isCarryObject = false");
                Destroy(carryObject);
                carryObject = null;
            }
        }
    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = cameraMain.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, 10, LayerMask.NameToLayer("NormalPropObject")))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }



    private IEnumerator DeselectSelectedObject()
    {
        if (selected.GetComponent<ISelectable>() != null)
            selected.GetComponent<ISelectable>().Deselected();

        yield return new WaitForSeconds(1f);
    }
}
