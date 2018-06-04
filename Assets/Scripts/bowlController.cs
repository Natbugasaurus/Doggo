using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlController : MonoBehaviour {
    public GameController gameController;
    public GameObject flourParticle, yeastParticle, waterParticle;

    float previousX, previousY, amountStirred;

    void OnTriggerEnter(Collider other) {
        // OOOOOOH, you got something in the bowl?!
        if (other.tag == "Pickup") {

            // Flour's in!
            if (other.name == "flour") {
                Debug.Log("GOT DAT FLOUR THX");
                other.transform.position = other.GetComponent<itemController>().originalPos;
                other.gameObject.SetActive(false);
                Instantiate(flourParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                      
                gameController.SendMessage("ThingInBowl", other.name);
            }

            // There's that yeast!
            if (other.name == "yeast") {
                Debug.Log("YUMMY YEAST");
                other.transform.position = other.GetComponent<itemController>().originalPos;
                other.gameObject.SetActive(false);
                Instantiate(yeastParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);

                gameController.SendMessage("ThingInBowl", other.name);                
            }

            // Thirsty boiii
            if (other.name == "water") {
                Debug.Log("GLUG GLUG GLUG");
                other.transform.position = other.GetComponent<itemController>().originalPos;
                other.gameObject.SetActive(false);
                Instantiate(waterParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(90, 0, 0));

                gameController.SendMessage("ThingInBowl", other.name);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        // Stirring business
        if (other.name == "spoon") {
            amountStirred += Mathf.Abs(other.transform.position.x - previousX) + Mathf.Abs(other.transform.position.y - previousY);

            gameController.SendMessage("UpdateStir", amountStirred);

            previousX = other.transform.position.x;
            previousY = other.transform.position.y;
        }
    }
}
