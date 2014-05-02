using UnityEngine;
using System.Collections;

public class Surface : MonoBehaviour {
    public Surface top, bottom, left, right;
    public bool isFolded(Surface surface)
    {
        if (this.top == surface || this.bottom == surface || this.left == surface || this.right == surface) {
            return gameObject.transform.parent == surface.transform.parent;
        }
        return false;
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	[ContextMenu ("Set to root of path finder ")]
	void SetToRootOfPathFinder()
	{
		PathFinder.Instance.root = this;
		PathFinder.Instance.UpdatePath ();
	}
}
