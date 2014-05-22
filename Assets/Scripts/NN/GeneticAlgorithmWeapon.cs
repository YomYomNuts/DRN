using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GeneticAlgorithmWeapon : MonoBehaviour
{
    #region Public Attributs
    public List<int> sizeNeuronalNetworkWeapon;
    public Vector3 toleranceGetNNWeapon;
    public int numberBestSelectedByReproductionWeapon;
    public float valueMutationWeapon;
    #endregion

    #region Private Attributs
    private Dictionary<Vector3, NeuronalNetwork> listBestNN;
    #endregion

    // Use this for initialization
	void Start ()
    {
        this.listBestNN = new Dictionary<Vector3, NeuronalNetwork>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public NeuronalNetwork GetNNWeapon(Vector3 scale)
    {
        if (this.listBestNN.Count > 0)
        {
            bool find = false;
            Vector3 minDistance = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
            KeyValuePair<Vector3, NeuronalNetwork> pairFind = new KeyValuePair<Vector3,NeuronalNetwork>();
            foreach (KeyValuePair<Vector3, NeuronalNetwork> pair in this.listBestNN)
            {
                Vector3 distance = scale - pair.Key;
                distance.x = Mathf.Abs(distance.x);
                distance.y = Mathf.Abs(distance.y);
                distance.z = Mathf.Abs(distance.z);
                if (distance.x <= minDistance.x && distance.y <= minDistance.y && distance.z <= minDistance.z &&
                    distance.x <= this.toleranceGetNNWeapon.x && distance.y <= this.toleranceGetNNWeapon.y && distance.z <= this.toleranceGetNNWeapon.z)
                {
                    find = true;
                    minDistance = distance;
                    pairFind = pair;
                }
            }

            if (find)
            {
                NeuronalNetwork[] listBestReproductor = this.listBestNN.Select((scoredindi2) => scoredindi2.Value).ToArray();
                NeuronalNetwork newNN = listBestReproductor[Random.Range(0, listBestReproductor.Length - 1)].Crossover(pairFind.Value);
                newNN.Mutation(valueMutationWeapon);
                return newNN;
            }
        }

        return new NeuronalNetwork(this.sizeNeuronalNetworkWeapon, true);
    }

    public void AddNNWeapon(Vector3 scale, NeuronalNetwork nn)
    {
        this.listBestNN.Add(scale, nn);
        this.listBestNN = (Dictionary<Vector3, NeuronalNetwork>)this.listBestNN
                                                                        .OrderBy((scoredindi) => scoredindi.Value.Score)
                                                                        .Take(this.numberBestSelectedByReproductionWeapon);
    }
}
