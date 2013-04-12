using UnityEngine;
using System.Collections;

public class checkpoint_script : MonoBehaviour {
	public int signal = 0;
	void OnTriggerEnter(Collider other) {
		if (other.tag=="Player")
		{
			other.gameObject.GetComponent<avatar_script>().setCheckpoint(this.transform.position,signal);
			signal=-signal;
		}
    }
}
