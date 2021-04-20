using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //        Debug.Log("colision con el baul!");
        if (other.tag == "Player")
        {
            Debug.Log("Entro el player");
            var player = other.GetComponent<playerFps>();
            if (player.hasKey)
            {
                Debug.Log("y tiene llave");
                player.hasKey = false;
                animator.SetTrigger("Open");
            }

        }
    }
}
