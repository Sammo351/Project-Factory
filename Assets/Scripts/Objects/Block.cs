using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    
    public bool IsPartOfStructure()
    {
        Transform parent = transform.parent;
        if (parent != null && parent.GetComponent<Structure>() != null)
        {
            return true;
        }
        return false;
    }
    public Structure GetStructure()
    {
        Transform parent = transform.parent;
        if (parent != null)
        {
            return parent.GetComponent<Structure>();
        }
        return null;
    }
   

	
}
