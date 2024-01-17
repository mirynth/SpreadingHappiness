using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAngryState : AbstractMagicalGirlState
{	
	public AbstractAngryState(AbstractMagicalGirlController mg) : base(mg){}
	
	// Shoot in a specific direction
	public override void ShootStraight(Vector2 direction, float force)
	{
		GameObject projectileObject = GameObject.Instantiate(magicalGirl.enemyBulletPrefab, 
			magicalGirl.rigid2d.position + Vector2.up * 0.5f, 
			Quaternion.identity);

		EnemyBulletController projectile = projectileObject.GetComponent<EnemyBulletController>();
		projectile.Launch(direction, force);
	}
}
