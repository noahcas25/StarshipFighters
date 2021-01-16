using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{

    private GameObject player;
    private int speed = 2;
    private int counter = 0;

    void Start() 
    {
        player = GameObject.FindWithTag("Player");
        transform.GetChild(0).GetComponent<ParticleSystem>().Pause();
    }

    // Update is called once per frame
    void Update()
    {
       transform.LookAt(player.transform);
       
       transform.position += transform.forward * speed * Time.deltaTime;
       transform.Rotate(new Vector3(0, 0, 2) * Time.deltaTime);

    //    transform.GetChild(2).GetComponent<Light>().intensity = 0;

    }

    public void setExplosion()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Laser")) {
             Destroy(other.gameObject);
             setExplosion();
             Destroy(this.gameObject, 0.5f);
        }
    }

    // private IEnumerator blinker() 
    // {
       
    //     yield return new WaitForSeconds(1f);
        
    // }
}
