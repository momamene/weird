using UnityEngine;
using System.Collections;

public class Voxel : MonoBehaviour {
    public Surface up, down, left, right, forward, back;

    public Surface getOriginalTop(Surface surface)
    {
        if (surface == up || surface == down) {
            return right;
        }
        else if (surface == left || surface == right || surface == forward || surface == back) {
            return up;
        }
        return null;
    }
    public Surface getOriginalBottom(Surface surface)
    {
        if (surface == up || surface == down) {
            return left;
        }
        else if (surface == left || surface == right || surface == forward || surface == back) {
            return down;
        }
        return null;
    }
    public Surface getOriginalLeft(Surface surface)
    {
        if (surface == back || surface == forward) {
            return left;
        }
        else if (surface == up || surface == down || surface == left || surface == right) {
            return forward;
        }
        return null;
    }
    public Surface getOriginalRight(Surface surface)
    {
        if (surface == back || surface == forward) {
            return right;
        }
        else if (surface == up || surface == down || surface == left || surface == right) {
            return back;
        }
        return null;
    }
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
