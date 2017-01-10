using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : MonoBehaviour {

    public Vector2 size = Vector2.one * 10;
    public float height = 0.5f;
	void Start ()
    {

        GameObject block = (GameObject)Resources.Load("Floor Block");
        BuildFloor(block);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void BuildFloor(GameObject block)
    {
        GameObject parent = new GameObject();
        parent.name = "Floor";
        int x = Mathf.RoundToInt(size.x / 2);
        int y = Mathf.RoundToInt(size.y / 2);
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector3 spawnPos = new Vector3(-x + i, height, -y + j);
                GameObject g = GameObject.Instantiate(block, spawnPos, Quaternion.identity);
                g.transform.parent = parent.transform;
            }
        }
    }
}
