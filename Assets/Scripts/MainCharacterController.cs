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
	
	public float hSpeed = 10.0f;
	public float vSpeed = 10.0f;

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
   }
	
   // ************************************************************************
	
   // Update is called once per frame
   void Update()
   {
        // Create a direction vector from the horizontal and vertical inputs
        Vector2 direction = new Vector2(horizontal, vertical);

        // Normalize the direction vector to have a magnitude of 1 if it is not zero
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        // Update the position based on the normalized direction
        Vector2 position = transform.position;
        position += new Vector2(hSpeed * direction.x * Time.deltaTime, vSpeed * direction.y * Time.deltaTime);
        transform.position = position;

        //position.x = position.x + hSpeed * horizontal * Time.deltaTime;
        //position.y = position.y + vSpeed * vertical * Time.deltaTime;
        
        //Debug.Log("current speed: " + hSpeed);
		
		// TODO: This throws a ref not set exception
        //Debug.Log("current radius: " + hitboxCollider);
  
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

   public void IncrementBobaBitCount(int value = 1)
   {
	    BobaBits += value;
        //After changing the boba amount, fire the OnPlayerBobaCountChanged event
        UIEvents.OnPlayerBobaCountChanged(BobaBits);
        upgrades.BobaChanged(BobaBits);
    }
   
   // ************************************************************************
   	
   public void TakeDamage(int value = 1)
   {
	   // TODO To be determined
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
}
