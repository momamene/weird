using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TravelSurface : MonoBehaviour {
    public Surface surface;
    void Start()
    {
        List<Surface> path = BFS();
        foreach (Surface surface in path) {
            surface.renderer.material.color = Color.red;
        }
    }
    List<Surface> BFS()
    {
        Surface root = surface;
        Queue<Surface> queue = new Queue<Surface>();
        List<Surface> path = new List<Surface>();
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
}
