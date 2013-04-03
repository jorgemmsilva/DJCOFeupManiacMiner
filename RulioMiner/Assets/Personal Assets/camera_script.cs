using UnityEngine;
using System.Collections;

public class camera_script : MonoBehaviour {
	
	public GameObject target;
	Vector3 offset;
	Quaternion offset_rotation;
	Vector3 angle;
	Vector3 maxangle=new Vector3(30,360,0);
	
	void Awake () {
		offset = target.transform.position - transform.position;
		offset_rotation = transform.rotation;
	}

	void Start () {
	}
	// Update is called once per frame
	void Update () {
		angle.x -= Input.GetAxis("Mouse Y");
		angle.x = angle.x % 360;
		angle.x = Mathf.Clamp(angle.x,-maxangle.x,maxangle.x);
		angle.y += Input.GetAxis("Mouse X");
		angle.y = angle.y % 360;
		angle.y = Mathf.Clamp(angle.y,-maxangle.y,maxangle.y);
		
		Quaternion rotation = Quaternion.Euler(angle.x, angle.y, 0);
		
		//We can then multiply the offset by the rotation to orient the offset the same as the target.
		//We then subtract the result from the position of the target.
		transform.position = target.transform.position - (rotation * offset);
		
		//To keep looking at the player:
		//TODO don want player at center? LookAt doesn't work
		//transform.LookAt(target.transform);
		transform.rotation = Quaternion.Euler(offset_rotation.eulerAngles+rotation.eulerAngles);
		/*Debug.Log("rotation: " + rotation);
		Debug.Log("offset: " + offset_rotation);
		Debug.Log("conta: " + Quaternion.Euler(offset_rotation.eulerAngles+rotation.eulerAngles));
		Debug.Log("transform: " + transform.rotation);*/
	}
	
	public Vector3 getAngle() {
		return angle;
	}
}
