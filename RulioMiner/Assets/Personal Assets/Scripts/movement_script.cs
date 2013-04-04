using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]
 
public class movement_script : MonoBehaviour {
 
	public float speed = 8.0f;
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

		animation.Stop();		
	}
 
	void FixedUpdate () {
		//constant forces here
		rigidbody.AddForce(new Vector3(0,-gravity,0), ForceMode.Acceleration);

		
		//constant no animation/normal here
		//TODO must check if on air
		animation.CrossFade("idle");
		
		if(Input.GetButton("Jump"))
		{
			//canJump=false;
			rigidbody.AddForce(new Vector3(0,500,0),ForceMode.Acceleration);
			animation.Play("jump");
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
			rotation += new Vector3(0,cam.GetComponent<camera_script>().getAngle().y,0);
		
			transform.eulerAngles = rotation;
			
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
			targetMovement *= speed;
			Vector3 velocity = rigidbody.velocity;
			//axis not used must go 0 or else velocityChange will reverse current value on axis
			velocity.y=0;
	
			Vector3 velocityChange = (targetMovement - velocity);
		    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			
		    rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			}
	}
}