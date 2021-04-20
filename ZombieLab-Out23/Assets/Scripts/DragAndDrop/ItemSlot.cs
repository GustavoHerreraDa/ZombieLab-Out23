using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public GameObject item;
    public FlaskEnigma flaskEnigma;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("ItemSlot " + name);
        if (!item)
        {
            item = DragHandler.objBeingDraged;
            item.transform.SetParent(transform);
            item.transform.position = transform.position;

            var centrarObj = GetComponent<CentrarObjeto>();

            if (centrarObj != null)
            {
                centrarObj.ocupado = true;
                item.GetComponent<DragHandler>().canMove = false;
                item.GetComponent<DragHandler>().enabled = false;
            }
            //flaskEnigma.CheckEnigmaCanvas();
        }
    }

    private void Update()
    {
        if (item != null && item.transform.parent != transform)
        {
            item = null;
        }
    }
}

