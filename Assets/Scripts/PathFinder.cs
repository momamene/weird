using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {
    public Surface root;
	private List<Surface> path = new List<Surface>();
	private static PathFinder _instance;  
	public static PathFinder Instance {
		get {  
			if (!_instance) {  
				_instance = GameObject.FindObjectOfType (typeof(PathFinder)) as PathFinder;  
				if (!_instance) {  
					GameObject container = new GameObject ();  
					container.name = "TravelSurface";  
					_instance = container.AddComponent (typeof(PathFinder)) as PathFinder;  
				}  
			}  
			return _instance;  
		}
	}
	public void UpdatePath()
	{
		if (root != null && path != null) {
			path = BFS();
		}
	}
    List<Surface> BFS()
    {
        Queue<Surface> queue = new Queue<Surface>();
        path = new List<Surface>();
        queue.Enqueue(root);
        while (queue.Count > 0) {
            Surface current = queue.Dequeue();
            path.Add(current);
            if (!current.isFolded(current.top) && !path.Contains(current.top)) {
                queue.Enqueue(current.top);
            }
            if (!current.isFolded(current.bottom) && !path.Contains(current.bottom)) {
                queue.Enqueue(current.bottom);
            }
            if (!current.isFolded(current.left) && !path.Contains(current.left)) {
                queue.Enqueue(current.left);
            }
            if (!current.isFolded(current.right) && !path.Contains(current.right)) {
                queue.Enqueue(current.right);
            }
        }
        return path;
    }
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Surface prev = null;
		foreach (Surface surface in path) {
			if (prev != null)
			{
				Gizmos.DrawSphere(prev.transform.position, 0.2f);
				Gizmos.DrawSphere(surface.transform.position, 0.2f);
			}
			prev = surface;
		}
	}
}
