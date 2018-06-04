using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour {
    public bool pickedUp, outSideBox;
    public Vector3 originalPos;

    private void Start() {
        originalPos = transform.position;
    }

    void Update () {
        // Enable item outline when picked up and set it back to its original position when it falls out of bounds
        if (pickedUp) {
            this.GetComponent<Outline>().enabled = true;
        } else {
            this.GetComponent<Outline>().enabled = false;
            if (outSideBox == true) {
                this.transform.position = originalPos;
                outSideBox = false;
            }
        }
	}

    // This is where the game box will tell the item if it's fallen out
    void ResetPos() {
        outSideBox = true;       
    }
}
