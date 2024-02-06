using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaBitController : AbstractProjectileController
{	
	public override void OnCollisionEffect(MainCharacterController e)
	{
			e.IncrementBobaBitCount();		
	}

    public override void Proxy_Destroy()
    {
        Pools.Instance().bobaBitPool.DestroyPoolable(this);
    }
}
