using UnityEngine;
using System.Collections;

public class addplatform_script : MonoBehaviour {
	
	public GameObject platform;
	public GameObject avatar;
	
	int number = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")&&number>0) 
		{
			//this gives us the ray in the world right at the camera plane
			//need to cast ray to plane of avatar to know that position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(avatar.transform.TransformDirection(new Vector3(-1,0,0)), avatar.transform.position);
	        float ent = 100.0f;

	        if (plane.Raycast(ray, out ent))
	        {
				var hitPoint = ray.GetPoint(ent);

				GameObject clone;
            	clone = Instantiate(platform, hitPoint, new Quaternion(0,0,0,1)) as GameObject;
				number--;
			}
		}
	}
	
	public void addPower (int number_ch)
	{
		number=number_ch;
	}
}