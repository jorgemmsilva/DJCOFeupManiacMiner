using UnityEngine;
using System.Collections;

public class Powerup_pickup_script : MonoBehaviour {
	
	public enum MyEnumeratedType 
	{
  		NinjaRope, Platform, ChangeGravity
	}
 
	// in your script, declare a public variable of your enum type
	public MyEnumeratedType option;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag=="Player")
		{
			if (option == MyEnumeratedType.NinjaRope)
			{
				other.gameObject.GetComponent<ninjaRope_script>().addPower();
			}
			else if (option == MyEnumeratedType.Platform)
			{
				other.gameObject.GetComponent<addplatform_script>().addPower();
			}
			else
			{
				other.gameObject.GetComponent<movement_script>().changeGravity();
				//TODO script de mudar gravidade
			}
			Destroy(gameObject);
		}
    }
}
