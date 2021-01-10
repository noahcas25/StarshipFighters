using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{

    public int speed = 2;
    public int initialHealth;
    public bool tracker = false;
    public GameObject LaserEnemy;

    private bool passedWall = false;
    private int count = 0;
    private int laserSpeed = 10;
    private bool canFire = true;
    private int currentHealth;
    private GameObject player;
    private GameObject missle;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;
        transform.GetChild(0).GetComponent<ParticleSystem>().Pause();
        transform.GetChild(1).GetComponent<ParticleSystem>().Pause();
        player = GameObject.FindWithTag("Player");
        missle = GameObject.FindWithTag("Missle");
    }

    // Update is called once per frame
    void Update()
    {
        if(tracker) {
            transform.LookAt(player.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else{
             transform.position += transform.forward * speed * Time.deltaTime/3;
        }

        Attack();
        Die();
    }

    void Attack()
    {

        if(!tracker & transform.childCount == 2)
        {
            GameObject Missle = Instantiate(missle, transform.position, transform.rotation);
            Missle.transform.SetParent(transform, true);
        }

        if(passedWall) 
        {
            if(transform.childCount < 3 & !tracker) 
            {
                GameObject Missle = Instantiate(missle, transform.position, transform.rotation);
                Missle.transform.SetParent(transform, true);
            }

            if(tracker & canFire)
            {   
                StartCoroutine(AttackDelay());
                GameObject laserClone = Instantiate(LaserEnemy, transform.position, transform.rotation);
                laserClone.transform.GetComponent<Rigidbody>().velocity = transform.forward * laserSpeed;
                Destroy(laserClone, 6f);
            }
        }
    }

    public IEnumerator AttackDelay() 
    {
        canFire = false;
        yield return new WaitForSeconds(1f);
        canFire = true;
    }
    

    void Die() {
        
        if(currentHealth == 4)
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        
        if(currentHealth <= 0)
        {
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject, 0.5f); 
        }
    }

    public void setPassedWall(bool result)
    {
        passedWall = result;
    }

    public void SetHealth(int amount) 
    {
        currentHealth += amount;
    }
}
