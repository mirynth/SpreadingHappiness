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
        switch (bulletType)
        {
            case BulletType.Boba:
                return Pools.Instance().bobaBitPool.CreatePoolable().gameObject;
            case BulletType.Wrath:
                return Pools.Instance().wrathBulletPool.CreatePoolable().gameObject;
            case BulletType.Sloth:
                // @TODO: create this bullet type
            case BulletType.Pride:
                // @TODO: create this bullet type
            case BulletType.Envy:
                // @TODO: create this bullet type
            case BulletType.Lust:
                // @TODO: create this bullet type
            case BulletType.Greed:
                // @TODO: create this bullet type
            default: 
                return null;
        }
    }
}
