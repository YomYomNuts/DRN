using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public float output;
    public List<float> weight = new List<float>();
}

public class NeuronalNetwork
{
    #region Private Attributs
    private List<int> sizeNeuronalNetwork;
    private List<List<Node>> neuronalNetwork;
    private float score;
    #endregion

    #region Getter & Setter
    public List<List<Node>> Elements
    {
        get { return this.neuronalNetwork; }
        set { this.neuronalNetwork = value; }
    }
    public float Score
    {
        get { return this.score; }
        set { this.score = value; }
    }
    #endregion

    #region Constructor
    public NeuronalNetwork()
    {
        this.sizeNeuronalNetwork = new List<int>();
        this.neuronalNetwork = new List<List<Node>>();
        this.score = 0;
    }

    public NeuronalNetwork(List<int> sizeNeuronalNetwork, bool generateWeights)
    {
        this.sizeNeuronalNetwork = sizeNeuronalNetwork;
        this.neuronalNetwork = new List<List<Node>>();
        this.score = 0;
        for (int i = 0; i < this.sizeNeuronalNetwork.Count; i++)
	    {
		    List<Node> layer = new List<Node>();
            int size = this.sizeNeuronalNetwork[i];
		    int sizePrev = 0;
		    if (i > 0)
                sizePrev = this.sizeNeuronalNetwork[i - 1];

		    for (int j = 0; j < size; ++j)
		    {
			    Node node = new Node();
			    node.output = 1;
			    for (int m = 0; m < sizePrev; ++m)
			    {
				    node.weight.Add(0);
			    }
			    layer.Add(node);
		    }
            this.neuronalNetwork.Add(layer);
	    }
        if (generateWeights)
            InitializeWeight();
    }
    #endregion

    #region Private Functions
    void InitializeWeight()
    {
        for (int i = 1; i < this.neuronalNetwork.Count; i++)
	    {
            List<Node> layer = this.neuronalNetwork[i];
            int sizePrev = this.neuronalNetwork[i - 1].Count;

		    for (int j = 0; j < layer.Count; ++j)
		    {
			    Node node = layer[j];
			    for (int m = 0; m < sizePrev; ++m)
			    {
				    node.weight[m] = Random.Range(-1.0f, 1.0f);
			    }
		    }
	    }
    }

    float FunctionSigmoid(float wx)
    {
        return 1.0f / (1.0f + Mathf.Exp(-wx));
    }
    #endregion

    #region Public Functions
    public List<float> Evaluate(List<float> inputs)
    {
        List<Node> layerInput = this.neuronalNetwork[0];

		/* Propagate the inputs */
		for (int k = 0; k < layerInput.Count; ++k)
		{
            layerInput[k].output = inputs[k];
		}
        for (int j = 1; j < this.neuronalNetwork.Count; ++j)
		{
            List<Node> layer = this.neuronalNetwork[j];
            List<Node> prevLayer = this.neuronalNetwork[j - 1];
			for(int k = 0; k < layer.Count; ++k)
			{
				Node node = layer[k];
				float sum = 0;
				for (int l = 0; l < prevLayer.Count; ++l)
					sum += node.weight[l] * prevLayer[l].output;
				node.output = FunctionSigmoid(sum);
			}
		}

        List<Node> layerOutput = this.neuronalNetwork[this.neuronalNetwork.Count - 1];
        List<float> ouputs = new List<float>();
	    for (int i = 0; i < layerOutput.Count; i++)
	    {
            ouputs.Add(layerOutput[i].output);
	    }
        return ouputs;
    }

    public float GetError()
    {
        return Const.MAX_SCORE - this.Score;
    }

    public NeuronalNetwork Crossover(NeuronalNetwork otherParent)
    {
        NeuronalNetwork child = new NeuronalNetwork(this.sizeNeuronalNetwork, false);

        for (int i = 0; i < this.Elements.Count; i++)
        {
            for (int j = 0; j < this.Elements[i].Count; ++j)
            {
                if (Random.Range(0f, 1f) >= 0.5f)
                {
                    float[] tempWeight = new float[this.Elements[i][j].weight.Count];
                    this.Elements[i][j].weight.CopyTo(tempWeight);
                    child.Elements[i][j].weight = new List<float>(tempWeight);
                }
                else
                {
                    float[] tempWeight = new float[this.Elements[i][j].weight.Count];
                    otherParent.Elements[i][j].weight.CopyTo(tempWeight);
                    child.Elements[i][j].weight = new List<float>(tempWeight);
                }
            }
        }

        return child;
    }

    public void Mutation(float mutationRate)
    {
        for (int i = 0; i < this.Elements.Count; i++)
        {
            for (int j = 0; j < this.Elements[i].Count - 1; ++j)
            {
                if (Random.Range(0f, 1f) < mutationRate)
                {
                    int size = Random.Range(0, this.Elements[i][j].weight.Count - 1);
                    this.Elements[i][j].weight[size] += Random.Range(-0.1f, 0.1f);
                }
            }
        }
    }
    #endregion
}
