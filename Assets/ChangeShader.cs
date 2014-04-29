using UnityEngine;
using System.Collections;

public class ChangeShader : MonoBehaviour {
	public Material replaced;
	private Material original;
	public GameObject target;
	// Use this for initialization
	void Start () {
		original = target.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(transform.rotation.z);
		if (Mathf.Abs(transform.rotation.z) > 0.37f) {
			target.renderer.material = replaced;
		} else { 
			target.renderer.material = original;
		}
	}
}
