using UnityEngine;
using System.Collections;

public class Const
{
    #region Resources
    public static GameObject Robot = Resources.Load("Prefabs/Robotv3") as GameObject;
    #endregion

    #region Layers
    public static int LAYER_PLANE = 8;
    #endregion

    #region Values
    public static int MAX_SCORE = 99999999;
    #endregion
}
