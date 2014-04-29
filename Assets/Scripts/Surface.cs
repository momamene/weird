using UnityEngine;
using System.Collections;

public class Surface : MonoBehaviour {
	public enum AdjacencyState {
		FOLDED = 0,
		UNFOLDED = 1
	}
	public class Adjacent {
		public Surface surface;
		public AdjacencyState state;
	}
	public Surface asdf;
	public Adjacent top = new Adjacent(), bottom = new Adjacent(), left = new Adjacent(), right = new Adjacent();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
