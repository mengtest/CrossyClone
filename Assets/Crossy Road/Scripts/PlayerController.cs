using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1;

    // The player different states
    public enum State {
        Idle, Dead, Moving
    }

    public State state;
    public ParticleSystem particle = null;
    public GameObject chick = null;

    private Vector3 movement;
    private new Renderer renderer = null;
    private bool isVisible = false;

    private void Start() {
        renderer = chick.GetComponent<Renderer>();
        state = State.Idle;
    }

    private void Update() {
        // If we can't play, get out of here :D
        if(!Manager.instance.CanPlay()) return;

        if(state == State.Dead) return;
        Idle();
        IsVisible();
    }

    private void Idle() {
        if(state == State.Idle) {
            if(Input.GetKeyUp(KeyCode.Z)) {
                Rotate(new Vector3(0, 0, 0));
                movement = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
                // If the player moved formward, increase the scrore
                SetMoveForwardState();
            } else if(Input.GetKeyUp(KeyCode.S)) {
                Rotate(new Vector3(0, 180, 0));
                movement = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance);
            } else if(Input.GetKeyUp(KeyCode.D)) {
                Rotate(new Vector3(0, 90, 0));
                movement = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
            } else if(Input.GetKeyUp(KeyCode.Q)) {
                Rotate(new Vector3(0, -90, 0));
                movement = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
            }
            Move(movement);
        }
    }

    private void CheckIfCanMove() {
        // Raycast - Find if there is a collider box in front of player
        RaycastHit hit;
        Physics.Raycast(transform.position, -chick.transform.up, out hit, colliderDistCheck);

        // Debug.DrawRay(transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2.0f);

        if(hit.collider == null) {
            state = State.Moving;
        } else {
            if(hit.collider.tag == "collider") {
                Debug.Log("Hit something with collider tag");
            } else {
                state = State.Moving;
            }
        }

        
    }

    private void Rotate(Vector3 euler) {
        // Rotate the player in the right direction
        gameObject.transform.rotation = Quaternion.Euler(euler);

        // Check if player can move
        CheckIfCanMove();
    }

    private void Move(Vector3 pos) {
        // Checks if the fplayer can move and then move the player
        if(state == State.Moving) {
            // Use LeanTween to move the player and use the callback to MoveComplete
            LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(MoveComplete);
        }
    }

    private void MoveComplete() {
        // Set juping to false and idle to true
        state = State.Idle;
    }

    private void SetMoveForwardState() {
        Manager.instance.UpdateDistanceCount();
    }

    private void IsVisible() {
        if(renderer.isVisible) {
            isVisible = true;
        }

        if(!renderer.isVisible && isVisible == true) {
            Debug.Log("Player off screen. Apply GotHit()");
            GotHit();
        }
    }

    public void GotHit() {
        state = State.Dead;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;

        // Show the GameOver screen
        Manager.instance.GameOver();
    }
}
