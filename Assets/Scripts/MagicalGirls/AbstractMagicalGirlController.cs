using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The abstract mother magical girl class.
// All magical girls inherit from this
public abstract class AbstractMagicalGirlController : MonoBehaviour
{
	// Each magical girl type has one angrystate and one happystate
	// that inherit from these two abstract classes.
	AbstractAngryState angryState;
	AbstractHappyState happyState;

	// we're using a State DP. Each state has 2 methods: Move() and Shoot()
	// Happy States will be shooting good bullets to help the MC
	// Angry States will be shooting stuff for the MC to avoid
	protected AbstractMagicalGirlState magicalGirlState;
	
	// ************************************************************************
	
	// Constructor
	public AbstractMagicalGirlController() : base()
	{
	}	
	
	// ************************************************************************
	
	public void TurnHappy()
	{
		// DO STUFF HERE
		magicalGirlState = happyState;
	}
}
