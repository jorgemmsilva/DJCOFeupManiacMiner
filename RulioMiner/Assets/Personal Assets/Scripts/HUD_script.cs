using UnityEngine;
using System.Collections;

public class HUD_script : MonoBehaviour {

	public Texture2D Life;
	
	void OnGUI ()
	{
		GUI.Box (new Rect (0,0,200,100), Life);
	}
}
