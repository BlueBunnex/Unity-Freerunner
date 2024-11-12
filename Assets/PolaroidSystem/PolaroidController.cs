using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidController : MonoBehaviour, IResettable {
    
    private void OnTriggerEnter(Collider other) {
        
        LevelController.instance.collectPolaroid(this);
        gameObject.SetActive(false);
    }

    public void Reset() {

        gameObject.SetActive(true);
    }

}
