using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryStatePlaceholder : AbstractAngryState
{
	float wave_scale = 1.0f;

	public override float CooldownTimeBeforeShooting {get {return 0.1f;}}

	public AngryStatePlaceholder(AbstractMagicalGirlController mg) : base(mg)
	{}

	void ShootWave()
	{
		// This shoot function should be a lot more sophisticated
		// but as an example we will shoot boba in a wave

		GameObject projectileObject = GameObject.Instantiate(magicalGirl.enemyBulletPrefab, 
			magicalGirl.rigid2d.position + Vector2.up * 0.5f, 
			Quaternion.identity);

		EnemyBulletController projectile = projectileObject.GetComponent<EnemyBulletController>();
		projectile.SetPattern(new BobaPatternWave(Vector2.left, projectileObject.transform.position, 1.2f * this.wave_scale, 5.0f, 5.0f));

		// fix to flip wave direction each time we shoot to complete the example.
		this.wave_scale *= -1.0f;
	}

	public override void Shoot(){ ShootWave(); }
	// public override void Shoot(){this.ShootInCircle(300, BulletType.Wrath);}

	public override void Move(){}
}
