using UnityEngine;
using System.Collections;

public class ninjaProjectile_script : MonoBehaviour {
	
	public float pull = 30.0f;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag!="Player")
		{
			GameObject avatar = GameObject.FindGameObjectWithTag("Player");
			//avatar.GetComponent<ThirdPersonController>().addVel(((other.transform.position - avatar.transform.position).normalized)*10);
			
			avatar.GetComponent<movement_script>().forceJump();
			avatar.rigidbody.AddForce(((transform.position - avatar.collider.bounds.center).normalized)*pull,ForceMode.VelocityChange);
			
			Destroy(gameObject);
		}
    }
}
