using UnityEngine;
using UnityEditor;
using System.Collections;

public class VoxelGenerator : EditorWindow {
    [MenuItem("Voxel/Generator")]
    public static void ShowWindow()
    {
        VoxelGenerator window = (VoxelGenerator)EditorWindow.GetWindow(typeof(VoxelGenerator));
    }
    void InstantiateLayout(string label, Vector3 delta)
    {
        Object prefabRoot = PrefabUtility.GetPrefabParent(Selection.activeGameObject);
        if (GUILayout.Button(label)) {
            GameObject cloned = PrefabUtility.InstantiatePrefab(prefabRoot) as GameObject;
            cloned.transform.position = Selection.activeGameObject.transform.position + delta;
            cloned.transform.parent = Selection.activeGameObject.transform.parent;
            cloned.name = prefabRoot.name + cloned.transform.position;
            Selection.activeGameObject = cloned;

        }
    }
    void OnGUI()
    {
        if (Selection.activeGameObject) {
            string[] labels = { "x+1", "x-1", "y+1", "y-1", "z+1", "z-1" };
            Vector3[] deltas = { Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.forward, Vector3.back };
            for (int i = 0; i < labels.Length; i++) {
                InstantiateLayout(labels[i], deltas[i]);
            }
        }
    }
}
