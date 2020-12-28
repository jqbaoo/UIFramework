using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class RTS_NewSyntaxe : MonoBehaviour {

    private float speed=30f;
	
	void Update () {
	
		Gesture current = EasyTouch.current;

	

		// Swipe
		if (current.type == EasyTouch.EvtType.On_Swipe && current.touchCount == 1){
			transform.Translate( Vector3.left * current.deltaPosition.x / Screen.width* speed);
			transform.Translate( Vector3.back * current.deltaPosition.y / Screen.height * speed);
		}

		//// Pinch
		//if (current.type == EasyTouch.EvtType.On_Pinch ){
		//	Camera.main.fieldOfView += current.deltaPinch * 10 * Time.deltaTime;
		//}

		//// Twist
		//if (current.type == EasyTouch.EvtType.On_Twist ){
		//	transform.Rotate( Vector3.up * current.twistAngle);
		//}
	}

	
}
