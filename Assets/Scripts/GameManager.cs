using Cinemachine;
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

    StageManager stage_manager;
    VNManager vn_manager;

    public CinemachineVirtualCamera virtual_camera;
    public PolygonCollider2D stage_bounds;
    public PolygonCollider2D boss_bounds;

    private void Awake()
    {
        GameManager.Instance = this;
        Pools.Clean();
        stage_manager = GetComponent<StageManager>();
        vn_manager = GetComponent<VNManager>();
    }

    private void Start()
    {
        vn_manager.Initialize();
        stage_manager.Initialize();
    }

    // Converts an enum bullet type to the prefab
    public GameObject CreatePoolableFromBulletType(BulletType bulletType)
    {
        AbstractProjectileController controller = Pools.Instance().projectilePool.CreatePoolable();

        switch (bulletType)
        {
            case BulletType.Boba:
                controller.SetEffect(new BobaBulletEffect());
                break;
            case BulletType.Wrath:
                controller.SetEffect(new WrathBulletEffect());
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
                controller.SetEffect(new GreedBulletEffect(0.25f, 12.0f, 4.0f));
                break;
            default: 
                return null;
        }
        return controller.gameObject;
    }

    public GameObject CreatePoolableFromMagicalGirl(AbstractAngryState angry, AbstractHappyState happy, bool isAngry, Vector3 position)
    {
        MagicalGirlController obj = Pools.Instance().magicalGirlPool.CreatePoolable();

        obj.Setup(angry, happy, isAngry);
        obj.transform.position = position;

        return obj.gameObject;
    }

    public void SetCameraBounds(PolygonCollider2D bounds)
    {
        virtual_camera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = bounds;
    }

    public void SetPlayerInputable(bool playable)
    {
        MainCharacterController.instance.input_on = playable;
    }
}
