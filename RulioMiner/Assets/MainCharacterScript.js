#pragma strict
var checkpointX=0;
var checkpointY=0;
var checkpointZ=0;

function setCheckpoint() {
	checkpointX=this.transform.position.x;
	checkpointY=this.transform.position.y;
	checkpointZ=this.transform.position.z;
}

function die() {
	this.transform.position.x=Mathf.Ceil(checkpointX);
	this.transform.position.y=Mathf.Ceil(checkpointY+2);
	this.transform.position.z=Mathf.Ceil(checkpointZ);
}

function OnControllerColliderHit(collision:ControllerColliderHit) {
	if (collision.gameObject.name.Contains("Trap")) {
		die();
	}
	else if (collision.gameObject.name.Contains("Checkpoint")) {
		setCheckpoint();
	}
}