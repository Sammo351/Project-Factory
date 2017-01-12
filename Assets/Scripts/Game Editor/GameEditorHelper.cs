using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEditorHelper
{

    public static int Round(float f)
    {
        int multi = f >= 0 ? 1 : -1;
        f = Mathf.Abs(f);
        f += 0.5F;
        return (int)Mathf.Floor(f) * multi;
    }
    public static void RoundVector(ref Vector3 vec)
    {

        int x = Round(vec.x);
        int y = Round(vec.y);
        int z = Round(vec.z);
        vec = new Vector3(x, y, z);
    }
    public static Vector3 RoundVector(Vector3 vec)
    {
        Vector3 v = new Vector3();
        RoundVector(ref v);
        return v;
    }
    public static Block GetBlock(GameObject block)
    {
        return block.GetComponent<Block>();
    }
    public static bool CanBlockBePlaced(GameObject block, Vector3 vec)
    {
        Vector3 origin = block.GetComponent<PreBlock>().GetOriginOffset() + vec;
        return block.GetComponent<PreBlock>().CanPlace(origin);
    }
}
  