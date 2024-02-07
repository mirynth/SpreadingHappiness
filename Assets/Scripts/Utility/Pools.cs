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
            instance.obj_bobabit = Resources.Load<GameObject>("Prefabs/BobaBit 1");
            instance.obj_wrath_proj = Resources.Load<GameObject>("Prefabs/EnemyBullet");

            //Create & Setup the object pools
            instance.bobaBitPool = new();
            instance.bobaBitPool.Initialize(128, () => { return GameObject.Instantiate(instance.obj_bobabit).GetComponent<BobaBitController>(); });
            instance.wrathBulletPool = new();
            instance.wrathBulletPool.Initialize(128, () => { return GameObject.Instantiate(instance.obj_wrath_proj).GetComponent<WrathBulletController>(); });
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
            instance.bobaBitPool.Destroy();
            instance.wrathBulletPool.Destroy();
            instance = null;
        }
    }

    GameObject obj_bobabit;
    GameObject obj_wrath_proj;

    //Pools for objects here.
    public ObjectPool<BobaBitController> bobaBitPool;
    public ObjectPool<WrathBulletController> wrathBulletPool;

}
