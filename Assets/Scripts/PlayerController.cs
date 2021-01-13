using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int initialHealth = 10;
    public int currentHealth;
    public bool canTakeDamage = true;

    public float flightSpeed;
    public float movementSpeed;
    public float turnSpeed = 2;
    public int laserSpeed = 40;
    public float fireRate = 0.15f;
    public GameObject laser;

    private bool canShoot = true;
    private float angleX = 0;
    private float angleY = 0;
    private Vector2 input;
    private Quaternion targetRotation;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    { 
      currentHealth = initialHealth;
      cam = Camera.main.transform;
      transform.GetChild(1).GetComponent<ParticleSystem>().Pause();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        Die();
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0) && canShoot)
           StartCoroutine(Attack());
    }

    void CalculateAngleX()
    {
            angleX = Mathf.Atan2(input.x, 0);
            angleX = Mathf.Rad2Deg * angleX;
            if(angleX > 40)
               angleX = 40;
            if(angleX < -40)
               angleX = -40;
    }

    void CalculateAngleY()
    {
        angleY = Mathf.Atan2(input.y, 0);
        angleY = Mathf.Rad2Deg * angleY;
        if(angleY > 25)
            angleY = 25;
        if(angleY < -25)
            angleY = -25;
        
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(-angleY, 0, -angleX);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void CalculateDirection() 
    {
        if(input.x == 0) 
            angleX = 0;

        if(input.y == 0)
            angleY = 0;

         transform.position += new Vector3(input.x * movementSpeed * Time.deltaTime, input.y * movementSpeed * Time.deltaTime, 0);
    }

    void Movement()
    {   
        CalculateAngleX();
        CalculateAngleY();
        CalculateDirection();
        Rotate();

        transform.position += transform.forward * flightSpeed * Time.deltaTime;
    }

    public IEnumerator Attack() 
    {
        GameObject laserClone = Instantiate(laser, transform.position, transform.rotation);
        laserClone.transform.GetComponent<Rigidbody>().velocity = transform.forward * laserSpeed;
        Destroy(laserClone, 2f);

        canShoot = false;
        yield return new WaitForSeconds (fireRate);
        canShoot  = true;

    }

    private void Die() 
    {
        if(currentHealth == 2)
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();

        if(currentHealth <= 0)
        {
            UnityEngine.Debug.Log("You ded");
            // Destroy(this.gameObject, 0.5f); 
        }
    }

    public bool GetCanTakeDamage() {
        return canTakeDamage;
    }

    public void SetHealth(int amount)
    {
        currentHealth += amount;
        StartCoroutine(DamageDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Missle")) {
            if(other.CompareTag("Missle"))
                other.GetComponentInParent<Missle>().setExplosion();

            if(canTakeDamage) {
                 SetHealth(-1);
                 StartCoroutine(DamageDelay());
            }

            Destroy(other.gameObject, 0.2f);
        }
    }

    public IEnumerator DamageDelay() 
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
    }
}
