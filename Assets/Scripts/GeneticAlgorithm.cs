using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GeneticAlgorithm
{
    #region Attributes
    public int SizePopulation = 30;
    public int NumberElements = 10;
    public float PercentageSelectionReproduction = 0.4f;
    public float MutationRate = 0.4f;
    #endregion

    #region Private Attributes
    private int NumberIndividualSelectedByReproduction;
    private List<Individal> Population;
    private int NumberIterations;
    private Individal CurrentIndividual;
    private float CurrentError;
    #endregion

    void Start()
    {
        this.NumberIndividualSelectedByReproduction = (int)(this.SizePopulation * this.PercentageSelectionReproduction);

        this.CurrentIndividual = new Individal(this.NumberElements);
        this.CurrentError = this.CurrentIndividual.GetError();

        this.Population = new List<Individal>();
        for (int i = 0; i < this.SizePopulation; ++i)
        {
            this.Population.Add(new Individal(this.NumberElements));
        }

        this.NumberIterations = 0;
    }

    IEnumerator Update()
    {
        while (this.CurrentError != 0)
        {
            #region Evaluate all individual

            ScoredIndividual[] scoredIndividuals = new ScoredIndividual[this.SizePopulation];
            for (int i = 0; i < this.SizePopulation; ++i)
            {
                scoredIndividuals[i].Configuration = this.Population[i].CopyIndividal();
                scoredIndividuals[i].Score = scoredIndividuals[i].Configuration.GetError();
            }

            #endregion

            #region Select the reproductors
            Individal[] bestIndividuals = Selection(scoredIndividuals);
            #endregion

            #region Get the best one
            if (bestIndividuals.Length > 0)
            {
                this.CurrentIndividual = bestIndividuals[0].CopyIndividal();
                this.CurrentError = this.CurrentIndividual.GetError();
            }
            #endregion

            #region Croisement de la Individal
            Individal[] newPopulation = Crossover(bestIndividuals);
            #endregion

            #region Mutation de la Individal
            newPopulation = Mutation(newPopulation);
            #endregion

            this.Population = new List<Individal>(newPopulation);
            ++this.NumberIterations;

            yield return new WaitForSeconds(0.0001f);
        }
    }

    Individal[] Selection(ScoredIndividual[] scoredIndividuals)
    {
        return scoredIndividuals
            .OrderBy((scoredindi) => scoredindi.Score)
            .Take(this.NumberIndividualSelectedByReproduction)
            .Select((scoredindi2) => scoredindi2.Configuration)
            .ToArray();
    }

    Individal[] Crossover(Individal[] bestIndividuals)
    {
        Individal[] crossPopulation = new Individal[this.SizePopulation];

        for (int i = 0; i < this.SizePopulation; ++i)
        {
            Individal parent1 = bestIndividuals[Random.Range(0, bestIndividuals.Length)];
            Individal parent2 = bestIndividuals[Random.Range(0, bestIndividuals.Length)];

            crossPopulation[i] = parent1.Crossover(parent2); ;
        }

        return crossPopulation;
    }

    Individal[] Mutation(Individal[] newPopulation)
    {
        for (int i = 0; i < this.SizePopulation; i++)
        {
            if (Random.Range(0f, 1f) < this.MutationRate)
            {
                // Reverse two elements
                int pos1index = Random.Range(0, newPopulation[i].Elements.Length);
                int pos2index = Random.Range(0, newPopulation[i].Elements.Length);

                Element action1 = newPopulation[i].Elements[pos1index];
                Element action2 = newPopulation[i].Elements[pos2index];

                newPopulation[i].Elements[pos1index] = action2;
                newPopulation[i].Elements[pos2index] = action1;
            }
        }

        return newPopulation;
    }
}
