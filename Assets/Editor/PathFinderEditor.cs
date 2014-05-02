using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PathFinder))]
public class PathFinderEditor : Editor {

	// Use this for initialization
	void Start () {
	
	}
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical ();
		PathFinder.Instance.root = (Surface)EditorGUILayout.ObjectField ("Root", PathFinder.Instance.root, typeof(Surface), true);
		if (GUILayout.Button ("Update")) {
			PathFinder.Instance.UpdatePath();
		}
		EditorGUILayout.EndVertical ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
