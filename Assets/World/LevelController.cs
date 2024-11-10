using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    // this takes in all the stuff it needs as parameters
    // and handles stuff like loading/setting up the level/whatever idk

    public Transform startPoint;
    public GameObject player;

    public static LevelController instance;

    void Start() {
        instance = this;
    }

    public void resetLevel() {
        player.transform.position = startPoint.position;
    }

}
