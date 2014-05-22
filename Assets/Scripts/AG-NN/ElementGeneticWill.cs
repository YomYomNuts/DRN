﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementGeneticWill
{
    #region Private Static Attributs
    private static int NUMBER_ELEMENTS = 4;
    private static int MAX_SCORE = 999999999;
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
            this.ValueElement.Add(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        this.Score = 0;
    }
    #endregion

    #region Public Functions
    public float GetError()
    {
        return ElementGeneticWill.MAX_SCORE - this.Score;
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
                this.ValueElement[i] += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f) , Random.Range(-0.1f, 0.1f));
        }
    }
    #endregion
}
