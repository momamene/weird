using UnityEngine;
using UnityEditor;
using System.Collections;

public class VoxelGenerator : EditorWindow {
    [MenuItem("Voxel/Generator")]
    public static void ShowWindow()
    {
        VoxelGenerator window = (VoxelGenerator)EditorWindow.GetWindow(typeof(VoxelGenerator));
    }
    void ConcatVoxel(GameObject selected, GameObject cloned, Vector3 delta)
    {
        Voxel selected_voxel = selected.GetComponent<Voxel>();
        Voxel cloned_voxel = cloned.GetComponent<Voxel>();
        if (delta == Vector3.back) {
			selected_voxel.back.Concat(cloned_voxel.forward);
        }
        else if (delta == Vector3.forward) {
			selected_voxel.forward.Concat(cloned_voxel.back);
        }
        else if (delta == Vector3.left) {
			selected_voxel.left.Concat(cloned_voxel.right);
        }
        else if (delta == Vector3.right) {
			selected_voxel.right.Concat(cloned_voxel.left);
        }
        else if (delta == Vector3.up) {
			selected_voxel.up.Concat(cloned_voxel.down);
        }
        else if (delta == Vector3.down) {
			selected_voxel.down.Concat(cloned_voxel.up);
        }
    }
    void InstantiateLayout(string label, Vector3 delta)
    {
        GameObject selected = Selection.activeGameObject;
        Object prefabRoot = PrefabUtility.GetPrefabParent(selected);
        if (GUILayout.Button(label)) {
            GameObject cloned = PrefabUtility.InstantiatePrefab(prefabRoot) as GameObject;
            PrefabUtility.DisconnectPrefabInstance(cloned);
            cloned.transform.position = selected.transform.position + delta;
            cloned.transform.parent = selected.transform.parent;
            cloned.name = prefabRoot.name + cloned.transform.position;
            Surface[] surfaces = cloned.GetComponentsInChildren<Surface>();
            foreach (Surface surface in surfaces) {
                surface.name = surface.name + cloned.transform.position;
            }
            Selection.activeGameObject = cloned;
            ConcatVoxel(selected, cloned, delta);
        }
    }
    void OnGUI()
    {
		if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<Voxel>()) {
            string[] labels = { "x+1", "x-1", "y+1", "y-1", "z+1", "z-1" };
            Vector3[] deltas = { Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.forward, Vector3.back };
            for (int i = 0; i < labels.Length; i++) {
                InstantiateLayout(labels[i], deltas[i]);
            }
			PathFinder.Instance.UpdatePath();
        }
    }
}
