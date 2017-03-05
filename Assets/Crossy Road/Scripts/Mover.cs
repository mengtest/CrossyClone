using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed = 1.0f;
    public float moveDirection = 0;
    public bool parentOnTrigger = true;
    public bool hitBoxOntrigger = false;
    public GameObject moverObject = null;

    private new Renderer renderer = null;
    private bool isVisible = false;

    private void Start() {
        renderer = moverObject.GetComponent<Renderer>();
    }

    private void Update() {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        IsVisible();
    }

    private void IsVisible() {
        if(renderer.isVisible) {
            isVisible = true;
        }

        if(!renderer.isVisible && isVisible == true) {
            Debug.Log("Remove object. No longer seen by camera");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(parentOnTrigger) {
                other.transform.parent = this.transform;
            }

            if(hitBoxOntrigger) {
                other.GetComponent<PlayerController>().GotHit();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            if(parentOnTrigger) {
                other.transform.parent = null; ;
            }
        }
    }
}
