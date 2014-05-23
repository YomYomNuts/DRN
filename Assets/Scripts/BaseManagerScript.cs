using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseManagerScript : MonoBehaviour {

	public List<GameObject> listOfSpawnPoints = new List<GameObject>();
	public int nbUnitToSpawn;
	[HideInInspector] public List<GameObject> listOfUnits = new List<GameObject>();
	private int currentSpawnPoint;
	[HideInInspector] public List<GameObject> listOfBases = new List<GameObject>();
	public float timeToWaitBetweenSpawns;

    private GeneticAlgorithm geneticAlgorithm;
	public int unitsRemoved = 0;
	public int unitsKilled = 0;
	
	// Use this for initialization
	void Start () {
        geneticAlgorithm = this.GetComponent<GeneticAlgorithm>();

		currentSpawnPoint = 0;
		
		GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
		
		foreach(GameObject baseInScene in bases)
		{
			if(baseInScene != this.gameObject)
			{
				listOfBases.Add(baseInScene);
			}
		}
		
		StartCoroutine(CreateUnit());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator CreateUnit()
	{
		while(true)
		{
            if (this.listOfUnits.Count < nbUnitToSpawn)
			{
                GameObject unit = (GameObject)Instantiate(Const.Robot);
				unit.transform.position = listOfSpawnPoints[currentSpawnPoint++].transform.position;
				
				if(currentSpawnPoint >= listOfSpawnPoints.Count)
					currentSpawnPoint = 0;

                unit.GetComponent<MovementScript>().motherBase = this.gameObject;
                unit.GetComponentInChildren<RobotController>().SetBaseManager(this);
				
				Material baseMaterial = this.GetComponent<Renderer>().material;
                for (int i = 0; i < unit.transform.childCount; ++i)
                {
                    Transform tchild = unit.transform.GetChild(i);
                    if (tchild.name == "Body")
                    {
                        tchild.renderer.material = baseMaterial;
                        break;
                    }
                }
				
				listOfUnits.Add(unit);
				
				yield return new WaitForSeconds(timeToWaitBetweenSpawns);
			}
			yield return new WaitForSeconds(0);
		}
	}
	
	
	public GameObject GetClosestEnemy(Vector3 position)
	{
		GameObject closest = null;
		float distance = Mathf.Infinity;
		float currentDistance = 0.0f;
		
		for(int i = 0; i < listOfBases.Count; ++i)
		{
            BaseManagerScript baseManager = listOfBases[i].GetComponent<BaseManagerScript>();
            for (int j = 0; j < baseManager.getNumberOfUnit(); ++j)
			{
                currentDistance = Vector3.Distance(position, baseManager.listOfUnits[j].transform.position);

				if(currentDistance < distance)
				{
					distance = currentDistance;
                    closest = baseManager.listOfUnits[j];
				}
			}
		}
		
		return closest;
	}
	
	public List<GameObject> GetAlliedAround(GameObject unit, float distanceAround)
	{	
		List<GameObject> unitsAround = new List<GameObject>();
		float distance = 0.0f;
		
		for(int i = 0; i < listOfUnits.Count; ++i)
		{
			if(listOfUnits[i].gameObject != unit)
			{
				distance = Vector3.Distance(unit.transform.position, listOfUnits[i].transform.position);
				
				if(distance < distanceAround)
				{
					unitsAround.Add (listOfUnits[i]);
					return unitsAround;
				}
			}
		}
		
		return unitsAround;
	}
	
	public int getNumberOfUnit()
	{
        return this.listOfUnits.Count;
	}

    public GeneticAlgorithm GetGeneticAlgorithm()
    {
        return this.geneticAlgorithm;
    }

    public void RemoveUnit(GameObject unit)
    {
        this.listOfUnits.Remove(unit);
		//this.GetComponentsInChildren<TextMesh> () [1].text = "Lost: " + this.unitsRemoved;
		this.unitsRemoved++;
    }
}
