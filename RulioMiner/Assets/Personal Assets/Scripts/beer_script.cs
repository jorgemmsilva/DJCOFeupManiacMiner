using UnityEngine;
using System.Collections;

public class beer_script : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag=="Player")
		{
			other.gameObject.GetComponent<avatar_script>().add_jola();
			Destroy(gameObject);
		}
    }
}
