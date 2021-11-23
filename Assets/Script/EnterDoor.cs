using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    bool DoorTriggerStay = false;

    // Start is called before the first frame update

     private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && DoorTriggerStay)
            {
                SceneManager.LoadScene("FirstScene");
            }
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DoorTriggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DoorTriggerStay = false;
        }
    }
}