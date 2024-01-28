using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : AbstractProjectileController
{
	public override void OnCollisionEffect(MainCharacterController e)
	{
			e.TakeDamage();	
	}
}
