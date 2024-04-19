using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    public string myTarget = "invalid";
    // Start is called before the first frame update

    void OnTriggerStay(Collider other) //checks if collider (set below) is present inside trigger zone
    {
        if (other.gameObject.tag == "Player") //sets plr as the game object with the tag Player
        {
            SceneManager.LoadScene(myTarget);
        }
    }
}
