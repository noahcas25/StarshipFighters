using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")) {
             other.GetComponentInParent<EnemyTracker>().SetHealth(-1);
             Destroy(this.gameObject);
            //  Debug.Log("hit");
        }
    }
}
