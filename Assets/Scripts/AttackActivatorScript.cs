using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActivatorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")) {
               other.GetComponentInParent<EnemyTracker>().setPassedWall(true);
               Debug.Log("This is working");
        }

        // Debug.Log("This is working but not right");
    }
}

