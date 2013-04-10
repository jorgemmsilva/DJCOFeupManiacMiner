using UnityEngine;
using System.Collections;

public class ninjaRope_script : MonoBehaviour {
	
	public GameObject avatar;
	public Rigidbody projectile;
	public float speed=10.0f;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
		{
			//this gives us the ray in the world right at the camera plane
			//need to cast ray to plane of avatar to know that position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(avatar.transform.TransformDirection(new Vector3(-1,0,0)), avatar.transform.position);
	        float ent = 100.0f;

	        if (plane.Raycast(ray, out ent))
			{
				//Debug.Log("Plane Raycast hit at distance: " + ent);
				var hitPoint = ray.GetPoint(ent);
				Rigidbody clone;
            	clone = Instantiate(projectile,avatar.transform.position, new Quaternion(0,0,0,1)) as Rigidbody;
	        	clone.velocity =(hitPoint-avatar.transform.position).normalized * speed;
			}
			//else
				//Debug.DrawRay (avatar.transform.position, ray.direction * 10, Color.red);
        }
	}
}