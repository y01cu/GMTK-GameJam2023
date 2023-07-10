using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCharacterFallingManager : MonoBehaviour
{
    public static AvoidCharacterFallingManager instance;
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void AvoidFallingOfTransform(Transform protectedTransform) {
        float zPositiveLimit = 49;
        float xPositiveLimit = 8.5f;
        float xNegativeLimit = -8.5f;

        if (protectedTransform.position.z >= zPositiveLimit) {
            protectedTransform.position = new Vector3(protectedTransform.position.x, protectedTransform.position.y, zPositiveLimit);
        }
        if (protectedTransform.position.x >= xPositiveLimit) {
            protectedTransform.position = new Vector3(xPositiveLimit, protectedTransform.position.y, protectedTransform.position.z);
        }
        if (protectedTransform.position.x <= xNegativeLimit) {
            protectedTransform.position = new Vector3(xNegativeLimit, protectedTransform.position.y, protectedTransform.position.z);
        }
    }
}
