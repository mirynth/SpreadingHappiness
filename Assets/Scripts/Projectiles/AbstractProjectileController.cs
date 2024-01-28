using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileController : MonoBehaviour
{	
	Rigidbody2D rigidbody2d;
	AbstractBobaPattern pattern = new BobaPatternDoNothing();
	
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
	private void FixedUpdate()
	{
		this.pattern.onFixedUpdate(this.rigidbody2d);
	}

	// ************************************************************************
	public void SimpleLaunch(Vector2 direction, float force)
	{
		this.SetPattern(new BobaPatternSimpleMove(direction, force));
	}

	// ************************************************************************
	public void SetPattern(AbstractBobaPattern pattern)
	{
		this.pattern = pattern;
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

