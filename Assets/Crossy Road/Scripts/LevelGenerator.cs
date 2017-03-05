using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> platform = new List<GameObject>();
    public List<float> height = new List<float>();

    private int randomRange = 0;
    private float lastPosition = 0;
    private float lastScale = 0;

    public void RandomGenerator() {
        // Generate a random number
        randomRange = Random.Range(0, platform.Count);

        for(int i = 0; i < platform.Count; i++) {
            CreateLevelObject(platform[i], height[i], i);
        }
    }

    public void CreateLevelObject(GameObject obj, float height, int value) {
        // If the random number is equal to the prefab value number(id)
        if(randomRange == value) {
            // instantiation of the gameObject
            GameObject gameObject = Instantiate(obj) as GameObject;

            // Calulation the gameObject position
            float offset = lastPosition + (lastScale * 0.5f);
            offset += (gameObject.transform.localScale.z) * 0.5f;
            Vector3 position = new Vector3(0, height, offset);

            gameObject.transform.position = position;

            lastPosition = gameObject.transform.position.z;
            lastScale = gameObject.transform.localScale.z;

            // Parent the gameObject created to the level generator
            gameObject.transform.parent = this.transform;
        }

        
    }
}
