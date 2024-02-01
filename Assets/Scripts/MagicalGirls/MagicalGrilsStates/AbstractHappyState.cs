using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHappyState : AbstractMagicalGirlState
{
	public AbstractHappyState(AbstractMagicalGirlController mg) : base(mg){}
	
	// Shoot in a specific direction
	public override void ShootStraight(Vector2 direction, float force, BulletType bulletType)
	{
        BobaBitController projectile = Pools.Instance().bobaBitPool.CreatePoolable();
        projectile.transform.SetPositionAndRotation(magicalGirl.rigid2d.position + Vector2.up * 0.5f, Quaternion.identity);


        LaunchProjectiles(projectile.gameObject, direction, force, BulletType.Boba, new BobaPatternDoNothing());
	}

}
