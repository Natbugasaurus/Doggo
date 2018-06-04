using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public GameObject leftArm, rightArm, leftArmPivot, rightArmPivot, leftPaw, rightPaw, item;
    public int pawSpeed;
    public float rotateSpeed;
    public bool kneadLock;

    public int stationInUse = 0;

	
	void Update () {
        playerMovement();

        if (leftPaw.GetComponent<stickyPawController>().item == rightPaw.GetComponent<stickyPawController>().item && leftPaw.GetComponent<stickyPawController>().item != null) {
            // Grab the picked up item
            item = leftPaw.GetComponent<stickyPawController>().item;

            // Set items position to midpoint between both paws
            item.transform.position = leftPaw.transform.position + ((rightPaw.transform.position - leftPaw.transform.position) / 2);
            item.GetComponent<Rigidbody>().useGravity = false;

            // Set items picked up bool for Outline shader
            item.GetComponent<itemController>().pickedUp = true;

            if (item.name == "spoon") {
                item.transform.rotation = Quaternion.identity;
            }
        }
        else if (item != null) {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<itemController>().pickedUp = false;
            item = null;           
        }
    }

    public void ChangeStation(int station) {
        stationInUse = station;
        if (stationInUse == 0) {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, rotateSpeed);
        } else if (stationInUse == 1) {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            this.transform.position = new Vector3(1.5f, -1.5f, -8.8f);
        }
    }

    public void KneadLockToggle(bool state) {
        kneadLock = state;
    }

    private void playerMovement() {
        // Scale arms based on their position so you can reach items in the corners of the workspace
        leftArm.transform.localScale = new Vector3(leftArm.transform.localScale.x, leftArm.transform.localScale.y, 5 + Mathf.Abs(leftArm.transform.forward.x * 3f));
        rightArm.transform.localScale = new Vector3(rightArm.transform.localScale.x, rightArm.transform.localScale.y, 5 + Mathf.Abs(-rightArm.transform.forward.x * 3f));

        // Controller inputs
        if (kneadLock) {
            leftArmPivot.transform.rotation = Quaternion.Euler(leftArmPivot.transform.eulerAngles.x + Input.GetAxis("VerticalLeft") * Time.deltaTime * pawSpeed * -1, 10, 0);
            rightArmPivot.transform.rotation = Quaternion.Euler(rightArmPivot.transform.eulerAngles.x + Input.GetAxis("VerticalRight") * Time.deltaTime * pawSpeed * -1, -10, 0);
        } else {
            leftArmPivot.transform.rotation = Quaternion.Euler(leftArmPivot.transform.eulerAngles.x + Input.GetAxis("VerticalLeft") * Time.deltaTime * pawSpeed * -1, leftArmPivot.transform.eulerAngles.y + Input.GetAxis("HorizontalLeft") * Time.deltaTime * pawSpeed, 0);
            rightArmPivot.transform.rotation = Quaternion.Euler(rightArmPivot.transform.eulerAngles.x + Input.GetAxis("VerticalRight") * Time.deltaTime * pawSpeed * -1, rightArmPivot.transform.eulerAngles.y + Input.GetAxis("HorizontalRight") * Time.deltaTime * pawSpeed, 0);
        }
        

        // Keyboard inputs
        // Player One/Left Paw Controls

        // Movement
        // Right
        if (Input.GetKey(KeyCode.D)) {
            leftArmPivot.transform.Rotate(Vector3.up * Time.deltaTime * pawSpeed);
        }
        // Left
        if (Input.GetKey(KeyCode.A)) {
            leftArmPivot.transform.Rotate(Vector3.down * Time.deltaTime * pawSpeed);
        }
        // Up
        if (Input.GetKey(KeyCode.W)) {

            leftArmPivot.transform.Rotate(Vector3.left * Time.deltaTime * pawSpeed);
        }
        // Down
        if (Input.GetKey(KeyCode.S)) {
            leftArmPivot.transform.Rotate(Vector3.right * Time.deltaTime * pawSpeed);
        }

        // Player Two/Right Paw Controls
        // Right

        if (Input.GetKey(KeyCode.L)) {
            rightArmPivot.transform.Rotate(Vector3.up * Time.deltaTime * pawSpeed);
        }
        // Left
        if (Input.GetKey(KeyCode.J)) {
            rightArmPivot.transform.Rotate(Vector3.down * Time.deltaTime * pawSpeed);
        }
        // Up
        if (Input.GetKey(KeyCode.I)) {
            rightArmPivot.transform.Rotate(Vector3.left * Time.deltaTime * pawSpeed);
        }
        // Down
        if (Input.GetKey(KeyCode.K)) {
            rightArmPivot.transform.Rotate(Vector3.right * Time.deltaTime * pawSpeed);
        }
    }
}
