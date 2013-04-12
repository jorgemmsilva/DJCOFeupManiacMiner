using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]
 
public class movement_script : MonoBehaviour {
 
	public float speed = 8.0f;
	public float jump_perc_speed = 0.2f;
	public float jump_speed = 2f;
	public float maxVelocityChange = 10.0f;
	public float gravity = 30.0f;
	public float height_max_jump_time_ms = 100f;
	
	bool jumping = false;
	//dummy value, just needs to be above zero
	float cur_jump_time = 1;
	
	Vector3 cur_rotation;
	void Awake () {
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;
		cur_rotation=transform.rotation.eulerAngles;
		
		animation.wrapMode = WrapMode.Loop;
   		
		animation["jump"].wrapMode = WrapMode.ClampForever;
		animation["jump"].layer = 3;

		animation.Stop();		
	}
 
	void FixedUpdate () {
		//constant forces here
		rigidbody.AddForce(new Vector3(0,-gravity,0), ForceMode.Acceleration);

		
		//constant no animation/normal here
		animation.CrossFade("idle");
		
		if(cur_jump_time>0)
		{
			if(Input.GetButton("Jump"))
			{
				if(jumping == false)
				{
					//initialization of animation and values
					animation.Play("jump");
					jumping=true;
					cur_jump_time = height_max_jump_time_ms;
				}
				else
				{
					cur_jump_time -= Time.deltaTime * 1000f ;
				}
				rigidbody.AddForce(new Vector3(0,jump_speed,0),ForceMode.VelocityChange);
			}
			else
			{
				//so it doesnt allow multiple mini jumps midair
				if(jumping) cur_jump_time = 0;
			}	
		}

		//two dimension movement, this case Z is front and back, X is right and left, and axis Y not used this is locally speaking.
		Vector3 targetMovement = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));

		//normalize makes not running more rapidly if two dimensional movement
		targetMovement.Normalize();
		if(targetMovement!=Vector3.zero)
		{
			float angle;
			if(targetMovement.z<0) angle=-180;
			else angle=0;
			
			//then rotate acording to that and add camara view
			Vector3 rotation= new Vector3(0.0f,angle,0.0f);
			//rotation += new Vector3(0,cam.GetComponent<camera_script>().getAngle().y,0);
		
			transform.eulerAngles = cur_rotation + rotation;
			
			if(targetMovement.magnitude>0)
			{
				if(targetMovement.magnitude>0.66) animation.CrossFade("sprint");
				else if(targetMovement.magnitude>0.33) animation.CrossFade("run");
				else animation.CrossFade("walk");
			}
			

			//pass to world view, hell if i know
			//i do know. what it does is takes the Vector that you pass, this case (0,0,targetMovement.magnitude) and makes it relative to the one
			//you called the function on, this case transform. In this case it's telling that targetMovement will yeld
			//the vector that globally represents the direction (0,0,targetMovement.magnitude) if you are the transform. This tell us where to move
			//can't use targetMovement inside function because you're rotated and always want to move forward in Z, even if z value is <0
			targetMovement = transform.TransformDirection(new Vector3(0,0,targetMovement.magnitude));
			
			if (jumping)
			{
				if(((rigidbody.velocity.z<0 && targetMovement.z>0)||(rigidbody.velocity.z>0 && targetMovement.z<0)) || (rigidbody.velocity.z < speed && rigidbody.velocity.z > -speed))
				{
					targetMovement *= (speed * jump_perc_speed);
					rigidbody.AddForce(targetMovement, ForceMode.Acceleration);
				}
			}
			else
			{
				targetMovement *= speed;
				Vector3 velocity = rigidbody.velocity;
				//axis not used must go 0 or else velocityChange will reverse current value on axis
				velocity.y=0;
		
				Vector3 velocityChange = (targetMovement - velocity);
			    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				
			    rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			}
			
		}
		else
		{
			if(!jumping)
			{
				//simulate drag
				Vector3 velocityChange = -rigidbody.velocity;
				velocityChange.y = 0;
				rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			}
		}
	}
	
	//checking if is jumping and colided with foot on surface
	void OnCollisionEnter(Collision collision)
	{
		if(jumping)
		{
			//Debug.Log("entrou");
        	foreach (ContactPoint contact in collision.contacts)
			{
				
				//Debug.Log ("ponto: " + contact.point + "  normal: " + contact.normal );
				if(contact.normal.y<0.8&&contact.normal.y>-0.8)
				{
					//he didnt hit on solid ground
					return;
				}
            	Debug.DrawRay(contact.point, contact.normal, Color.white);
        	}
			cur_jump_time = height_max_jump_time_ms;
			jumping=false;
			animation.Stop("jump");
		}
    }
	
	
	public void changeGravity()
	{
		gravity=-gravity;
		jump_speed=-jump_speed;
		cur_rotation = cur_rotation + new Vector3(0,0,180);
		transform.RotateAround (transform.collider.bounds.center, transform.TransformDirection(Vector3.forward), 180);

	}
	
	public void forceJump()
	{
		jumping=true;
	}
	
	public void rotate90()
	{
		cur_rotation = cur_rotation + new Vector3(0,90,0);
	}
	
	public void rotateminus90()
	{
		cur_rotation = cur_rotation + new Vector3(0,-90,0);
	}	
}