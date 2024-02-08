using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalGirlController : MonoBehaviour, IPoolable
{
	private float cooldownShootingTimer;
    private float directionChangeInterval = 5f;
    private float directionTimer = 5f;
    private float movementSpeed = 500f;//0.01f;
    private int x_dir = 1;
    private int y_dir = 1;
    private Vector2 randomVector;

    // Each magical girl type has one angrystate and one happystate
    // that inherit from these two abstract classes.
    protected AbstractAngryState angryState;
	protected AbstractHappyState happyState;

	// we're using a State DP. Each state has 2 methods: Move() and Shoot()
	// Happy States will be shooting good bullets to help the MC
	// Angry States will be shooting stuff for the MC to avoid
	protected AbstractMagicalGirlState magicalGirlState;
	
	// Rigidbody2D. Do not set in Editor.
	private Rigidbody2D rigid2d;
    [NonSerialized] public SpriteRenderer magical_girl_renderable;

	// ************************************************************************
	
	public void TurnHappy()
	{
		// DO STUFF HERE
		magicalGirlState = happyState;
		
		// Resets the firing cooldown.
		cooldownShootingTimer = magicalGirlState.CooldownTimeBeforeShooting;
	}

    Vector2 ChangeDirection()
    {
        int randomAngle = Mathf.RoundToInt(UnityEngine.Random.Range(0, 360));
        Vector2 newVector = new Vector2(Mathf.Cos(randomAngle) * Mathf.Deg2Rad, Mathf.Sin(randomAngle) * Mathf.Deg2Rad);
        return newVector;
    }
    // ************************************************************************

	public void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        magical_girl_renderable = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
    }

    public void Setup(AbstractAngryState angry, AbstractHappyState happy, bool isAngry)
    {
        angryState = angry;
        happyState = happy;

        angry.Initialize(this);
        happy.Initialize(this);

        magicalGirlState = isAngry ? angryState : happyState;
        magicalGirlState.ApplyVisual(this);

        cooldownShootingTimer = magicalGirlState.CooldownTimeBeforeShooting;

        randomVector = ChangeDirection();
        directionTimer = directionChangeInterval;
    }

	// ************************************************************************
	
    // Update is called once per frame
    void Update()
    {        
        cooldownShootingTimer -= Time.deltaTime;
		if (cooldownShootingTimer < 0)
		{
			// Shoots once the timer is over.
			magicalGirlState.Shoot();
			cooldownShootingTimer += magicalGirlState.CooldownTimeBeforeShooting;
		}

        // Bounds Reached
        if (this.transform.position.x < -80 || this.transform.position.x > 80)
            x_dir *= -1;
        if (this.transform.position.y < -40 || this.transform.position.y > 40)
            y_dir *= -1;


        this.transform.position += new Vector3(x_dir*randomVector.x , y_dir*randomVector.y , 0) * movementSpeed * Time.deltaTime;
        //this.transform.position *= new Vector3(x_delta, y_delta, 1);


        directionTimer -= Time.deltaTime;
        if(directionTimer <= 0)
        {
            randomVector = ChangeDirection();
            directionTimer = directionChangeInterval;
        }
    }

    public void OnPooled()
    {
        //Show in Heirarchy
        gameObject.hideFlags &= ~HideFlags.HideInHierarchy;
    }

    public void OnUnPooled()
    {
        //Hide in Hierarchy
        gameObject.hideFlags |= HideFlags.HideInHierarchy;
        magicalGirlState.RemoveVisual(this);
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
