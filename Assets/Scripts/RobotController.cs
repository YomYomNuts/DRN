using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour {
	/* Parameters */ 
	//Modules: 0 (left arm) 1 (right arm) 2 (bottom module)
	public List<GameObject> modules = new List<GameObject>();

	/* Privates Attributes */
	private List<Transform> sockets = new List<Transform>();

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			sockets.Add(child);
		}
		for (int i = 0; i < modules.Count; i++) {
			GameObject module 				= (GameObject) GameObject.Instantiate(modules[i]);
			if(i < sockets.Count)
				module.transform.parent 	= sockets[i];
			else module.transform.parent	= this.transform;
			module.transform.localPosition 	= new Vector3(module.transform.localScale.x/2.0f, 0, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
