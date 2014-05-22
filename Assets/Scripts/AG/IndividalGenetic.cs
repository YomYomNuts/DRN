using UnityEngine;
using System.Collections;

public class IndividalGenetic
{
    #region Private Attributs
    private ElementGenetic[] elements;
    private float score;
    #endregion

    #region Getter & Setter
    public ElementGenetic[] Elements
    {
        get { return this.elements; }
        set { this.elements = value; }
    }
    public float Score
    {
        get { return this.score; }
        set { this.score = value; }
    }
    #endregion

    #region Constructor
    public IndividalGenetic(int numElements)
    {
        this.elements = new ElementGenetic[numElements];

        for (int i = 0; i < numElements; ++i)
            this.elements[i] = new ElementGenetic();
    }
    #endregion

    #region Functions
    public IndividalGenetic CopyIndividal()
    {
        IndividalGenetic newIndividal = new IndividalGenetic(this.Elements.Length);

        for (int i = 0; i < this.Elements.Length; ++i)
        {
            newIndividal.Elements[i] = this.Elements[i];
        }

        return newIndividal;
    }
    
    public float GetError()
    {
        int numberZero = 0;

        for (int i = 0; i < this.elements.Length; ++i)
        {
            if (this.elements[i].ValueElement == 0)
                ++numberZero;
        }

        return numberZero;
    }

    public IndividalGenetic Crossover(IndividalGenetic otherParent)
    {
        IndividalGenetic child = new IndividalGenetic(this.elements.Length);

        for (int i = 0; i < child.Elements.Length; i++)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                child.Elements[i] = this.Elements[i];
            }
            else
            {
                child.Elements[i] = otherParent.Elements[i];
            }
        }

        return child;
    }
    #endregion
}
