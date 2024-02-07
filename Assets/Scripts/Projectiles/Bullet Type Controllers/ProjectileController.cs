using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProjectileController : AbstractProjectileController
{
    public override void Proxy_Destroy()
    {
        Pools.Instance().projectilePool.DestroyPoolable(this);
    }
}
