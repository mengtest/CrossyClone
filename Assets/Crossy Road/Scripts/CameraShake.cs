using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float jumpIteration = 4.5f;

    private void Update() {
        if(Input.GetKeyDown("c")) {
            Shake();
        }
    }

    public void Shake() {
        float height = Mathf.PerlinNoise(jumpIteration, 0f);
        height *= height;

        float shakeAmount = height * 20;
        float shakePeriodTime = 0.25f;
        float dropOffTime = 0.5f;

        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, shakeAmount, shakePeriodTime).setEase(LeanTweenType.easeShake).setLoopClamp().setRepeat(-1);
        LeanTween.value(gameObject, shakeAmount, 0, dropOffTime).setOnUpdate((float val) => { shakeTween.setTo(Vector3.right * val); }).setEase(LeanTweenType.easeOutQuad);
    }
}
