using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Surface))]
public class SurfaceEditor : Editor {
	Surface surface;
	void OnEnable()	{
		surface = target as Surface;
	}

	public void AdjacentLayout(string label, Surface.Adjacent adjacent)
    {
		EditorGUILayout.BeginVertical();
		adjacent.state = (Surface.AdjacencyState)EditorGUILayout.EnumPopup(
			label + " State",
			adjacent.state
			);
		adjacent.surface = EditorGUILayout.ObjectField(
			label + " Surface",
			adjacent.surface, 
			typeof(Surface),
			true
			) as Surface;
		EditorGUILayout.EndVertical();
	}

	public override void OnInspectorGUI() {
		string[] labels = {"Top", "Bottom", "Left", "Right"};
		Surface.Adjacent[] adjacents = {surface.top, surface.bottom, surface.left, surface.right};
        for (int i = 0; i < labels.Length; i++) {
			AdjacentLayout(labels[i], adjacents[i]);
		}
		if (GUI.changed)
			EditorUtility.SetDirty(surface);
	}
}
