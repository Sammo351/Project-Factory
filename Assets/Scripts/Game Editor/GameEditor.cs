using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEditor : MonoBehaviour {

    public Color placeColor = Color.green;
    public Color removeColor = Color.red;
    public Color highlightColor = Color.white;
    public Color conflictColor = Color.yellow;
    public Texture placeholder;
    public GameObject selectedBlock;
    public bool showHighlighter = true;
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
        else if (showHighlighter)
        {
            OnHover();
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
        if (coords != null)
        {
            Color col = button == 0 ? placeColor : removeColor;

            Drawer.DrawCube((Vector3)coords, col);
        }
    }
    void OnHover()
    {
        Vector3? coords = GetClickPosition(0);
        if (coords != null)
        {
            bool canPlace = GameEditorHelper.CanBlockBePlaced(selectedBlock, (Vector3)coords);
           //print(canPlace);
            Color col = canPlace ? highlightColor : conflictColor;
            
            Drawer.DrawBlock(selectedBlock,(Vector3)coords, col);
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
          
            Vector3 coords = hit.collider.transform.position;
            GameEditorHelper.RoundVector(ref coords);
            if (button==0)
            {
                coords += hit.normal; 
            }
            return coords;
        }
        return null;
    }
  
    
}
