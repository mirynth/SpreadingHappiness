using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileController : MonoBehaviour
{	
    Rigidbody2D rigidbody2d;
	
	// Start is called before the first frame update
	void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();       
	}
	
	// ************************************************************************
	
	// Update is called once per frame
	void Update()
	{
		if(transform.position.magnitude > 1000.0f)
		{
			Destroy(gameObject);
		}
	}
	
	// ************************************************************************
	
	public void Launch(Vector2 direction, float force)
	{
		GetComponent<Rigidbody2D>().AddForce(direction * force);
	}
	
	// ************************************************************************
	
	// Destroy the projectile on collision
	void OnCollisionEnter2D(Collision2D other)
	{
		MainCharacterController e = other.collider.GetComponent<MainCharacterController>();
		if (e != null)
		{
			OnCollisionEffect(e);
			Destroy(gameObject);
		}
	}
	
	// Called during a collision.
	public abstract void OnCollisionEffect(MainCharacterController e);
}

