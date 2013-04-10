using UnityEngine;
using System.Collections;

public class avatar_script : MonoBehaviour {
	
	private Vector3 last_checkpoint;
	// Use this for initialization
	void Start () {
		last_checkpoint = transform.position;
	}
		
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if(hit.gameObject.tag=="enemy")
		{
			died ();
		}
		if(hit.gameObject.tag=="checkpoint")
		{
			setCheckpoint (hit.gameObject.transform.position);
		}
	}
	
	public void setCheckpoint(Vector3 check) 
	{
		last_checkpoint = check;
	}

	
	//acabou de morrer
	public void died() 
	{
		transform.position = last_checkpoint;
		transform.rigidbody.velocity = new Vector3(0,0,0);
	}
	
}
