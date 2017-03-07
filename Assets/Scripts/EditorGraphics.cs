using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGraphics : MonoBehaviour {

    public GameObject o;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public static void DrawMachine(GameObject o,Vector3 pos)
    {
        if (o.GetComponent<Structure>())
        {
            foreach (Block block in o.transform.GetComponentsInChildren<Block>())
            {
                Vector3 diff = o.transform.position - block.transform.position;
                DrawCube(block.gameObject, new Vector3(0, 1, 0) - diff, Color.white);
            }
        }
        else if (o.GetComponent<Block>())
        {
            DrawCube(o.gameObject, new Vector3(0, 1, 0), Color.white);
        }
    }
    
    public static void DrawCube(GameObject cube,Vector3 vec, Color col, bool drawInSceneView = false)
    {
     
        Mesh mesh = cube.GetComponent<MeshFilter>().sharedMesh;
        Material material = cube.GetComponent<Renderer>().sharedMaterial;
        Matrix4x4 matrix = Matrix4x4.TRS(vec, Quaternion.identity, Vector3.one*1.1f);
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        cube.GetComponent<Renderer>().GetPropertyBlock(properties);
        properties.SetColor("_Color", material.color);
        if (drawInSceneView)
        {
            Graphics.DrawMesh(mesh, matrix, material, 0); // works in scene view
        }
        else
        {
            Graphics.DrawMesh(mesh, matrix, material, 0, Camera.main, 0, properties, false); // removes shadow
        }
       
       
    }
}
