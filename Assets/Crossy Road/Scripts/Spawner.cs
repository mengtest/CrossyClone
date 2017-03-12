using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform startPosition = null;

    // Spawn time based variables
    public float delayMin = 1.5f;
    public float delayMax = 5;
    public float speedMin = 1;
    public float speedMax = 4;

    public bool useSpawnPlacement = false;
    public int spawnCountMin = 4;
    public int spawnCountMax = 20;

    private float _lastTime = 0;
    private float _delayTime = 0;
    private float _speed = 0;

    [HideInInspector]
    public GameObject item = null;
    [HideInInspector]
    public bool goLeft = false;
    [HideInInspector]
    public float spawnLeftPos = 0;
    [HideInInspector]
    public float spawnRightPos = 0;

    private void Start() {
        if(useSpawnPlacement) {
            // Generate random number at the start of the game
            int spawnCount = Random.Range(spawnCountMin, spawnCountMax);

            // Spawn the good number of items
            for(int i = 0; i < spawnCount; i++) {
                SpawnItem();
            }
        } else {
            // Randomize the speed
            _speed = Random.Range(speedMin, speedMax);
        }
    }

    private void Update() {
        // If we use the placement, get out of here :D
        if(useSpawnPlacement) return;

        // If the current time is greater than the lastime + delay
        if(Time.time > _lastTime + _delayTime) {
            // Set last time to current time and set delay to a random delay
            _lastTime = Time.time;
            _delayTime = Random.Range(delayMin, delayMax);

            // Spawn  an item
            SpawnItem();
        }
    }

    private void SpawnItem() {
        // Instantiate a gameobject as item
        GameObject obj = Instantiate(item) as GameObject;

        // Set the position to the spawnposition
        obj.transform.position = GetSpawnPosition();

        // Change the game object rotation to 180 if the object is going left
        float direction = 0;
        if(goLeft) {
            direction = 180;
        }

        // If the object is not using the palcement, set the speed and the rotation accordingly
        if(!useSpawnPlacement) {
            obj.GetComponent<Mover>().speed = _speed;
            obj.transform.rotation *= Quaternion.Euler(0, direction, 0);
        }
    }

    private Vector3 GetSpawnPosition() {
        // If the object uses the placement, generate a random position between the spawnLeft and spawnRight position
        if(useSpawnPlacement) {
            int x = (int)Random.Range(spawnLeftPos, spawnRightPos);
            Vector3 pos = new Vector3(x, startPosition.position.y, startPosition.position.z);
            return pos;
        } else {
            // else return the start position of the object
            return startPosition.position;
        }
    }
}
