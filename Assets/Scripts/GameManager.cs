using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Boba,
    Wrath,
    Sloth,
    Pride,
    Envy,
    Lust,
    Gluttony,
    Greed
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        GameManager.Instance = this;
        Pools.Clean();
    }

    // Converts an enum bullet type to the prefab
    public GameObject CreatePoolableFromBulletType(BulletType bulletType)
    {
        GameObject obj = Pools.Instance().projectilePool.CreatePoolable().gameObject;
        switch (bulletType)
        {
            case BulletType.Boba:
                obj.GetComponent<AbstractProjectileController>().SetEffect(new BobaBulletEffect());
                break;
            case BulletType.Wrath:
                obj.GetComponent<AbstractProjectileController>().SetEffect(new WrathBulletEffect());
                break;
            case BulletType.Sloth:
                // @TODO: create this bullet type
            case BulletType.Pride:
                // @TODO: create this bullet type
            case BulletType.Envy:
                // @TODO: create this bullet type
            case BulletType.Lust:
                // @TODO: create this bullet type
            case BulletType.Greed:
                obj.GetComponent<AbstractProjectileController>().SetEffect(new GreedBulletEffect(0.25f, 12.0f, 4.0f));
                break;
            default: 
                return null;
        }
        return obj;
    }
}
