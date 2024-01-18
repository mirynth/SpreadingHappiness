using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Boba Pattern provides explicit control of a boba projectile's movement each fixed update
public abstract class AbstractBobaPattern
{
	public abstract void onFixedUpdate(Rigidbody2D boba_body);
}

// An implementation that does nothing.
public class BobaPatternDoNothing : AbstractBobaPattern
{
	public override void onFixedUpdate(Rigidbody2D boba_body)
	{
	}
}
