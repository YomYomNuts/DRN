using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseManagerScript : MonoBehaviour {

	public List<GameObject> listOfSpawnPoints = new List<GameObject>();
	public int nbUnitToSpawn;
	[HideInInspector] public List<GameObject> listOfUnits = new List<GameObject>();
	private int unitCounter;
	private int currentSpawnPoint;
	[HideInInspector] public List<GameObject> listOfBases = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		
		unitCounter = 0;
		currentSpawnPoint = 0;
		
		GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
		
		foreach(GameObject baseInScene in bases)
		{
			if(baseInScene != this.gameObject)
			{
				listOfBases.Add(baseInScene);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if(unitCounter < nbUnitToSpawn)
		{
			int i=0;
			
			for(i = unitCounter; i < nbUnitToSpawn; ++i)
			{
				GameObject unit = (GameObject)Instantiate(Resources.Load("Prefabs/Unit"));
				unit.transform.position = listOfSpawnPoints[currentSpawnPoint++].transform.position;

				if(currentSpawnPoint >= listOfSpawnPoints.Count)
					currentSpawnPoint = 0;
				
				unit.GetComponent<MovementScript>().motherBase = this.gameObject;
				
				listOfUnits.Add(unit);
				//unit.GetComponent<MovementScript>().enemy = GetClosestEnemy(unit.transform.position);
			}
			unitCounter = i;
		}
	}
	
	
	public GameObject GetClosestEnemy(Vector3 position)
	{
		GameObject closest = null;
		float distance = Mathf.Infinity;
		float currentDistance = 0.0f;
		
		for(int i = 0; i < listOfBases.Count; ++i)
		{
		
			for(int j = 0; j < listOfBases[i].GetComponent<BaseManagerScript>().getNumberOfUnit(); ++j)
			{
				
				currentDistance = Vector3.Distance(position, listOfBases[i].GetComponent<BaseManagerScript>().listOfUnits[j].transform.position);

				if(currentDistance < distance)
				{
					distance = currentDistance;
					closest = listOfBases[i].GetComponent<BaseManagerScript>().listOfUnits[j];
				}
				
			}
		}
		
		return closest;
	}
	
	public int getNumberOfUnit()
	{
		//Debug.Log("fct:" + unitCounter);
		return unitCounter;
	}
	
	
}
