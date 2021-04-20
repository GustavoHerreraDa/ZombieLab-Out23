using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxDrop : MonoBehaviour, IEnigma
{
    public Transform myDropPosition;
    public Animator box_animator;
    public void CorrectAswerd()
    {

    }

    public Transform GetDropPosition()
    {
        return myDropPosition;
    }

    public void OpenCloseBox(bool close)
    {
        if (box_animator != null)
        {
            box_animator.SetBool("isClose", close);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
