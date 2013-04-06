using UnityEngine;
using System.Collections;

public class camera_script : MonoBehaviour {
	
	public GameObject target;
	Vector3 offset;
	Quaternion offset_rotation;
	Vector3 angle;
	private Vector3 targetAngle;
	private float DELTA_ANGLE=5.0f;
	
	void Awake () {
		offset =  transform.position - target.transform.position;
		offset_rotation = transform.rotation;
	}
	
	void Rot90() {
		targetAngle.y=(angle.y+90)%360;
	}
	void Rot270() {
		targetAngle.y=(angle.y-90)%360;
	}

	void Start () {
	}
	// Update is called once per frame
	void Update () {
	
		//angle.y += Input.GetAxis("Mouse X");
		if (targetAngle.y>angle.y) {
			if (angle.y+DELTA_ANGLE>targetAngle.y) {angle.y=targetAngle.y;}
			else {angle.y+=DELTA_ANGLE;}
		}
		else if (targetAngle.y<angle.y) {
			if (angle.y-DELTA_ANGLE<targetAngle.y) {angle.y=targetAngle.y;}
			else {angle.y-=DELTA_ANGLE;}
		}
		angle.y = angle.y % 360;
		
		//only allow rotation on y, this is to make 2.5 effect
		Quaternion rotation = Quaternion.Euler(0,angle.y,0);
		
		//this needs to be here or else when the character rotates so does the camera
		//this fixes camera ALWAYS at the starting position
		transform.position = target.transform.position + (rotation * offset);
		
		//To keep looking at the player:
		transform.rotation = Quaternion.Euler(offset_rotation.eulerAngles+rotation.eulerAngles);
		//Debug.Log("pos: "+target.transform.position);
		/*Debug.Log("rotation: " + rotation.eulerAngles);
		Debug.Log("offset: " + offset_rotation.eulerAngles);
		Debug.Log("conta: " + Quaternion.Euler(offset_rotation.eulerAngles+rotation.eulerAngles).eulerAngles);
		Debug.Log("transform: " + transform.rotation.eulerAngles);*/
	}
	
	public Vector3 getAngle() {
		return angle;
	}
}
