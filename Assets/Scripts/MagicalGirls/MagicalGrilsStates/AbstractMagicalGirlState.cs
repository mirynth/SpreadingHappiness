using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMagicalGirlState
{
    // ---- Init variables ---- //
    float shoot_up = 1.0f;
    int barrage_count = 0;

    // Base Shoot function
    public abstract void Shoot();

    // Time to wait between 2 shots in seconds.
    public abstract float CooldownTimeBeforeShooting {get;}
	
    public enum BulletType
    {
        None,
        Boba,
        Wrath,
        Sloth,
        Pride,
        Envy,
        Lust,
        Gluttony,
        Greed
    }
	
	private double tempAngleConversion;
	protected AbstractMagicalGirlController magicalGirl;
	
	// ************************************************************************
	
	public AbstractMagicalGirlState(
		AbstractMagicalGirlController mg)
	{
		this.magicalGirl = mg;
	}

    // ************************************************************************
    
    // Shoot in an angle (in °, 0° = shooting right)
    protected void ShootStraight(float angle, float force, BulletType bulletType)
	{
		tempAngleConversion = angle*Math.PI/180;
		ShootStraight(new Vector2((float)Math.Cos(tempAngleConversion), 
							(float)Math.Sin(tempAngleConversion)),
						force, bulletType);
	}
	
    protected void ShootInCircle(float force, BulletType bulletType)
    {
        for(int i = 0; i <= 7; i++)
        {
            ShootStraight(45*i, force, bulletType);
        }
    }

    protected void ShootWave(float force, BulletType bulletType)
    {
        // The shoot function should be a lot more sophisticated
        // but as an example we will shoot boba in a wave

        // fix to create a barrage, will need to create a way to specify attack patterns.
        this.barrage_count += 1;
        if (this.barrage_count > 30) { return; }

        // Create the projectile
        EnemyBulletController projectile = Pools.Instance().enemyBulletPool.CreatePoolable();
        projectile.transform.SetPositionAndRotation(magicalGirl.rigid2d.position + Vector2.up * 0.5f, Quaternion.identity);

        AbstractBobaPattern pattern = new BobaPatternWave(Vector2.left, projectile.gameObject.transform.position, 4.0f * this.shoot_up, 4.0f, 20.0f);
        LaunchProjectiles(projectile.gameObject, Vector2.left, force, bulletType, pattern);


        /*
        EnemyBulletController projectile = projectileObject.GetComponent<EnemyBulletController>();

        // Create and set the move pattern
        projectile.SetPattern();
        */

        // simple fix to flip wave direction each time we shoot.
        this.shoot_up *= -1.0f;
    }

    protected void ShootMissile(float force, BulletType bulletType)
    {
        // as another example we will shoot a missile boba

        // fix to create a barrage, we will need to create a better way to specify attack bursts.
        this.barrage_count += 1;
        if (this.barrage_count > 6) { return; }

        // Create projectile
        EnemyBulletController projectile = Pools.Instance().enemyBulletPool.CreatePoolable();
        projectile.transform.SetPositionAndRotation(magicalGirl.rigid2d.position + Vector2.up * 0.5f, Quaternion.identity);


        // Get the main character (through a singleton, sorry!)
        Rigidbody2D main_character_rigidbody = MainCharacterController.instance.GetComponent<Rigidbody2D>();
        Vector2 dir_to_main_character = (main_character_rigidbody.transform.position - projectile.gameObject.transform.position).normalized;

        // launch dir will be ~70 degrees off from dir_to_main_character
        Vector2 launch_dir = Vector2.MoveTowards(
            dir_to_main_character,
            Vector2.Perpendicular(dir_to_main_character) * this.shoot_up,
            Mathf.Sin(70.0f)
        ).normalized;

        // Create and set the projectile move pattern
        /*
        projectile.SetPattern(new BobaPatternLaunchedMissile(
            main_character_rigidbody,
            launch_dir,
            8.0f,
            1.0f,
            20.0f,
            1.0f
        ));*/

        AbstractBobaPattern pattern = new BobaPatternLaunchedMissile(
            main_character_rigidbody,
            launch_dir,
            8.0f,
            1.0f,
            20.0f,
            1.0f
        );
        LaunchProjectiles(projectile.gameObject, launch_dir, force, bulletType, pattern);


        // fix to flip direction each time we shoot.
        this.shoot_up *= -1.0f;
    }
    // ************************************************************************

    protected void LaunchProjectiles(GameObject projectileObject, Vector2 direction, float force, BulletType bulletType, AbstractBobaPattern pattern)
    {
        switch(bulletType)
        {
            case BulletType.Boba:
                BobaBitController bobaProjectile = projectileObject.GetComponent<BobaBitController>();
                bobaProjectile.Launch(direction, force);
                bobaProjectile.SetPattern(pattern);
                break;
            case BulletType.Wrath:
                EnemyBulletController wrathProjectile = projectileObject.GetComponent<EnemyBulletController>();
                wrathProjectile.Launch(direction, force);
                wrathProjectile.SetPattern(pattern);
                break;
        }
        
    }
	// Shoot in a specific direction
	public abstract void ShootStraight(Vector2 direction, float force, BulletType bulletType);
}
