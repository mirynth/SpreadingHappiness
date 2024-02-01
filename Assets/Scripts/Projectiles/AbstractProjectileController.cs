using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileController : MonoBehaviour, IPoolable
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
            Proxy_Destroy();
		}
	}

	// ************************************************************************
	private void FixedUpdate()
	{
		this.pattern.onFixedUpdate(this.rigidbody2d);
	}

	// ************************************************************************
	public void Launch(Vector2 direction, float force)
	{
		// disable boba patterns when doing a simple launch
		this.SetPattern(new BobaPatternDoNothing());

		GetComponent<Rigidbody2D>().AddForce(direction * force);
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
            Proxy_Destroy();
		}
	}
	
	// Called during a collision.
	public abstract void OnCollisionEffect(MainCharacterController e);

	//Proxy Destroy lets us use the Concrete Class to recycle the Pooled Instance instead of actually Destroying it.
	public abstract void Proxy_Destroy();

    public void OnPooled()
    {
		//Show in Heirarchy
        gameObject.hideFlags &= ~HideFlags.HideInHierarchy;
    }

    public void OnUnPooled()
    {
		//Hide in Hierarchy
        gameObject.hideFlags |= HideFlags.HideInHierarchy;
    }

    public void OnPoolCreate()
    {
        gameObject.hideFlags |= HideFlags.HideInHierarchy;
    }

    public void OnPoolDestroy()
    {
    }

    public void OnPoolReset()
    {
		gameObject.hideFlags |= HideFlags.HideInHierarchy;
    }
}

