using UnityEngine;

public class ActivateTrigger : MonoBehaviour {
	public enum Mode {
		Trigger   = 0, // Just broadcast the action on to the target
		Replace   = 1, // replace target with source
		Activate  = 2, // Activate the target GameObject
		Enable    = 3, // Enable a component
		Animate   = 4, // Start animation on target
		Deactivate= 5, // Decativate target GameObject
		Fall= 6 // Activates gravity
	}

	/// The action to accomplish
	public Mode action = Mode.Activate;
	
	public float fallWaitTime=3.0f;
	public float fallRespawnTime=3.0f;

	/// The game object to affect. If none, the trigger work on this game object
	public Object target;
	public GameObject source;
	public int triggerCount = 1;///
	public bool repeatTrigger = false;
	
	void DoActivateTrigger () {
		triggerCount--;

		if (triggerCount == 0 || repeatTrigger) {
			Object currentTarget = target != null ? target : gameObject;
			Behaviour targetBehaviour = currentTarget as Behaviour;
			GameObject targetGameObject = currentTarget as GameObject;
			if (targetBehaviour != null)
				targetGameObject = targetBehaviour.gameObject;
		
			switch (action) {
				case Mode.Trigger:
					targetGameObject.BroadcastMessage ("DoActivateTrigger");
					break;
				case Mode.Replace:
					if (source != null) {
						Object.Instantiate (source, targetGameObject.transform.position, targetGameObject.transform.rotation);
						DestroyObject (targetGameObject);
					}
					break;
				case Mode.Activate:
					targetGameObject.active = true;
					break;
				case Mode.Enable:
					if (targetBehaviour != null)
						targetBehaviour.enabled = true;
					break;	
				case Mode.Animate:
					targetGameObject.animation.Play ();
					break;	
				case Mode.Deactivate:
					targetGameObject.active = false;
					break;
				case Mode.Fall:
					StartCoroutine(PlatformFall(targetGameObject));
					break;
			}
		}
	}
	
	System.Collections.IEnumerator PlatformDestroy(GameObject targetGameObject) {
		targetGameObject.renderer.material.color=Color.red;
		yield return new WaitForSeconds(fallWaitTime);
		targetGameObject.active=false;
	}
	
	System.Collections.IEnumerator PlatformRestore(GameObject targetGameObject) {
		yield return new WaitForSeconds(fallRespawnTime);
		targetGameObject.active=true;
		targetGameObject.renderer.material.color=Color.white;
	}
	
	System.Collections.IEnumerator PlatformFall(GameObject targetGameObject) {
		UnityEngine.Vector3 startPos = targetGameObject.rigidbody.position;
		targetGameObject.collider.rigidbody.useGravity = true;
		yield return new WaitForSeconds(fallWaitTime);
		targetGameObject.collider.rigidbody.useGravity = false;
		targetGameObject.rigidbody.position = startPos;
	}
	
	void OnTriggerEnter (Collider other) {
		DoActivateTrigger ();
	}
}