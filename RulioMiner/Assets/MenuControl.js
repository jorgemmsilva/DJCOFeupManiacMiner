#pragma strict

var quitButton=false;

function OnMouseDown() {
	if (quitButton) {
		Application.Quit();
	}
	else {
		Application.LoadLevel(1);
	}
}