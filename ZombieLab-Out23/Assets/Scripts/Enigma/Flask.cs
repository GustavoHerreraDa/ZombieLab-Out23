using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour, ISelectable
{
    public string Letter;
    public bool isSelected;

    public void Update()
    {
        if (isSelected) OnSelected(); else Deselected();
    }
    public void Deselected()
    {
        isSelected = false;

        if (GetComponent<Outline>() != null)
            GetComponent<Outline>().OutlineWidth = 0;
    }

    public void OnSelected()
    {
        isSelected = true;

        if (GetComponent<Outline>() != null)
            GetComponent<Outline>().OutlineWidth = 10;
    }
}
