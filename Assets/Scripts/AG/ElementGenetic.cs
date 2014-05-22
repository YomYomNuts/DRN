using UnityEngine;
using System.Collections;

public class ElementGenetic
{
    #region Private Attributs
    private float valueElement;
    #endregion

    #region Getter & Setter
    public float ValueElement
    {
        get { return this.valueElement; }
        set { this.valueElement = value; }
    }
    #endregion

    #region Constructor
    public ElementGenetic()
    {
        this.valueElement = RandomFloat(-1.0f, 1.0f);
    }
    #endregion

    #region Private Function
    float RandomFloat(float Low, float High)
    {
        return Random.Range(Low, High);
    }
    #endregion
}
