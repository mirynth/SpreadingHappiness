using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMagicalGirlState
{
	// Time to wait between 2 shots in seconds.
	public abstract float CooldownTimeBeforeShooting {get;}
	
	public abstract void Shoot();
	public abstract void Move();

    public enum BulletType
    {
        None,
        Boba,
        Wrath,
        Sloth,
        Pride,
        Envy,
        Lust,
        Gluttony,
        Greed
    }
	
	private double tempAngleConversion;
	protected AbstractMagicalGirlController magicalGirl;
	
	// ************************************************************************
	
	// The only way to guarantee that a projectile prefab will be there
	// Is to only allow a constructor with a parameter.
	// TODO: there might be a better solution but I don't know it and
	// I'm too tired 
	public AbstractMagicalGirlState(
		AbstractMagicalGirlController mg)
	{
		this.magicalGirl = mg;
	}
	
	// ************************************************************************
	
	// Shoot in an angle (in °, 0° = shooting right)
	protected void ShootStraight(float angle, float force, BulletType bulletType)
	{
		tempAngleConversion = angle*Math.PI/180;
		ShootStraight(new Vector2((float)Math.Cos(tempAngleConversion), 
							(float)Math.Sin(tempAngleConversion)),
						force, bulletType);
	}
	
    protected void ShootInCircle(float force, BulletType bulletType)
    {
        for(int i = 0; i <= 7; i++)
        {
            ShootStraight(45*i, force, bulletType);
        }
    }
	// ************************************************************************
	
    protected void LaunchProjectiles(GameObject projectileObject, Vector2 direction, float force, BulletType bulletType)
    {
        switch(bulletType)
        {
            case BulletType.Boba:
                BobaBitController bobaProjectile = projectileObject.GetComponent<BobaBitController>();
                bobaProjectile.Launch(direction, force);
                break;
            case BulletType.Wrath:
                EnemyBulletController wrathProjectile = projectileObject.GetComponent<EnemyBulletController>();
                wrathProjectile.Launch(direction, force);
                break;
        }
        
    }
	// Shoot in a specific direction
	public abstract void ShootStraight(Vector2 direction, float force, BulletType bulletType);
}
