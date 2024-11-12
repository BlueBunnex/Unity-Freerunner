using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    // this takes in all the stuff it needs as parameters
    // and handles stuff like loading/setting up the level/whatever idk

    [HeaderAttribute("Level Config")]
    public int polaroidsToWin = 3;
    //public IResettable[] enabledInThisLevel;

    public Transform startPoint;

    [HeaderAttribute("Level Dependencies")]
    public GameObject player;
    public Text timeReadout;

    private ArrayList toReset = new ArrayList(); // holds IResettable-s
    private int polaroidCount = 0;
    
    private float timeSeconds = 0f;

    void Start() {
        
        instance = this;

        resetLevel();
    }

    void Update() {
        timeSeconds += Time.deltaTime;

        int ms = (int) (timeSeconds * 1000);

        string minutes = "" + (ms / 60000);
        ms %= 60000;
        string seconds = "" + (ms / 1000);
        ms %= 1000;
        string millisec = "" + ms;

        if (seconds.Length == 1) {
            seconds = "0" + seconds;
        }

        while (millisec.Length < 3) {
            millisec = "0" + millisec;
        }

        timeReadout.text = minutes + ":" + seconds + "." + millisec;
    }

    public void resetLevel() {

        timeSeconds = 0;

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
