using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The abstract mother magical girl class.
// All magical girls inherit from this
public abstract class AbstractMagicalGirlController : MonoBehaviour
{
	public bool isAngryAtStart;
	
	private float cooldownShootingTimer;
    private float directionChangeInterval = 5f;
    private float directionTimer = 5f;
    private float movementSpeed = 0.01f;
    private int x_angle = 0;
    private int y_angle = 0;

    // Each magical girl type has one angrystate and one happystate
    // that inherit from these two abstract classes.
    protected AbstractAngryState angryState;
	protected AbstractHappyState happyState;

	// Please set in editor
	public GameObject enemyBulletPrefab;
	public GameObject bobaBitPrefab;

	// we're using a State DP. Each state has 2 methods: Move() and Shoot()
	// Happy States will be shooting good bullets to help the MC
	// Angry States will be shooting stuff for the MC to avoid
	protected AbstractMagicalGirlState magicalGirlState;
	
	// Rigidbody2D. Do not set in Editor.
	public Rigidbody2D rigid2d;
	
	// ************************************************************************
	
	// Constructor
	public AbstractMagicalGirlController() : base()
	{
		float cooldownShootingTimer = 0;		
	}	
	
	// ************************************************************************
	
	public void TurnHappy()
	{
		// DO STUFF HERE
		magicalGirlState = happyState;
		
		// Resets the firing cooldown.
		cooldownShootingTimer = magicalGirlState.CooldownTimeBeforeShooting;
	}

    public virtual void Move()
    {
    }


    void ChangeDirection()
    {
        x_angle = UnityEngine.Random.Range(-180, 180);
        y_angle = UnityEngine.Random.Range(-180, 180);
    }
    // ************************************************************************

    // Start is called before the first frame update
    void Start()
    {
        cooldownShootingTimer = magicalGirlState.CooldownTimeBeforeShooting;
        rigid2d = GetComponent<Rigidbody2D>();
		
		if (isAngryAtStart)
			magicalGirlState = angryState;
		else magicalGirlState = happyState;

        ChangeDirection();
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

        this.transform.position += new Vector3(1 * x_angle, 1 * y_angle, 1) * movementSpeed * Time.deltaTime;

        directionTimer -= Time.deltaTime;
        if(directionTimer <= 0)
        {
            ChangeDirection();
            directionTimer = directionChangeInterval;
        }
    }
}
