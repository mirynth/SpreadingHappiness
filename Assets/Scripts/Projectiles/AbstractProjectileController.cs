using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class AbstractProjectileController : MonoBehaviour, IPoolable
{	
	Rigidbody2D rigidbody2d;
	AbstractBobaPattern pattern = new BobaPatternDoNothing();
	AbstractProjectileEffect effect = null;

	//projectiles will be destroyed if they are destruction_radius away form MainCharacter
	[SerializeField] float destruction_radius = 75.0f;
	float destruction_check_timer = 0.0f;
	[SerializeField] float destruction_check_interval = 1.5f;
	//Timeout is a countdown based on destruction_check_timer 
	int timeout_countdown = 0;	
	[SerializeField] int timeout_interval = 15;


	// Start is called before the first frame update
	void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// ************************************************************************
	
	// Update is called once per frame
	void Update()
	{
		if (effect != null)
			effect.Update();

	}

	// ************************************************************************
	private void FixedUpdate()
	{
		this.pattern.onFixedUpdate(this.rigidbody2d);
		
		destruction_check_timer -= Time.fixedDeltaTime;
		if(destruction_check_timer <= 0.0f )
		{
			destruction_check_timer += destruction_check_interval;
			timeout_countdown--;

			if (timeout_countdown <= 0)
			{
				Proxy_Destroy();
			}

			if(MainCharacterController.instance != null)
			{
				if(Vector3.Distance(MainCharacterController.instance.transform.position, transform.position) > destruction_radius)
				{
					Proxy_Destroy();
				}
			}
			
		}
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
	
	public void SetEffect(AbstractProjectileEffect effect)
	{
		//Cleanup last visual (Incase we use animation for some projectiles etc)
		if(effect != null)
		{
			effect.RemoveVisual(this);
		}

		this.effect = effect;

		if(effect != null)
		{
			effect.ApplyVisual(this);
		}
	}

	// Destroy the projectile on collision
	void OnCollisionEnter2D(Collision2D other)
	{
		MainCharacterController e = other.collider.GetComponent<MainCharacterController>();
		if (e != null)
		{
			if (effect != null)
			{
				effect.OnCollision(this, e);
			}
            Proxy_Destroy();
		}
	}
	
    //Proxy Destroy lets us use the Concrete Class to recycle the Pooled Instance instead of actually Destroying it.
    public abstract void Proxy_Destroy();

    public void OnPooled()
    {
        //Show in Heirarchy
        gameObject.hideFlags &= ~HideFlags.HideInHierarchy;
		timeout_countdown = timeout_interval;
		destruction_check_timer = destruction_check_interval;
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

