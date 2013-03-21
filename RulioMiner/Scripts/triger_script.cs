using UnityEngine;
using System.Collections;

public class triger_script : MonoBehaviour {
	
	public ParticleSystem particle_effect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0,1,0);
	}
	
	//se forçar por triggers não existira colisoes, no physx
	void OnTriggerEnter (Collider other) {
		if(other.tag=="Player")
		{
			if(particle_effect!=null) particle_effect.Play();
			audio.Play();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag=="Player")
		{
			if(particle_effect!=null) particle_effect.Play();
			audio.Play();
		}
        /*foreach (ContactPoint contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 0.1f);*/
    }
}
