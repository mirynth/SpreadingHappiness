using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBulletController : AbstractProjectileController
{
	public override void OnCollisionEffect(MainCharacterController e)
	{
			e.TakeDamage();	
	}
}
