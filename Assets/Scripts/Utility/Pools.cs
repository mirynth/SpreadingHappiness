using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.activeSceneChanged += ChangedActiveScene;

            //cache the resource.load instead of searching every Instantiation.
            instance.obj_projectile_base = Resources.Load<GameObject>("Prefabs/ProjectileObject");
            instance.magical_girl_base = Resources.Load<GameObject>("Prefabs/MagicalGirlObject");

            //Create & Setup the object pools
            instance.projectilePool = new();
            instance.projectilePool.Initialize(256, () => { return GameObject.Instantiate(instance.obj_projectile_base).GetComponent<ProjectileController>(); });
            instance.magicalGirlPool = new();
            instance.magicalGirlPool.Initialize(32, () => { return GameObject.Instantiate(instance.magical_girl_base).GetComponent<MagicalGirlController>(); });
        }
        return instance;
    }

    static void ChangedActiveScene(Scene a, Scene b)
    {
        Clean();
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }

    public static void Clean()
    {
        if (instance != null)
        {
            instance.projectilePool.Destroy();
            instance.magicalGirlPool.Destroy();
            instance = null;
        }
    }

    GameObject obj_projectile_base;
    GameObject magical_girl_base;

    //Pools for objects here.
    public ObjectPool<ProjectileController> projectilePool;
    public ObjectPool<MagicalGirlController> magicalGirlPool;

}
