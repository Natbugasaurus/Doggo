using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovenDoorController : MonoBehaviour {
    Vector3 originalPos;
    bool doorOpen, overideDoor;
    private void Start() {
        originalPos = this.transform.position;
    }
    void Update () {
        this.transform.position = originalPos;
        if (transform.eulerAngles.z < 0) {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (!overideDoor) {
            this.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
        }
    }

    void OverideDoor(string state) {
        if (state == "open") {
            this.transform.rotation = Quaternion.Euler(0, 0, 95);
        } else if (state == "close") {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        overideDoor = true;
    }
}
