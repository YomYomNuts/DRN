using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementGeneticWill
{
    #region Private Static Attributs
    private static int NUMBER_ELEMENTS = 4;
    #endregion

    #region Private Attributs
    private List<Vector3> valueElement;
    private float score;
    #endregion

    #region Getter & Setter
    public List<Vector3> ValueElement
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
    public ElementGeneticWill()
    {
        this.ValueElement = new List<Vector3>();
        for (int i = 0; i < ElementGeneticWill.NUMBER_ELEMENTS; ++i)
        {
            float scale = Random.Range(0f, 1.0f);
            this.ValueElement.Add(new Vector3(scale, scale, scale));
        }
        this.Score = 0;
    }
    #endregion

    #region Public Functions
    public float GetError()
    {
        return Const.MAX_SCORE - this.Score;
    }

    public ElementGeneticWill Crossover(ElementGeneticWill otherParent)
    {
        ElementGeneticWill child = new ElementGeneticWill();

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
            for (int i = 0; i < ElementGeneticWill.NUMBER_ELEMENTS; ++i)
            {
                float modif = Random.Range(-0.1f, 0.1f);
                this.ValueElement[i] += new Vector3(modif, modif, modif);
            }
        }
    }
    #endregion
}
