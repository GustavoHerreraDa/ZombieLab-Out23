using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private playerFps playerController;
    public Animator animator;
    void Start()
    {
        playerController = GetComponent<playerFps>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimControl();
    }

    private void AnimControl()
    {
        Spawner.Instance.SendAnimWalk("X", playerController.curSpeedX, "Y", playerController.curSpeedY);

        animator.SetFloat("X", playerController.curSpeedX);
        animator.SetFloat("Y", playerController.curSpeedY);

        if (Input.GetButtonDown("Jump"))
        {
            Spawner.Instance.SendAnimJump("Jump");
            animator.SetTrigger("Jump");
        }
        //if (playerController.moveDirection == Vector3.zero)
        //    animator.SetBool("is_Walking", false);
        //else
        //    animator.SetBool("is_Walking", true);
    }
}
