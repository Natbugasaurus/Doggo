using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovenController : MonoBehaviour {
    public ovenDoorController ovenDoorController;
    // Use this for initialization

    private void OnTriggerEnter(Collider other) {
        if (other.name == "breadpan") {
            ovenDoorController.SendMessage("OverideDoor", "close");         
        }
    }
}
