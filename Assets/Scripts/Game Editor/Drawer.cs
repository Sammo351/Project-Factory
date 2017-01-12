﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Drawer
{
   
    static GameObject GetPlaceholderCube()
    {
        return (GameObject)Resources.Load("Cube");
        
    }
    static Texture GetTexture()
    {
        return  (Texture)Resources.Load("Textures/placeholder.png");
    }
    static Material GetMaterial()
    {
        return (Material)Resources.Load("Materials/placeholder");
    }
   
    public static void DrawCube(Vector3 vec, Color col, bool drawInSceneView = false)
    {
        GameObject cube = GetPlaceholderCube();
        Mesh mesh = cube.GetComponent<MeshFilter>().sharedMesh;
        Material material = cube.GetComponent<MeshRenderer>().sharedMaterial;
        material.color = col;
        Matrix4x4 matrix = Matrix4x4.TRS(vec, Quaternion.identity, Vector3.one*1.1f);
        MaterialPropertyBlock properties = new MaterialPropertyBlock();

        cube.GetComponent<Renderer>().GetPropertyBlock(properties);
        properties.SetColor("_Color", col);
        if (drawInSceneView)
        {
            Graphics.DrawMesh(mesh, matrix, material, 0); // works in scene view
        }
        else
        {
            Graphics.DrawMesh(mesh, matrix, material, 0, Camera.main, 0, properties, false); // removes shadow
        }
       
       
    }
    public static void DrawBlock(GameObject block, Vector3 vec, Color? col= null, bool drawInSceneView = false)
    {

        vec =block.GetComponent<PreBlock>().GetPositionFromOrigin(vec);
        Mesh mesh = block.GetComponent<MeshFilter>().sharedMesh;
        Material material = block.GetComponent<MeshRenderer>().sharedMaterial;
        Matrix4x4 matrix = Matrix4x4.TRS(vec, Quaternion.identity, block.transform.localScale);
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        block.GetComponent<Renderer>().GetPropertyBlock(properties);
       
        if(col != null)//draw indicator
        {
            
            GameObject cube = GetPlaceholderCube();
            Mesh cubeMesh = block.GetComponent<MeshFilter>().sharedMesh;
            Material cubeMaterial = block.GetComponent<MeshRenderer>().sharedMaterial;
            Graphics.DrawMesh(cubeMesh, matrix, cubeMaterial,0);
        }
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
