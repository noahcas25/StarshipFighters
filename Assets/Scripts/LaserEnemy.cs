using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTarget")) 
        {   if(other.GetComponentInParent<PlayerController>().GetCanTakeDamage())
                other.GetComponentInParent<PlayerController>().SetHealth(-1);
                Destroy(transform.parent.gameObject);
        }
    }
}
