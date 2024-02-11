using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEditor;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public static MainCharacterController instance;

	// number of boba bits
	public int BobaBits = 0;

    float speed = 1.0f;
    float defence = 0.0f;
    float regen = 0.0f;
    float health = 0.0f;
    float max_health = 10.0f;

    public bool input_on = true;
	public float hSpeed = 10.0f;
	public float vSpeed = 10.0f;
    bool strafeModeOn = false;
    CircleCollider2D hitboxCollider;
    SpriteRenderer hitboxRenderer;

    float horizontal;
    float vertical;

    public MainCharacterUpgrades upgrades;

    // ************************************************************************
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
   {
        hitboxCollider = transform.GetComponent<CircleCollider2D>();
        hitboxRenderer = transform.GetComponent<SpriteRenderer>();

        health = max_health;
        UIEvents.OnPlayerHealthChanged(new(health, max_health));
    }
	
   // ************************************************************************
	
   // Update is called once per frame
   void Update()
   {

        if (!input_on)
            return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Create a direction vector from the horizontal and vertical inputs
        Vector2 direction = new Vector2(horizontal, vertical);

        // Normalize the direction vector to have a magnitude of 1 if it is not zero
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        // Update the position based on the normalized direction
        Vector2 position = transform.position;
        position += new Vector2(hSpeed * direction.x * Time.deltaTime, vSpeed * direction.y * Time.deltaTime) * speed;
        transform.position = position;

        //position.x = position.x + hSpeed * horizontal * Time.deltaTime;
        //position.y = position.y + vSpeed * vertical * Time.deltaTime;
        
        // Apply strafe when LeftShift is held down
        if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyUp(KeyCode.LeftShift)))
        {
            toggleStrafe();
        }
        //Debug.Log("current speed: " + hSpeed);
		
		// TODO: This throws a ref not set exception
        //Debug.Log("current radius: " + hitboxCollider);
  
        if(regen > 0.0f)
        {
            health = Mathf.Min(health + (regen * Time.deltaTime), max_health);
            UIEvents.OnPlayerHealthChanged(new(health, max_health));
        }

        if(health <= 0.0f)
        {
            GameManager.Instance.PlayerDeath();
        }
    }
	
   // ************************************************************************
	
    /*void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + hSpeed * horizontal * Time.deltaTime;
        position.y = position.y + vSpeed * vertical * Time.deltaTime;
        transform.position = position;
    }*/

   // ************************************************************************
	
   void toggleStrafe()
   {
        if(strafeModeOn)
        {
            hSpeed = 10.0f;
            vSpeed = 10.0f;
            hitboxCollider.radius = 2;
            hitboxRenderer.enabled = false;
            strafeModeOn = false;
        }
        else
        {
            hSpeed = 5.0f;
            vSpeed = 5.0f;
            hitboxCollider.radius = 0.01f;
            hitboxRenderer.enabled = true;
            strafeModeOn = true;
        }
        Debug.Log("strafe status: " + strafeModeOn);
   }
   
    // ************************************************************************
   	
    public void IncrementBobaBitCount(int value = 1)
    {
	    BobaBits += value;
        //After changing the boba amount, fire the OnPlayerBobaCountChanged event
        UIEvents.OnPlayerBobaCountChanged(BobaBits);
        upgrades.BobaChanged(BobaBits);
    }
   
    // ************************************************************************
   	
    public void TakeDamage(float value = 1)
    {
        //Defence reduces by a flat amount down to 10% of damage taken.
        health -= Mathf.Max(value * 0.1f, value - defence);
        UIEvents.OnPlayerHealthChanged(new(health, max_health));
        if (health <= 0.0f)
        {
            GameManager.Instance.PlayerDeath();
        }
    }

    public void Upgrade_Speed()
    {
        speed += 0.1f;
    }
    public void Upgrade_Defence()
    {
        defence += 0.1f;
    }

    public void Upgrade_Regen()
    {
        regen += 0.025f;
    }

    public void Upgrade_MaxHealth()
    {
        max_health += 1.0f;
        UIEvents.OnPlayerHealthChanged(new(health, max_health));
    }

}
