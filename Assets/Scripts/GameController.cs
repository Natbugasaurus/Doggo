using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
    int gameState = 0;
    public Camera startCam, playerCam;
    public int startCamSpeed;
    int escapePower = 0;

    public GameObject player, bowl, spoon, ingredientsObjs, title, startText, dough, breadPan, leftPawPart, rightPawPart, credits;
    public int listLength, doughHealth;
    public float amountStirred;

    List<string> ingredients = new List<string>();
    public TextMeshPro goalText, ingredientList, mixMeter;
    public TextMeshProUGUI escape;

    public AudioSource audioSource;
    public AudioClip bork;

	void Start () {
        // Sorting out ingredients and list
        ingredients.Add("yeast");
        ingredients.Add("flour");
        ingredients.Add("water");
        
    }

    // If something is in the bowl, remove from the array
    void ThingInBowl(string thing) {
        ingredients.Remove(thing);
    }

    // Grab the amount stirred from the bowl to update the mix meter
    void UpdateStir(float amount) {
        amountStirred = amount;
    }

    void DoughHit() {
        doughHealth += 1;
    }

    void RestTextUpdate(string text) {
        goalText.text = text;
    }

    void Update () {
        // Exit game
        if (Input.GetKey(KeyCode.Escape) || Input.GetButton("Cancel")) {        
            escape.gameObject.SetActive(true);
            escape.text = "Quit power: " + escapePower.ToString() + "%";
            if (escapePower >= 100) {
                Application.Quit();
            } else {
                escapePower++;
            }
        } else {
            escapePower = 0;
            escape.gameObject.SetActive(false);
        }

        // Pre game section
        if (gameState == 0) {
            startCam.enabled = true;
            startCam.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * startCamSpeed);
            playerCam.enabled = false;

            if (Input.GetButtonDown("Submit")) {
                // Clear out the old
                startCam.enabled = false;
                startCam.gameObject.SetActive(false);
                title.SetActive(false);
                startText.SetActive(false);
                credits.SetActive(false);

                audioSource.PlayOneShot(bork, 0.7F);

                gameState = 1;
            }
        }

        // Adding the ingredients!
        if (gameState == 1) {

            // Bring in the new
            player.SetActive(true);
            ingredientsObjs.SetActive(true);
            playerCam.enabled = true;
            goalText.gameObject.SetActive(true);
            ingredientList.gameObject.SetActive(true);
            goalText.text = "Add the ingredients!";

            // Get that to do list going on the back wall
            if (ingredients.Count != listLength) {
                ingredientList.text = "";
                for (int i = 0; i < ingredients.Count; i++) {
                    if (i == 0 || i % 2 == 0) {
                        ingredientList.text += "-" + ingredients[i] + "\t";
                    }
                    else {
                        ingredientList.text += "-" + ingredients[i] + "\n";
                    }
                }
            }

            // This is for checking when there are changes to the ingredients list so it knows when to update
            listLength = ingredients.Count;

            // Check if it's time to move to the next start
            if (ingredients.Count == 0) {              
                ingredientList.gameObject.SetActive(false);
                gameState = 2;              
            }
        }

        // Mixing!
        if (gameState == 2) {
            goalText.text = "Time to mix!";
            mixMeter.gameObject.SetActive(true);         
            spoon.gameObject.SetActive(true);

            if ((amountStirred * 10) < 100) {
                mixMeter.text = (amountStirred * 10).ToString("f0") + "%";
            } else {
                bowl.SetActive(false);
                mixMeter.gameObject.SetActive(false);
                spoon.gameObject.SetActive(false);
                gameState = 3;
            }
        }
        
        // Kneading!
        if (gameState == 3) {          
            goalText.text = "The bread kneads you!";
            
            player.GetComponent<playerController>().pawSpeed = 150;
            player.GetComponent<playerController>().SendMessage("KneadLockToggle", true);

            dough.SetActive(true); 
            
            if (doughHealth == 15) {
                dough.SetActive(false);
                player.GetComponent<playerController>().pawSpeed = 40;
                player.GetComponent<playerController>().SendMessage("KneadLockToggle", false);
                leftPawPart.SetActive(false);
                rightPawPart.SetActive(false);

                gameState = 4;
                player.GetComponent<playerController>().ChangeStation(1);
            }
        }

        if (gameState == 4) {
            
            breadPan.SetActive(true);
            if (player.GetComponent<playerController>().item != null) { 
                if (player.GetComponent<playerController>().item.name == "breadpan") {
                    Debug.Log("Bread in hand");
                }
            }
        }
    }
}
