using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject objBeingDraged;

    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;
    public Transform itemDraggerParent;
    public bool canMove;
    public Vector3 originalPosition;
    public GameObject Oringalparent;
    public void Awake()
    {
        originalPosition = transform.localPosition;
    }
    private void Start()
    {
        canMove = true;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region DragFunctions

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canMove)
            return;

        objBeingDraged = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(itemDraggerParent);
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        objBeingDraged = null;

        canvasGroup.blocksRaycasts = true;
        if (transform.parent == itemDraggerParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }

    #endregion

    private void Update()
    {

    }

    public void ReturnToOriginalPosition()
    {
        transform.parent = Oringalparent.transform;
        //transform.localPosition = originalPosition;
    }
}
