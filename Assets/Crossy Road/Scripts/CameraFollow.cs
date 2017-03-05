using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public bool autoMove = true;
    public float speed = 0.5f;
    public GameObject player = null;
    public Vector3 offset = new Vector3(2.5f, 5, -3);

    private Vector3 _depth = Vector3.zero;
    private Vector3 _position = Vector3.zero;
	
	private void Update () {
        // If we can't play, get out of here :D
        if(!Manager.instance.CanPlay()) return;

        // Position between the camera and the palyer
        _position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime);

        if(autoMove) {
            _depth = transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            transform.position = new Vector3(_position.x, offset.y, _depth.z);
        } else {
            transform.position = new Vector3(_position.x, offset.y, _position.z);
        }
	}
}
