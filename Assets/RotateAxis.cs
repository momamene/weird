using UnityEngine;
using System.Collections;

public class RotateAxis : MonoBehaviour {
	public Transform axisObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis("Mouse X");
		float deltaY = Input.GetAxis("Mouse Y");
		float scale = 10.0f, angle = scale * deltaY;
		transform.RotateAround (axisObject.position, axisObject.TransformDirection (Vector3.forward), angle);
	}
}
