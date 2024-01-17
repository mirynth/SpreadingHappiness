using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
	// number of boba bits
	public int BobaBits = 0;
	
	public float hSpeed = 10.0f;
	public float vSpeed = 10.0f;
    bool strafeModeOn = false;
    CircleCollider2D hitboxCollider;
    SpriteRenderer hitboxRenderer;
	
	float horizontal;
	float vertical;

   // ************************************************************************
	
    // Start is called before the first frame update
    void Start()
   {
        hitboxCollider = transform.Find("hitboxCircle").GetComponent<CircleCollider2D>();
        hitboxRenderer = transform.Find("hitboxCircle").GetComponent<SpriteRenderer>();
   }
	
   // ************************************************************************
	
   // Update is called once per frame
   void Update()
   {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Apply strafe when LeftShift is held down
        if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyUp(KeyCode.LeftShift)))
        {
            toggleStrafe();
        }
        //Debug.Log("current speed: " + hSpeed);
		
		// TODO: This throws a ref not set exception
        //Debug.Log("current radius: " + hitboxCollider.radius);
    }
	
   // ************************************************************************
	
    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + hSpeed * horizontal * Time.deltaTime;
        position.y = position.y + vSpeed * vertical * Time.deltaTime;
        transform.position = position;
    }

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
            hSpeed = 0.5f * hSpeed;
            vSpeed = 0.5f * vSpeed;
            hitboxCollider.radius = 0.01f;
            hitboxRenderer.enabled = true;
            strafeModeOn = true;
        }
   }
   
   // ************************************************************************
   	
   public void IncrementBobaBitCount()
   {
	   BobaBits++;   	
   }
   
   // ************************************************************************
   	
   public void TakeDamage()
   {
	   // TODO To be determined
   }
}
