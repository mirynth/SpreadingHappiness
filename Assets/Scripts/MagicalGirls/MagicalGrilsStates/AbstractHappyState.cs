using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHappyState : AbstractMagicalGirlState
{
	public AbstractHappyState(AbstractMagicalGirlController mg) : base(mg){}
	
	// Shoot in a specific direction
	public override void ShootStraight(Vector2 direction, float force)
	{
		GameObject projectileObject = GameObject.Instantiate(magicalGirl.bobaBitPrefab, 
			magicalGirl.rigid2d.position + Vector2.up * 0.5f, 
			Quaternion.identity);

		BobaBitController projectile = projectileObject.GetComponent<BobaBitController>();
		projectile.Launch(direction, force);
	}
}
