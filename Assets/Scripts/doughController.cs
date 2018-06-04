using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doughController : MonoBehaviour {
    int doughHealth;
    bool rest;
    public GameController gameController;
    public GameObject leftPawPart, rightPawPart, doughMain, doughLeft, doughRight;

    public AudioSource audioSource;
    public AudioClip bork;

    private void Update() {
        if (rest) {
            gameController.SendMessage("RestTextUpdate", "Let it rest!");
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f,0.6f,1.3f), 0.01f);
            leftPawPart.SetActive(false);
            rightPawPart.SetActive(false);
        } else {
            gameController.SendMessage("RestTextUpdate", "The bread kneads you!");
            leftPawPart.SetActive(true);
            rightPawPart.SetActive(true);
        }

        if (doughHealth == 0) {
            doughMain.SetActive(true);
        } else if (doughHealth % 2 == 0) {
            doughMain.SetActive(false);
            doughLeft.SetActive(false);
            doughRight.SetActive(true);
        } else {
            doughMain.SetActive(false);
            doughLeft.SetActive(true);
            doughRight.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!rest) {
            if (other.gameObject.name == "Arm Left") {
                if (doughHealth % 2 == 0) {
                    Debug.Log("chop left");
                    gameController.SendMessage("DoughHit");
                    doughHealth += 1;

                    if (doughHealth == 5 || doughHealth == 10) {
                        rest = true;
                        StartCoroutine(BreadRest());
                    }
                } else {
                    Debug.Log("grrrr");
                    audioSource.PlayOneShot(bork, 0.5F);
                }
            }

            if (other.gameObject.name == "Arm Right") {
                if (doughHealth % 2 != 0) {
                    Debug.Log("chop right");
                    gameController.SendMessage("DoughHit");
                    doughHealth += 1;

                    if (doughHealth == 5 || doughHealth == 10) {
                        rest = true;
                        StartCoroutine(BreadRest());
                    }
                } else {
                    Debug.Log("grrrr");
                    audioSource.PlayOneShot(bork, 0.5F);
                }
            }

            
        } else {
            Debug.Log("extra grrrr");
            audioSource.PlayOneShot(bork, 1F);
        }
    }

    IEnumerator BreadRest() {       
        yield return new WaitForSeconds(5);
        rest = false;
    }
}