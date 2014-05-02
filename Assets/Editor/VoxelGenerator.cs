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
        selected.getOriginalTop(attached_selected).right = cloned.getOriginalTop(attached_cloned);
        selected.getOriginalBottom(attached_selected).right = cloned.getOriginalBottom(attached_cloned);
        selected.getOriginalLeft(attached_selected).right = cloned.getOriginalLeft(attached_cloned);
        selected.getOriginalRight(attached_selected).right = cloned.getOriginalRight(attached_cloned);

        cloned.getOriginalTop(attached_cloned).left = selected.getOriginalTop(attached_selected);
        cloned.getOriginalBottom(attached_cloned).left = selected.getOriginalBottom(attached_selected);
        cloned.getOriginalLeft(attached_cloned).left = selected.getOriginalLeft(attached_selected);
        cloned.getOriginalRight(attached_cloned).left = selected.getOriginalRight(attached_selected);
    }
    void ConcatForward(Voxel selected, Voxel cloned)
    {
        ConcatBack(cloned, selected);
    }
    void ConcatLeft(Voxel selected, Voxel cloned)
    {
        Surface attached_selected = selected.left, attached_cloned = cloned.right;

        selected.getOriginalTop(attached_selected).bottom = cloned.getOriginalTop(attached_cloned);
        selected.getOriginalBottom(attached_selected).top = cloned.getOriginalBottom(attached_cloned);
        selected.getOriginalLeft(attached_selected).left = cloned.getOriginalLeft(attached_cloned);
        selected.getOriginalRight(attached_selected).left = cloned.getOriginalRight(attached_cloned);

        cloned.getOriginalTop(attached_cloned).bottom = selected.getOriginalTop(attached_selected);
        cloned.getOriginalBottom(attached_cloned).top = selected.getOriginalBottom(attached_selected);
        cloned.getOriginalLeft(attached_cloned).right = selected.getOriginalLeft(attached_selected);
        cloned.getOriginalRight(attached_cloned).right = selected.getOriginalRight(attached_selected);
    }
    void ConcatRight(Voxel selected, Voxel cloned)
    {
		ConcatLeft(cloned, selected);
    }
    void ConcatUp(Voxel selected, Voxel cloned)
    {
        Surface attached_selected = selected.up, attached_cloned = cloned.down;

        selected.getOriginalTop(attached_selected).top = cloned.getOriginalTop(attached_cloned);
        selected.getOriginalBottom(attached_selected).top = cloned.getOriginalBottom(attached_cloned);
        selected.getOriginalLeft(attached_selected).top = cloned.getOriginalLeft(attached_cloned);
        selected.getOriginalRight(attached_selected).top = cloned.getOriginalRight(attached_cloned);

        cloned.getOriginalTop(attached_cloned).bottom = selected.getOriginalTop(attached_selected);
        cloned.getOriginalBottom(attached_cloned).bottom = selected.getOriginalBottom(attached_selected);
        cloned.getOriginalLeft(attached_cloned).bottom = selected.getOriginalLeft(attached_selected);
        cloned.getOriginalRight(attached_cloned).bottom = selected.getOriginalRight(attached_selected);    
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
