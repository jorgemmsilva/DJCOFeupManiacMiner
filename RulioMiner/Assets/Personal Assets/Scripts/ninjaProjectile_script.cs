using UnityEngine;
using System.Collections;

public class ninjaProjectile_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log("entrou: "+other.name + " - " + this.name);
		if (other.tag!="Player" && other.tag!="floor")
		{
			GameObject avatar = GameObject.FindGameObjectWithTag("Player");
			//avatar.GetComponent<ThirdPersonController>().addVel(((other.transform.position - avatar.transform.position).normalized)*10);
			
			//avatar.GetComponent<CharacterController>().enabled = false;
			
			//	addVel(((other.transform.position - avatar.transform.position).normalized)*10);
			
			avatar.rigidbody.AddForce(((other.transform.position - avatar.transform.position).normalized)*10,ForceMode.VelocityChange);
			gameObject.rigidbody.velocity = Vector3.zero;
			Destroy(gameObject);
			//this.transform.Find("Particle System").particleSystem.enableEmission=false;
		}
    }
}
