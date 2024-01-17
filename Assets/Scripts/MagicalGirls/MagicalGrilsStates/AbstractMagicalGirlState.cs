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
	protected void ShootStraight(float angle, float force)
	{
		tempAngleConversion = angle*Math.PI/180;
		ShootStraight(new Vector2((float)Math.Cos(tempAngleConversion), 
							(float)Math.Sin(tempAngleConversion)),
						force);
	}
	
	// ************************************************************************
	
	// Shoot in a specific direction
	public abstract void ShootStraight(Vector2 direction, float force);
}
