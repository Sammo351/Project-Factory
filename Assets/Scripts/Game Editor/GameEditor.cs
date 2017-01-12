using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEditor : MonoBehaviour {

    public Color placeColor = Color.green;
    public Color removeColor = Color.red;
    public Color highlightColor = Color.white;
    public Texture placeholder;
    public bool drawInScene = true;
    private bool _leftMouseDown = false;
    private bool _rightMouseDown = false;
    
    void Start ()
    {
       
	}
	
	
	void Update ()
    {
        CheckInput();
	}
    void LateUpdate()
    {
       // Drawer.DrawCube(Vector3.one, drawInScene);
    } 
    void CheckInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
        else
            if (Input.GetMouseButtonUp(0))
        {
            OnClickRelease();
        }
        else if (Input.GetMouseButton(0))
        {
            OnClickHold();
        }
        else
            if (Input.GetMouseButtonDown(1))
        {
            OnClick(1);
        }
        else
            if (Input.GetMouseButtonUp(1))
        {
            OnClickRelease(1);
        }
        else if (Input.GetMouseButton(1))
        {
            OnClickHold(1);
        }
    }
    void OnClick(int button =0)
    {
        if(button==0)
        {
            _leftMouseDown = true;
        }
        else
        {
            _rightMouseDown = true;
        }
    }
    void OnClickRelease(int button = 0)
    {
        if (button == 0)
        {
            _leftMouseDown = false;
        }
        else
        {
            _rightMouseDown = false;
        }
    }
    void OnClickHold(int button = 0)
    {
        Vector3? coords = GetClickPosition(button);
        if(coords != null)
        {
            Color col = button == 0 ? placeColor : removeColor;
            Drawer.DrawCube((Vector3)coords, col);
        }
    }
    bool isMouseDown(int button)
    {
        return button ==0?_leftMouseDown:_rightMouseDown;
    }

    Vector3? GetClickPosition(int button = 0)
    {
        string[] layers = { "Editable" };
        int editableLayer = LayerMask.GetMask(layers);
        LayerMask layer = button == 0 ? -1 : editableLayer;
       
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer.value))
        {
          
            Vector3 coords = hit.point;
            print("before: " +coords);
            RoundVector(ref coords, hit.normal);
            print("after rounding: " + coords);
            if (button==0)
            {
                coords += hit.normal; 
            }
            print("final: " + coords);

            return coords;
        }
        return null;
    }
    void RoundVector(ref Vector3 vec, Vector3 normal)
    {
        int x = Round(vec.x, normal, 0);
        int y = Round(vec.y, normal, 1);
        int z = Round(vec.z, normal, 2);
        vec = new Vector3(x, y, z);
    }
    Vector3 RoundVector( Vector3 vec, Vector3 normal)
    {
        Vector3 v = new Vector3();
        RoundVector(ref v, normal);
        return v;
    }
    /*
    math rounding goes to the next even
    rounding alos depends on which axis the normal is on
    Correct position:
    z+, y-, x-
    Incorrect position(reduce by 1 when rounding):
    z-, y+, x+
    */
    int Round(float x, Vector3 normal, int axis) 
    {
        //x= -5.5
        int multi = x >= 0 ? 1 : -1;
        float absX = Mathf.Abs(x); // 5.5
        int integer = (int)(absX / 1); // 5
        float deci = absX - integer; // 5.5 - 5 = 0.5
        if(deci >=0.5f)
        {
            deci = 1;
            //if incorrect axis , deci=-1
            if((axis==0 && normal.x >0) || (axis == 1 && normal.y > 0) || (axis == 2 && normal.z < 0))
            {
                deci -= 1;
            }
        }
        int final = (int)(integer + deci);
        return final * multi;
    }
    
}
