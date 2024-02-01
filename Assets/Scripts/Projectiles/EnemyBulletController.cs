using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : AbstractProjectileController
{
	public override void OnCollisionEffect(MainCharacterController e)
	{
			e.TakeDamage();
    }

    public override void Proxy_Destroy()
    {
        Pools.Instance().enemyBulletPool.DestroyPoolable(this);
    }
}
