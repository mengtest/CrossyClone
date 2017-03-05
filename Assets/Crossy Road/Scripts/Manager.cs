using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public Text coin = null;
    public Text distance = null;
    public new Camera camera = null;
    public GameObject guiGameOver = null;

    private int _currentCoins;
    private int _currentDistance;
    private bool canPlay;

    private static Manager _instance;

    private void Start() {
        _currentCoins = 0;
        _currentDistance = 0;
        canPlay = false;

        // TODO: Level generator start up
    }

    private void Update() {

    }

    public void UpdateCoinCount(int value) {
        // Update coin value and coin GUI
        _currentCoins += value;
        coin.text = _currentCoins.ToString();
    }

    public void UpdateDistanceCount() {
        // Update coin value and coin GUI
        _currentDistance += 1;
        distance.text = _currentDistance.ToString();

        // TODO: Generate new level piece here
    }

    public bool CanPlay() {
        return canPlay;
    }

    public void StartPlay() {
        canPlay = true;
    }

    public void GameOver() {
        camera.GetComponent<CameraShake>().Shake();
        camera.GetComponent<CameraFollow>().enabled = false;

        GuiGameOver();
    }

    private void GuiGameOver() {
        guiGameOver.SetActive(true);
    }

    public void PlayAgain() {
        // Get active scene
        Scene scene = SceneManager.GetActiveScene();

        // Reload the scene
        SceneManager.LoadScene(scene.name);
    }

    public void Quit() {
        Application.Quit();
    }

    public static Manager instance {
        get {
            if(_instance == null) {
                _instance = FindObjectOfType(typeof(Manager)) as Manager;
            }
            return _instance;
        }
    }

}
