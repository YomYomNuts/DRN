using UnityEngine;
using System.Collections;

public class ElementGeneticWeapon
{
    #region Private Static Attributs
    private static int MAX_SCORE = 999999999;
    #endregion

    #region Private Attributs
    private Vector3 valueElement;
    private float score;
    #endregion

    #region Getter & Setter
    public Vector3 ValueElement
    {
        get { return this.valueElement; }
        set { this.valueElement = value; }
    }
    public float Score
    {
        get { return this.score; }
        set { this.score = value; }
    }
    #endregion

    #region Constructor
    public ElementGeneticWeapon()
    {
        this.ValueElement = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        this.Score = 0;
    }
    #endregion

    #region Public Functions
    public float GetError()
    {
        return ElementGeneticWeapon.MAX_SCORE - this.Score;
    }

    public ElementGeneticWeapon Crossover(ElementGeneticWeapon otherParent)
    {
        ElementGeneticWeapon child = new ElementGeneticWeapon();

        if (Random.Range(0f, 1f) >= 0.5f)
        {
            child.ValueElement = this.ValueElement;
        }
        else
        {
            child.ValueElement = otherParent.ValueElement;
        }

        return child;
    }

    public void Mutation(float mutationRate)
    {
        if (Random.Range(0f, 1f) < mutationRate)
        {
            this.ValueElement += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f) , Random.Range(-0.1f, 0.1f));
        }
    }
    #endregion
}
