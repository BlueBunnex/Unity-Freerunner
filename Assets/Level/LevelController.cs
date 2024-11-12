using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    // this takes in all the stuff it needs as parameters
    // and handles stuff like loading/setting up the level/whatever idk

    public Transform startPoint;
    public GameObject player;

    private ArrayList toReset = new ArrayList(); // holds IResettable-s
    private int polaroidCount = 0;

    void Start() {
        
        instance = this;

        resetLevel();
    }

    public void resetLevel() {

        // reset resettables
        foreach (var resettable in toReset) {

            ((IResettable) resettable).Reset();
        }
        toReset.Clear();

        // reset polaroid count
        polaroidCount = 0;

        // reset player
        player.transform.position = startPoint.position;
        
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.movePlanarGlobal *= 0;
        pc.moveVertical = 0;

        Debug.Log(polaroidCount + " polaroids in UI");
    }

    public void collectPolaroid(IResettable polaroid) {

        toReset.Add(polaroid);

        polaroidCount++;

        Debug.Log(polaroidCount + " polaroids in UI");
    }

}