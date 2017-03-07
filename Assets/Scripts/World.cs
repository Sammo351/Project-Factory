using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}
    public Block GetBlock(Vector3 vec)
    {
        Collider[] cols = Physics.OverlapSphere(vec, 0.2f, LayerMask.GetMask("Block"));
        if(cols!= null && cols.Length>0)
        {
            return cols[0].GetComponent<Block>();
        }
        return null;
    }
    public Block GetBlock(int x, int y, int z)
    {
        return GetBlock(new Vector3(x, y, z));
    }
   

}
