using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    CircleCollider2D hitboxCollider;
    SpriteRenderer hitboxRenderer;

    float horizontal;
    float vertical;

    public MainCharacterUpgrades upgrades;

    public Bounds movement_restriction = new Bounds();
    public bool restrict_movement = true;

    // ************************************************************************
    private void Awake()
    {
        instance = this;
    }

    void Start()
   {
        hitboxCollider = transform.GetComponent<CircleCollider2D>();
        hitboxRenderer = transform.GetComponent<SpriteRenderer>();

        health = max_health;
        UIEvents.OnPlayerHealthChanged(new(health, max_health));
    }
	
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

        if(restrict_movement)
        {
            transform.position = new Vector2(Mathf.Min(Mathf.Max(movement_restriction.min.x, position.x), movement_restriction.max.x),
                Mathf.Min(Mathf.Max(movement_restriction.min.y, position.y), movement_restriction.max.y));
        } else
        {
            transform.position = position;
        }
  
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

   public void IncrementBobaBitCount(int value = 1)
   {
	    BobaBits += value;
        //After changing the boba amount, fire the OnPlayerBobaCountChanged event
        UIEvents.OnPlayerBobaCountChanged(BobaBits);
        upgrades.BobaChanged(BobaBits);
    }
   
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

    public void OnMoveInput(InputAction.CallbackContext context)
    {
	    Vector2 movementVector = context.ReadValue<Vector2>();
	    horizontal = movementVector.x;
	    vertical = movementVector.y;
    }

    public void OnStrafeInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hSpeed = 5.0f;
            vSpeed = 5.0f;
            hitboxCollider.radius = 0.01f;
            hitboxRenderer.enabled = true;
        }
        else if (context.canceled)
        {
            hSpeed = 10.0f;
            vSpeed = 10.0f;
            hitboxCollider.radius = 2;
            hitboxRenderer.enabled = false;
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
