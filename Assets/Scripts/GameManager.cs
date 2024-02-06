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
    
    // Prefabs which represent each bullet type
    public GameObject bulletBobaPrefab;
    public GameObject bulletWrathPrefab;
    
    private void Awake()
    {
        GameManager.Instance = this;
    }

    // Converts an enum bullet type to the prefab
    public GameObject ConvertBulletTypeToPrefab(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.Boba:
                return bulletBobaPrefab;
            case BulletType.Wrath:
                return bulletWrathPrefab;
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
