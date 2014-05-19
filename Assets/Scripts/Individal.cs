using UnityEngine;
using System.Collections;

public class Individal
{
    #region Private Attributs
    private Element[] elements;
    private float score;
    #endregion

    #region Getter & Setter
    public Element[] Elements
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
    public Individal(int numElements)
    {
        this.elements = new Element[numElements];

        for (int i = 0; i < numElements; ++i)
            this.elements[i] = new Element();
    }
    #endregion

    #region Functions
    public Individal CopyIndividal()
    {
        Individal newIndividal = new Individal(this.Elements.Length);

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

    public Individal Crossover(Individal otherParent)
    {
        Individal child = new Individal(this.elements.Length);

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
