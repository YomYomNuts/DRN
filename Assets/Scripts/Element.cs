using UnityEngine;
using System.Collections;

public class Element
{
    #region Possible Values
    static readonly public int[] PossibleValues =
    {
        0,
        1
    };
    #endregion

    #region Private Attributs
    private int valueElement;
    #endregion

    #region Getter & Setter
    public int ValueElement
    {
        get { return this.valueElement; }
        set { this.valueElement = value; }
    }
    #endregion

    #region Constructor
    public Element()
    {
        this.valueElement = PossibleValues[UnityEngine.Random.Range(0, PossibleValues.Length)];
    }
    #endregion
}
