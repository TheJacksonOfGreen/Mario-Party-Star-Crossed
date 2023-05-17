using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGate : MonoBehaviour {
    public Transform leftDoor;
    public Transform rightDoor;

    public void Open() {
        leftDoor.rotation = Quaternion.Euler(0.0f, -120.0f, 0.0f);
        rightDoor.rotation = Quaternion.Euler(0.0f, 120.0f, 0.0f);
    }

    public void Close() {
        leftDoor.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        rightDoor.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void Start() {

    }

    void Update() {

    }
}
