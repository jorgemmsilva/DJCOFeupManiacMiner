using UnityEngine;
using System.Collections;

public class kill_script : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag=="Player")
		{
			other.gameObject.GetComponent<avatar_script>().died();
		}
    }
}
