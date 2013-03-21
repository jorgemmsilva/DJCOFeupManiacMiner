using UnityEngine;
using System.Collections;

public class colision_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {     
		
		if ( collision.gameObject.tag == "Sphere")
		{
			//collision.gameObject.rigidbody.AddForce(0,200f,0);
			
			foreach (ContactPoint contact in collision.contacts) {
				Debug.Log(contact.normal);
				collision.gameObject.rigidbody.AddForce((-contact.normal)*100);
        	}
			if(audio.isPlaying) audio.Stop();
			else audio.Play ();
		}
    }
}
