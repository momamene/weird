using UnityEngine;
using System.Collections;

public class Surface : MonoBehaviour {
    public Surface[] adjacent = new Surface[4];

	public Surface getAdjecent(Surface surface) {
		for (int i = 0; i < 4; i++) {
			if (this.adjacent[i] == surface) {
				return this.adjacent[i];
			}
		}
		return null;
	}
	public void setAdjecent(Surface surface, Surface target) {
		for (int i = 0; i < 4; i++) {
			if (this.adjacent[i] == surface) {
				this.adjacent[i] = target;
			}
		}
	}

	public void Concat(Surface that)
	{
		Surface adjacentA, adjacentB;
		for (int i = 0; i < 4; i++) {
			adjacentA = this.adjacent[i];
			adjacentB = that.adjacent[i];
			if (this.isInSameVoxel(adjacentA) && that.isInSameVoxel(adjacentB)) {
				adjacentA.setAdjecent(this, adjacentB);
				this.setAdjecent(adjacentA, null);
				adjacentB.setAdjecent(that, adjacentA);
				that.setAdjecent(adjacentB, null);
			}
		}
	}

    public bool isInSameVoxel(Surface surface)
    {
		if (surface == null) {
			return false;
		}
		return gameObject.transform.parent == surface.transform.parent;
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
