using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBulletController : AbstractProjectileController
{
	public override void OnCollisionEffect(MainCharacterController e)
	{
			e.TakeDamage();	
	}

    public override void Proxy_Destroy()
    {
        Pools.Instance().wrathBulletPool.DestroyPoolable(this);
    }
}
