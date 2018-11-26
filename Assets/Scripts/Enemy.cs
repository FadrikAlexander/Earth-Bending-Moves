using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Kill the enemy when hit ground
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
