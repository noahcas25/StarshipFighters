using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // public AudioClip powerUpSound;
    // public AudioSource audioPowerUp;
    private GameObject player;
    private float powerUpTime = 10f;
    private bool canObtain = true;
    
    
    private void Awake() 
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
       transform.Rotate(new Vector3(0, 0, 2) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canObtain)
        {
                // audioPowerUp.PlayOneShot(powerUpSound);

            StartCoroutine(powerUpDelay());

            int rand = 2;

            switch(rand) 
            {
                case 1:
                    player.GetComponent<PlayerController>().currentHealth = 10;
                    Debug.Log("1");
                break;

                case 2:
                    StartCoroutine(powerUpTimerShield());
                     Debug.Log("2");
                break;

                case 3:
                    StartCoroutine(powerUpTimerFireRate());
                     Debug.Log("3");
                break; 
            }
            
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            
            Destroy(this.gameObject, 11);
        }

        
    }

    private IEnumerator powerUpTimerSpeed() 
    {
        player.GetComponent<PlayerController>().movementSpeed = 10;
        yield return new WaitForSeconds(powerUpTime);
        player.GetComponent<PlayerController>().movementSpeed = 8;
    }

    private IEnumerator powerUpTimerShield() 
    {
        player.GetComponent<PlayerController>().canTakeDamage = false;
        player.transform.GetChild(3).transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(powerUpTime);
        player.GetComponent<PlayerController>().canTakeDamage = true;
        player.transform.GetChild(3).transform.localPosition = new Vector3(100, 100, 100);
    }

    private IEnumerator powerUpTimerFireRate() 
    {
        player.GetComponent<PlayerController>().fireRate = 0.1f;
        yield return new WaitForSeconds(powerUpTime);
        player.GetComponent<PlayerController>().fireRate = 0.15f;
    }

    private IEnumerator powerUpDelay() 
    {
        canObtain = false;
        yield return new WaitForSeconds(3f);
        canObtain = true;
    }
}
