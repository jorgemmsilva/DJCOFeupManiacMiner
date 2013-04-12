using UnityEngine;
using System.Collections;

public class ninjaRope_script : MonoBehaviour {
	
	public GameObject avatar;
	public GameObject projectile;
	public float speed=10.0f;
	
	int number = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")&&number>0) 
		{
			Vector3 center = avatar.collider.bounds.center;
			//this gives us the ray in the world right at the camera plane
			//need to cast ray to plane of avatar to know that position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(avatar.transform.TransformDirection(new Vector3(-1,0,0)), center);
	        float ent = 100.0f;

	        if (plane.Raycast(ray, out ent))
			{
				var hitPoint = ray.GetPoint(ent);
				GameObject clone;
            	clone = Instantiate(projectile,center, new Quaternion(0,0,0,1)) as GameObject;
	        	clone.rigidbody.velocity =(hitPoint-center).normalized * speed;
				number--;
			}
        }
	}
	
	public void addPower (int number_ch)
	{
		number=number_ch;
	}
}