using UnityEngine;
using UnityEditor;
using System.Collections;

public class VoxelGenerator : EditorWindow {
    [MenuItem("Voxel/Generator")]
    public static void ShowWindow()
    {
        VoxelGenerator window = (VoxelGenerator)EditorWindow.GetWindow(typeof(VoxelGenerator));
    }
    void ConcatBack(Voxel selected, Voxel cloned) {
        Surface attached_selected = selected.back, attached_cloned = cloned.forward;
        attached_selected.top.right = attached_cloned.top;
        attached_selected.bottom.right = attached_cloned.bottom;
        attached_selected.left.right = attached_cloned.left;
        attached_selected.right.right = attached_cloned.right;

        attached_cloned.top.left = attached_selected.top;
        attached_cloned.bottom.left = attached_selected.bottom;
        attached_cloned.left.left = attached_selected.left;
        attached_cloned.right.left = attached_selected.right;
    }
    void ConcatForward(Voxel selected, Voxel cloned)
    {
        ConcatBack(cloned, selected);
    }
    void ConcatLeft(Voxel selected, Voxel cloned)
    {
        Surface attached_selected = selected.left, attached_cloned = cloned.right;

        attached_selected.top.bottom = attached_cloned.top;
        attached_selected.bottom.top = attached_cloned.bottom;
        attached_selected.left.left = attached_cloned.left;
        attached_selected.right.left = attached_cloned.right;

        attached_cloned.top.bottom = attached_selected.top;
        attached_cloned.bottom.top = attached_selected.bottom;
        attached_cloned.left.right = attached_selected.left;
        attached_cloned.right.right = attached_selected.right;
    }
    void ConcatRight(Voxel selected, Voxel cloned)
    {
		ConcatLeft(cloned, selected);
    }
    void ConcatUp(Voxel selected, Voxel cloned)
    {
        Surface attached_selected = selected.up, attached_cloned = cloned.down;

        attached_selected.top.top = attached_cloned.top;
        attached_selected.bottom.top = attached_cloned.bottom;
        attached_selected.left.top = attached_cloned.left;
        attached_selected.right.top = attached_cloned.right;

        attached_cloned.top.bottom = attached_selected.top;
        attached_cloned.bottom.bottom = attached_selected.bottom;
        attached_cloned.left.bottom = attached_selected.left;
        attached_cloned.right.bottom = attached_selected.right;    
    }
    void ConcatDown(Voxel selected, Voxel cloned)
    {
		ConcatUp(cloned, selected);
    }
    void ConcatVoxel(GameObject selected, GameObject cloned, Vector3 delta)
    {
        Voxel selected_voxel = selected.GetComponent<Voxel>();
        Voxel cloned_voxel = cloned.GetComponent<Voxel>();
        if (delta == Vector3.back) {
            ConcatBack(selected_voxel, cloned_voxel);
        }
        else if (delta == Vector3.forward) {
            ConcatForward(selected_voxel, cloned_voxel);
        }
        else if (delta == Vector3.left) {
            ConcatLeft(selected_voxel, cloned_voxel);
        }
        else if (delta == Vector3.right) {
            ConcatRight(selected_voxel, cloned_voxel);
        }
        else if (delta == Vector3.up) {
            ConcatUp(selected_voxel, cloned_voxel);
        }
        else if (delta == Vector3.down) {
            ConcatDown(selected_voxel, cloned_voxel);
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
