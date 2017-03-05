using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    public PlayerController playerController = null;

    private Animator animator = null;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(playerController.state == PlayerController.State.Dead) {
            animator.SetBool("dead", true);
        }

        if(playerController.state == PlayerController.State.Moving) {
            animator.SetBool("move", true);
        }else {
            animator.SetBool("move", false);
        }

        if(playerController.state != PlayerController.State.Idle) {
            return;
        }
    }
}
