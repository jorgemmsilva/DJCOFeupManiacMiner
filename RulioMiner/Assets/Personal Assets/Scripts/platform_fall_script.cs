using UnityEngine;
using System.Collections;

public class platform_fall_script : MonoBehaviour {
	
	public bool respawn = false;
	public float time_ms_fall = 3000f;
	public float time_to_respawn = 3000f;
	
	private bool activated = false;
	private bool actived_v2 = false;
	private float time_passed = 0.0f;
	
	void Update () {
		time_passed += Time.deltaTime * 1000.0f;
		
		//Debug.Log ("time: " +  
		if((activated||!respawn) && time_passed > time_ms_fall)
		{
			if(!respawn)
			{
				Destroy(gameObject);
			}
			else
			{
				renderer.enabled = false;
				collider.enabled = false;
				if(time_passed > time_to_respawn + time_ms_fall)
				{
						renderer.enabled = true;
						collider.enabled = true;
						activated = false;
						time_passed = 0.0f;

				}

			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player" && activated == false)
		{
			activated = true;
			time_passed = 0.0f;
			//TODO change color, something on renderer
		}
    }	
}
