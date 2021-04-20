using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, ISelectable
{
    // Start is called before the first frame update
    public bool isSelected;
    public string KeyLock;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Deselected()
    {
        isSelected = false;

        GetComponent<Outline>().OutlineWidth = 0;
    }

    public void OnSelected()
    {
        Debug.Log("Key On Select");
        isSelected = true;
        GetComponent<Outline>().OutlineWidth = 10;
    }
}
