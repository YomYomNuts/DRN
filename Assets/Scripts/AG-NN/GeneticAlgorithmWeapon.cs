using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GeneticAlgorithmWeapon : MonoBehaviour
{
    #region Public Attributs
    // Weapon
    public List<int> sizeNeuronalNetworkWeapon;
    public Vector3 toleranceGetNNWeapon;
    public int numberBestReproductionNNWeapon;
    public float valueMutationNNWeapon;
    public float valueMutationWeapon;

    // Will
    public int numberBestReproductionWill;
    public float valueMutationWill;
    #endregion

    #region Private Attributs
    private List<WeaponType> listBestWeapon;
    private List<WillType> listBestWill;
    #endregion

    // Use this for initialization
	void Start ()
    {
        this.listBestWeapon = new List<WeaponType>();
        this.listBestWill = new List<WillType>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public WeaponType GetNewWeapon()
    {
        WeaponType wt = new WeaponType();

        if (this.listBestWeapon.Count > 0)
        {
            wt.Scale = this.listBestWeapon[Random.Range(0, this.listBestWeapon.Count - 1)].Scale
                                    .Crossover(this.listBestWeapon[Random.Range(0, this.listBestWeapon.Count - 1)].Scale);
            wt.Scale.Mutation(valueMutationWeapon);

            bool find = false;
            Vector3 minDistance = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
            WeaponType wpFind = new WeaponType();
            foreach (WeaponType wp in this.listBestWeapon)
            {
                Vector3 distance = wt.Scale.ValueElement - wp.Scale.ValueElement;
                distance.x = Mathf.Abs(distance.x);
                distance.y = Mathf.Abs(distance.y);
                distance.z = Mathf.Abs(distance.z);
                if (distance.x <= minDistance.x && distance.y <= minDistance.y && distance.z <= minDistance.z &&
                    distance.x <= this.toleranceGetNNWeapon.x && distance.y <= this.toleranceGetNNWeapon.y && distance.z <= this.toleranceGetNNWeapon.z)
                {
                    find = true;
                    minDistance = distance;
                    wpFind = wp;
                }
            }

            if (find)
            {
                wt.Network = this.listBestWeapon[Random.Range(0, this.listBestWeapon.Count - 1)].Network.Crossover(wpFind.Network);
                wt.Network.Mutation(valueMutationNNWeapon);
            }
            else
            {
                wt.Network = new NeuronalNetwork(this.sizeNeuronalNetworkWeapon, true);
            }
        }
        else
        {
            wt.Network = new NeuronalNetwork(this.sizeNeuronalNetworkWeapon, true);
        }
 
        return wt;
    }

    public void ResultWeapon(WeaponType weaponType)
    {
        this.listBestWeapon.Add(weaponType);
        this.listBestWeapon = this.listBestWeapon
                                .OrderBy((scoredindi) => scoredindi.Score)
                                .Take(this.numberBestReproductionNNWeapon)
                                as List<WeaponType>;
    }

    public WillType GetNewWill()
    {
        WillType wt = new WillType();

        if (this.listBestWill.Count > 0)
        {
            wt.ScaleWills = this.listBestWill[Random.Range(0, this.listBestWill.Count - 1)].ScaleWills
                                    .Crossover(this.listBestWill[Random.Range(0, this.listBestWill.Count - 1)].ScaleWills);
            wt.ScaleWills.Mutation(valueMutationWill);
        }

        return wt;
    }

    public void ResultWill(WillType willType)
    {
        this.listBestWill.Add(willType);
        this.listBestWill = this.listBestWeapon
                                .OrderBy((scoredindi) => scoredindi.Score)
                                .Take(this.numberBestReproductionWill)
                                as List<WillType>;
    }

}
