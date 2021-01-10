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

    private void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canObtain)
        {
                // audioPowerUp.PlayOneShot(powerUpSound);

            StartCoroutine(powerUpDelay());

            int rand = Random.Range(1, 4);

            switch(rand) 
            {
                case 1:
                //   player.GetComponent<PlayerController>().currentPowerUp = "Heart";
                //   if(player.GetComponent<PlayerControl>().health <= 5)
                //      player.GetComponent<PlayerControl>().health += 3;
                //   else
                //      player.GetComponent<PlayerControl>().health = 8;
                //      Destroy(this.gameObject, 3);

                Debug.Log("Chill Foo");
                break;

                case 2:
                //   player.GetComponent<PlayerControl>().currentPowerUp = "Speed";
                  StartCoroutine(powerUpTimerSpeed());
                break;

                case 3:
                //   player.GetComponent<PlayerControl>().currentPowerUp = "Damage";
                  StartCoroutine(powerUpTimerFireRate());
                break; 
            }
            
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(this.gameObject, 11);
        }
    }

    private IEnumerator powerUpTimerSpeed() 
    {
         player.GetComponent<PlayerController>().movementSpeed = 10;
         yield return new WaitForSeconds(powerUpTime);
         player.GetComponent<PlayerController>().movementSpeed = 10;
    }

    private IEnumerator powerUpTimerFireRate() 
    {
        player.GetComponent<PlayerController>().fireRate = 0.1f;
        yield return new WaitForSeconds(powerUpTime);
        player.GetComponent<PlayerController>().fireRate = 0.1f;
    }

    private IEnumerator powerUpDelay() 
    {
        canObtain = false;
        yield return new WaitForSeconds(3f);
        canObtain = true;
    }
}
