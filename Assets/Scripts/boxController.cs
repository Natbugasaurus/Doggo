using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxController : MonoBehaviour {

    // If an item falls out of the play space, let the poor thing know to get back to it's original spot
    void OnTriggerExit(Collider other) {
        if (other.tag == "Pickup") {
            other.GetComponent<itemController>().SendMessage("ResetPos");
        }
    }
}
