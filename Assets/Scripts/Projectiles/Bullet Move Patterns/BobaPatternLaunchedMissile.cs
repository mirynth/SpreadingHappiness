using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPatternLaunchedMissile : AbstractBobaPattern
{
    enum MissileState
    {
        LaunchState,
        HomingState,
        LinearState
    }

    Rigidbody2D target;
    Vector2 velocity;
    float homing_delay;
    float homing_delay_current;
    float homing_speed;
    float max_turning_rate;
    MissileState missile_state;

    public BobaPatternLaunchedMissile(Rigidbody2D target, Vector2 launch_direction, float launch_speed, float homing_delay, float homing_speed, float max_turning_rate)
    {
        this.target = target;
        this.velocity = launch_direction.normalized * launch_speed;
        this.homing_delay = homing_delay;
        this.homing_delay_current = homing_delay;
        this.homing_speed = homing_speed;
        this.max_turning_rate = max_turning_rate;
        this.missile_state = MissileState.LaunchState;
    }

    public override void onFixedUpdate(Rigidbody2D boba_body)
    {
        switch (this.missile_state)
        {
            case MissileState.LaunchState:
                // When in the launch state, move along velocity given in constructor and slow down towards the end. 
                float t = (homing_delay_current / homing_delay);
                boba_body.MovePosition((Vector2)boba_body.transform.position + (this.velocity * t * Time.fixedDeltaTime));

                // countdown to the transition into homing state
                homing_delay_current -= Time.fixedDeltaTime;
                if (homing_delay_current < 0.0f)
                {
                    // When the timer finishes, set to homing state and set velocity towards player
                    this.missile_state = MissileState.HomingState;
                    this.velocity = (target.transform.position - boba_body.transform.position).normalized * homing_speed;
                }
                break;

            case MissileState.HomingState:
                // When in homing state we slightly adjust missile velocity towards
                // player each frame limited by the max turning rate
                Vector2 new_dir = Vector2.MoveTowards(this.velocity.normalized, (target.transform.position - boba_body.transform.position).normalized, Mathf.Sin(this.max_turning_rate * Time.fixedDeltaTime));
                this.velocity = new_dir.normalized * this.velocity.magnitude;

                boba_body.MovePosition((Vector2)boba_body.transform.position + this.velocity * Time.fixedDeltaTime);

                // Destroy the bullet after another 4 seconds.
                // if we dont do this the bullet eventually comes back...
                homing_delay_current -= Time.fixedDeltaTime;
                if (homing_delay_current < -4.0f)
                {
                    missile_state = MissileState.LinearState;
                }
                break;
            case MissileState.LinearState:
                boba_body.MovePosition((Vector2)boba_body.transform.position + this.velocity * Time.fixedDeltaTime);
                break;
        }
    }
}
