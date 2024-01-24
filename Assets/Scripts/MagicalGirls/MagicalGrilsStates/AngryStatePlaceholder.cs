using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryStatePlaceholder : AbstractAngryState
{
	float shoot_up = 1.0f;
	int barrage_count = 0;

	public override float CooldownTimeBeforeShooting {get {return 2.05f;}}

	public AngryStatePlaceholder(AbstractMagicalGirlController mg) : base(mg)
	{}

	void ShootWaveExample()
	{
		// The shoot function should be a lot more sophisticated
		// but as an example we will shoot boba in a wave

		// fix to create a barrage, will need to create a way to specify attack patterns.
		this.barrage_count += 1;
		if (this.barrage_count > 30) { return; }

		// Create the projectile
		GameObject projectileObject = GameObject.Instantiate(magicalGirl.enemyBulletPrefab, 
			magicalGirl.rigid2d.position + Vector2.up * 0.5f, 
			Quaternion.identity);
		EnemyBulletController projectile = projectileObject.GetComponent<EnemyBulletController>();

		// Create and set the move pattern
		projectile.SetPattern(new BobaPatternWave(Vector2.left, projectileObject.transform.position, 4.0f * this.shoot_up, 4.0f, 20.0f));

		// simple fix to flip wave direction each time we shoot.
		this.shoot_up *= -1.0f;
	}

	void ShootMissileExample()
	{
		// as another example we will shoot a missile boba

		// fix to create a barrage, we will need to create a better way to specify attack bursts.
		this.barrage_count += 1;
		if (this.barrage_count > 6) { return; }

		// Create projectile
		GameObject projectileObject = GameObject.Instantiate(magicalGirl.enemyBulletPrefab, 
			magicalGirl.rigid2d.position + Vector2.up * 0.5f, 
			Quaternion.identity);
        EnemyBulletController projectile = projectileObject.GetComponent<EnemyBulletController>();

		// Get the main character (through a singleton, sorry!)
		Rigidbody2D main_character_rigidbody = MainCharacterController.instance.GetComponent<Rigidbody2D>();
        Vector2 dir_to_main_character = (main_character_rigidbody.transform.position - projectileObject.transform.position).normalized;

		// launch dir will be ~70 degrees off from dir_to_main_character
        Vector2 launch_dir = Vector2.MoveTowards(
            dir_to_main_character,
            Vector2.Perpendicular(dir_to_main_character) * this.shoot_up,
            Mathf.Sin(70.0f)
        ).normalized;

		// Create and set the projectile move pattern
        projectile.SetPattern(new BobaPatternLaunchedMissile(
            main_character_rigidbody,
            launch_dir,
            8.0f,
            1.0f,
            20.0f,
            1.0f
        ));

		// fix to flip direction each time we shoot.
		this.shoot_up *= -1.0f;
    }

    //public override void Shoot(){ ShootMissileExample(); }
	public override void Shoot(){this.ShootInCircle(300, BulletType.Wrath);}

	public override void Move(){}
}
