using UnityEngine;
using System.Collections;

public class avatar_script : MonoBehaviour {
	
	private Vector3 last_checkpoint;
	private int number_jolas;
	
	// Use this for initialization
	void Start () {
		last_checkpoint = transform.position;
		number_jolas = 0;
	}
		
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if(hit.gameObject.tag=="enemy")
		{
			died ();
		}
	}
	
	public void setCheckpoint(Vector3 check) 
	{
		last_checkpoint = check;
	}

	public void died() 
	{
		transform.position = last_checkpoint;
		transform.rigidbody.velocity = new Vector3(0,0,0);
	}
	
	public void add_jola()
	{
		number_jolas++;
		Debug.Log(number_jolas+" jolas");
	}
	
}
