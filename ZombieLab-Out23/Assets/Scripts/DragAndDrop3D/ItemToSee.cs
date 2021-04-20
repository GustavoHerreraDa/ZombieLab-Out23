using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSee : MonoBehaviour, ISelectable
{
    // Start is called before the first frame update
    public Transform originalPosition;
    public Vector3 originalPositionV3;
    public Quaternion originalRotation;

    private bool isSelected;
    void Start()
    {
        originalPositionV3 = this.transform.position;
        originalRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<BoxCollider>().enabled = !isSelected;

    }

    public Transform GetDropPosition()
    {
        originalPosition.position = originalPositionV3;
        originalPosition.rotation = originalRotation;
        return originalPosition;
    }

    public void Deselected()
    {
        isSelected = false;
        GetComponent<Outline>().OutlineWidth = 0;
    }

    public void OnSelected()
    {
        isSelected = true;
        GetComponent<Outline>().OutlineWidth = 10;
    }
}
