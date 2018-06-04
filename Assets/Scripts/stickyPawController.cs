using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyPawController : MonoBehaviour {
    public GameObject item;
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Pickup") {
            item = other.gameObject;           
        }                  
    }

    private void OnTriggerExit(Collider other) {
        item = null;
    }
}
