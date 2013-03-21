using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]
 
public class FPScont : MonoBehaviour {
 
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	
	private Camera cam;
	
	void Awake () {
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = true;
		cam=transform.Find("Camera").camera;
		
		animation.wrapMode = WrapMode.Loop;
   		
		animation["jump"].wrapMode = WrapMode.Once;
		animation["jump"].layer = 3;
		
		animation["strafeLeft"].layer = 2;
		animation["strafeRight"].layer = 1;
		//animation["strafeLeft"].blendMode = AnimationBlendMode.Additive;
		//animation["strafeRight"].blendMode = AnimationBlendMode.Additive;
		
		animation.Stop();
		
		animation.Play("strafeLeft");
		animation.Play("strafeRight");
		animation["strafeLeft"].weight = 0f;
		animation["strafeRight"].weight = 0f;
		
	}
 
	void FixedUpdate () {
		//constant forces here
		rigidbody.AddForce(new Vector3(0,-gravity,0), ForceMode.Acceleration);

		
		//constant no animation/normal here
		//TODO must check if on air
		animation.CrossFade("idle");
		animation["strafeLeft"].weight = 0f;
		animation["strafeRight"].weight = 0f;
		
		if(Input.GetButton("Jump"))
		{
			rigidbody.AddForce(new Vector3(0,150,0),ForceMode.Acceleration);
			animation.Play("jump");
		}
		//two dimension movement, this case Z is front and back, X is right and left, and axis Y not used this is locally speaking.
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		if(targetVelocity!=Vector3.zero)
		{
			//then rotate acording to camara view, not done always to permite see without rotating
			transform.eulerAngles = new Vector3(0,cam.GetComponent<camera_script>().getAngle().y,0);
			
			//normalize makes not running more rapidly if two dimensional movement
			targetVelocity.Normalize();
			if(targetVelocity.z>0)
			{
				if(targetVelocity.z>0.66) animation.CrossFade("sprint");
				else if(targetVelocity.z>0.33) animation.CrossFade("run");
				else animation.CrossFade("walk");
			}
			else if(targetVelocity.z<0)
			{
				animation.CrossFade("crouch");
			}
	
			if(targetVelocity.x>0)
			{
				animation["strafeRight"].weight = targetVelocity.x;
			}
			else if(targetVelocity.x<0)
			{
				animation["strafeLeft"].weight = -(targetVelocity.x);
			}
		}
		
		//pass to world view, hell if i know
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;
		Vector3 velocity = rigidbody.velocity;
		//axis not used must go 0 or else velocityChange will reverse current value on axis
		velocity.y=0;

		Vector3 velocityChange = (targetVelocity - velocity);
	    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		
	    rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	}
}

		/*
	    if (grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
			angle+=Input.GetAxis("Horizontal");
			transform.eulerAngles = new Vector3(0,angle,0);
			targetVelocity = Quaternion.AngleAxis(angle, Vector3.up) * targetVelocity;

	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
	        }
	    }
 
	    // We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
 
	    grounded = false;*/
 
	/*void OnCollisionStay () {
	    grounded = true;    
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}*/