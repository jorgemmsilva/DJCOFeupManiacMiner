using UnityEngine;
using System.Collections;

public class ninjaProjectile_script : MonoBehaviour {

	public GameObject avatar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("entrou: "+other.name + " - " + this.name);
		if (other.tag!="Player" && other.tag!="Floor")
		{
			avatar.rigidbody.AddForce(((other.transform.position - avatar.transform.position).normalized)*10,ForceMode.VelocityChange);
			Destroy(gameObject);
		}
    }
}
