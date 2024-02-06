using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPatternWave : AbstractBobaPattern
{
	Vector2 movement_direction;
	Vector2 virtual_position;
	float wave_magnitude;
	float wave_frequency;
	float move_speed;
	float start_time;

	public BobaPatternWave(Vector2 movement_direction, Vector2 virtual_position, float wave_magnitude, float wave_frequency, float move_speed) : base()
	{
		this.movement_direction = movement_direction;
		this.virtual_position = virtual_position;
		this.wave_magnitude = wave_magnitude;
		this.wave_frequency = wave_frequency;
		this.move_speed = move_speed;

		this.start_time = Time.time;
	}

	public override void onFixedUpdate(Rigidbody2D boba_body)
	{
		// virtual position is the boba's position if it were to simply fly in a straight line down movement_direction
		this.virtual_position = this.virtual_position + this.movement_direction * this.move_speed * Time.fixedDeltaTime;

		// find the offset for this frame from virtual position using Mathf.Sin
		float t = (Time.time - this.start_time) * this.wave_frequency;
		Vector2 offset = Vector2.Perpendicular(this.movement_direction) * Mathf.Sin(t) * this.wave_magnitude;
		
		// move the boba to its new location using virtual position and the offset
		boba_body.MovePosition(this.virtual_position + offset);
	}
}
