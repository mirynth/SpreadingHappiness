using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AbstractMagicalGirlState 
{
    // ---- Init variables ---- //
    float shoot_up = 1.0f;
    int barrage_count = 0;

    // Base Shoot function
    public abstract void Shoot();

    // Time to wait between 2 shots in seconds.
    public abstract float CooldownTimeBeforeShooting {get; }
    public abstract void ApplyVisual(MagicalGirlController parent_controller);
    public abstract void RemoveVisual(MagicalGirlController parent_controller);

    protected MagicalGirlController magicalGirl;
	
	// ************************************************************************
	
	public void Initialize(MagicalGirlController parent)
	{
		this.magicalGirl = parent;
	}

    // ************************************************************************

    // Converts an angle in degrees into a vector pointing at that angle from origin (in °, 0° = right)
    protected static Vector2 AngleToVector(float angleDeg)
    {
		float angleRad = angleDeg*Mathf.Deg2Rad;
        return new Vector2(
            (float)Math.Cos(angleRad),
            (float)Math.Sin(angleRad)
        ).normalized;
    }
	
    protected void ShootInCircle(float speed, BulletType bulletType)
    {
        for(int i = 0; i <= 7; i++)
        {
            LaunchProjectile(bulletType, new BobaPatternSimpleMove(AngleToVector(45 * i), speed));
        }
    }

    protected void ShootWave(BulletType bulletType)
    {
        // The shoot function should be a lot more sophisticated
        // but as an example we will shoot boba in a wave

        // fix to create a barrage, will need to create a way to specify attack patterns.
        this.barrage_count += 1;
        if (this.barrage_count > 30) { return; }

        AbstractBobaPattern pattern = new BobaPatternWave(Vector2.left, magicalGirl.transform.position, 4.0f * this.shoot_up, 4.0f, 20.0f);

        LaunchProjectile(bulletType, pattern);


        /*
        EnemyBulletController projectile = projectileObject.GetComponent<EnemyBulletController>();

        // Create and set the move pattern
        projectile.SetPattern();
        */

        // simple fix to flip wave direction each time we shoot.
        this.shoot_up *= -1.0f;
    }

    protected void ShootMissile(BulletType bulletType, float speed = 8.0f, float home_speed = 20.0f, float max_turning_rate = 1.0f)
    {
        // as another example we will shoot a missile boba

        // fix to create a barrage, we will need to create a better way to specify attack bursts.
        this.barrage_count += 1;
        if (this.barrage_count > 6) { return; }

        // Get the main character (through a singleton, sorry!)
        Rigidbody2D main_character_rigidbody = MainCharacterController.instance.GetComponent<Rigidbody2D>();
        Vector2 dir_to_main_character = (main_character_rigidbody.transform.position - magicalGirl.transform.position).normalized;

        // launch dir will be ~70 degrees off from dir_to_main_character
        Vector2 launch_dir = Vector2.MoveTowards(
            dir_to_main_character,
            Vector2.Perpendicular(dir_to_main_character) * this.shoot_up,
            Mathf.Sin(70.0f)
        ).normalized;

        AbstractBobaPattern pattern = new BobaPatternLaunchedMissile(
            main_character_rigidbody,
            launch_dir,
            speed,
            1.0f,
            home_speed,
            max_turning_rate
        );

        LaunchProjectile(bulletType, pattern);

        // fix to flip direction each time we shoot.
        this.shoot_up *= -1.0f;
    }
    // ************************************************************************

    // Instantiate and launch a projectile of the given type from the given initial position and with the given movement pattern
    protected void LaunchProjectile(BulletType bulletType, AbstractBobaPattern pattern)
    {
        LaunchProjectile(bulletType, pattern, magicalGirl.transform.position);
    }
    protected void LaunchProjectile(BulletType bulletType, AbstractBobaPattern pattern, Vector2 instantiation_position)
    {
        GameObject projectileObject = GameManager.Instance.CreatePoolableFromBulletType(bulletType);
        projectileObject.transform.SetPositionAndRotation(instantiation_position, Quaternion.identity);

        projectileObject.GetComponent<AbstractProjectileController>().SetPattern(pattern);
    }
}
