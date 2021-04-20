using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandlerWithClick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static GameObject objBeingDraged;

    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;
    public Transform itemDraggerParent;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region DragFunctions

    public void OnBeginDrag(PointerEventData eventData)
    {
        objBeingDraged = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(itemDraggerParent);
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag " + name);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        if (Input.GetMouseButtonDown(1))
        {
            objBeingDraged = null;

            canvasGroup.blocksRaycasts = true;
            if (transform.parent == itemDraggerParent)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
            }
        }
    }

    #endregion

    private void Update()
    {
        if (objBeingDraged != null)
            transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(1) && objBeingDraged != null)
        {
            objBeingDraged = null;

            canvasGroup.blocksRaycasts = true;
            if (transform.parent == itemDraggerParent)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (objBeingDraged is null)
        //{
        //    objBeingDraged = gameObject;

        //    startPosition = transform.position;
        //    startParent = transform.parent;
        //    transform.SetParent(itemDraggerParent);
        //    canvasGroup.blocksRaycasts = false;
        //}
    }
}
