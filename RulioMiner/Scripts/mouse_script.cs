using UnityEngine;
using System.Collections;

public class mouse_script : MonoBehaviour {
	public Rigidbody mycube;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
            mycube.AddForce(0,200F,0);
    }
    void OnMouseEnter() {
		mycube.renderer.material.color = Color.red;
		mycube.renderer.material.shader = Shader.Find("Diffuse");
    }
	void OnMouseExit() {
		mycube.renderer.material.color = Color.blue;
		mycube.renderer.material.shader = Shader.Find("Diffuse");
	}
	void OnMouseDrag() {
		mycube.AddExplosionForce(90.0F,new Vector3(0,-0.5f,0),90.0F);
	}
}
