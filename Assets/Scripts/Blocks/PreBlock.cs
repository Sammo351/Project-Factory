using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreBlock : MonoBehaviour
{
    List<Vector3> checkPositions = new List<Vector3>();
    public bool requiresSolidBase = true;
    public virtual bool CanPlace(Vector3 vec) //requires origin, doesnt work with rotations
    {
        if (requiresSolidBase)
        {
            bool solid = CheckForSolidBase(vec);
            if (!solid) {return false; } //else check for collisions
        }
       
        Vector3 scale = transform.localScale;
        Collider[] cols;
            for (int i = 0; i < scale.x; i++)
            {
                for (int j = 0; j < scale.y; j++)
                {
                    for (int k = 0; k < scale.z; k++)
                    {

                        cols = Physics.OverlapSphere(vec + new Vector3(i, j,k), 0.45f); //same layer

                        if (cols.Length > 0) // if collider
                        {
                            return false;
                        }

                    }

                }
            }
               
        return true;
    }
    bool CheckForSolidBase(Vector3 vec)
    {
        Vector3 scale = transform.localScale;
        Collider[] cols;
       
        for (int i = 0; i < scale.x; i++)//3, x, x+1, x+2  = 3
        {
            for (int j = 0; j < scale.z; j++)//2
            {
                Vector3 checkPos = vec + new Vector3(i, -1, j);
                cols = Physics.OverlapSphere(checkPos, 0.45f); //layer below
                if(cols.Length<=0)//no floor block
                {
                    return false;
                }
            }
        }
        return true;
    }

    public Vector3 GetOrigin()
    {
        Vector3 scale = transform.localScale / 2f;
        Vector3 centre = transform.position;
        centre -= scale;
        return centre + new Vector3(0.5f, 0.5f, 0.5f);
    }
    public Vector3 GetOriginOffset()
    {
        Vector3 centre = Vector3.zero;
        Vector3 scale = transform.localScale / 2f;
        centre -= new Vector3(0.5f, 0.5f, 0.5f);
        centre += scale;
        return centre;
    }
    public Vector3 GetPositionFromOrigin(Vector3 origin)
    {
        Vector3 centre = origin;
        Vector3 scale = transform.localScale / 2f;
        centre -= new Vector3(0.5f, 0.5f, 0.5f);
        centre += scale;
        return centre;
    }
    public void OnDrawGizmos()
    {
        print("gizmos called");
        Gizmos.color = Color.yellow;
        foreach (Vector3 vec in checkPositions)
        {
            print("drawing");
            Gizmos.DrawCube(vec, Vector3.one);
        }
    }
}
