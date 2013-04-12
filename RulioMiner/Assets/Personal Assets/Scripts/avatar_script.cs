using UnityEngine;
using System.Collections;

public class avatar_script : MonoBehaviour {
	
	private Vector3 last_checkpoint;
	private int number_jolas;
	private bool z_axis_lock = false;
	
	// Use this for initialization
	void Start ()
	{
		last_checkpoint = transform.position;
		number_jolas = 0;
	}
	
	void Update ()
	{
		Vector3 temp = transform.position;

		if (z_axis_lock) temp.z = last_checkpoint.z;
		else temp.x = last_checkpoint.x;

		transform.position=temp;
	}
		
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if(hit.gameObject.tag=="enemy")
		{
			died ();
		}
	}
	
	public void setCheckpoint(Vector3 check, int sign) 
	{
		last_checkpoint = check;
		if (sign==1)
		{
			GetComponent<movement_script>().rotate90();
			Camera.main.GetComponent<camera_script>().Rot90();
			z_axis_lock=!z_axis_lock;
			transform.position = last_checkpoint;
		}
		else if (sign==-1)
		{
			GetComponent<movement_script>().rotateminus90();
			Camera.main.GetComponent<camera_script>().Rot270();
			z_axis_lock=!z_axis_lock;
			transform.position = last_checkpoint;
		}
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
