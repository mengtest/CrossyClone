using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    // In which direction is the object going ?
    public bool goLeft = false;
    public bool goRight = false;
    public bool both = false;

    // Containers of the spawnable gameObjects and positions
    public List<GameObject> items = new List<GameObject>();
    public List<Spawner> spawnersLeft = new List<Spawner>();
    public List<Spawner> spawnersRight = new List<Spawner>();

    private void Start() {
        // Generate a random number and create a gameObject from the items list
        int itemId = Random.Range(0, items.Count);
        GameObject item = items[itemId];

        // Assign a value to the direction
        int direction = Random.Range(0, 2);

        // If direction equals at least 1, then go right
        if(both) {
            goLeft = true;
            goRight = true;
        }else if(direction > 0) {
            goLeft = false;
            goRight = true;
        } else {
            // else, go left
            goLeft = true;
            goRight = false;
        }

        // Loop throught the spawnersLeft lsit and assign the item, the direction, the position and if the gameobject is active
        for(int i = 0; i < spawnersLeft.Count; i++) {
            spawnersLeft[i].item = item;
            spawnersLeft[i].goLeft = goLeft;
            spawnersLeft[i].gameObject.SetActive(goRight);
            spawnersLeft[i].spawnLeftPos = spawnersLeft[i].transform.position.x;
        }

        // Loop throught the spawnersRight lsit and assign the item, the direction, the position and if the gameobject is active
        for(int i = 0; i < spawnersRight.Count; i++) {
            spawnersRight[i].item = item;
            spawnersRight[i].goLeft = goLeft;
            spawnersRight[i].gameObject.SetActive(goLeft);
            spawnersRight[i].spawnRightPos = spawnersRight[i].transform.position.x;
        }
    }

    private void Update() {

    }
}
