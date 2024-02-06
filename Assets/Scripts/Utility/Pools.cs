using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Access with Pools.Instance().pool_type.Func()
 * probably CreatePoolable() or DestroyPoolable(type)
 * */
public class Pools 
{
    static Pools instance;

    public static Pools Instance()
    {
        if (instance == null)
        {
            instance = new Pools();

            //cache the resource.load instead of searching every Instantiation.
            instance.obj_projectile_base = Resources.Load<GameObject>("Prefabs/ProjectileObject");

            //Create & Setup the object pools
            instance.projectilePool = new();
            instance.projectilePool.Initialize(256, () => { return GameObject.Instantiate(instance.obj_projectile_base).GetComponent<ProjectileController>(); });
        }
        return instance;
    }

    public static void Clean()
    {
        if (instance != null)
        {
            instance.projectilePool.Destroy();
            instance = null;
        }
    }

    GameObject obj_projectile_base;

    //Pools for objects here.
    public ObjectPool<ProjectileController> projectilePool;

}
