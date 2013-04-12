using UnityEngine;
using System.Collections;

public class Powerup_pickup_script : MonoBehaviour {
	
	public enum MyEnumeratedType 
	{
  		NinjaRope, Platform, ChangeGravity
	}
 
	// in your script, declare a public variable of your enum type
	public MyEnumeratedType option;
	
	public int number_charges = 1;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag=="Player")
		{
			if (option == MyEnumeratedType.NinjaRope)
			{
				other.gameObject.GetComponent<ninjaRope_script>().addPower(number_charges);
			}
			else if (option == MyEnumeratedType.Platform)
			{
				other.gameObject.GetComponent<addplatform_script>().addPower(number_charges);
			}
			else
			{
				other.gameObject.GetComponent<movement_script>().changeGravity();
			}
			//gameObject.renderer.enabled=false;
			//gameObject.particleSystem.Stop();
			//Destroy(gameObject);
			
			
		}
    }
}
