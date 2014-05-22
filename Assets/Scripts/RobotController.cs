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
			foreach(Transform subchild in child.transform){
				sockets.Add(subchild);
			}
		}
		for (int i = 0; i < modules.Count; i++) {
			GameObject module 				= (GameObject) GameObject.Instantiate(modules[i]);
			if(i < sockets.Count)
				module.transform.parent 	= sockets[i];
			else module.transform.parent	= this.transform;

			module.transform.localRotation = Quaternion.identity;
			module.transform.localPosition = Vector3.zero;
			module.transform.localScale = Vector3.one;

			if(i < 2) module.GetComponent<ArmModuleScript>().SetModule(new Vector3(2.0f, 2.0f, 2.0f), 1f);

			Vector3 rotation 				= Vector3.zero;
			Vector3 position 				= Vector3.zero;
			float middle 					= this.transform.GetChild(i).transform.localScale.x / 2.0f;

			if(i == 0) {
				position.x = middle;
			} else if (i == 1) {
				rotation.y = 180;
				position.x = -middle;
			}

//			if(i > 1 && i < 2){
//				rotation.z = 90;
//				position.y = -middle;
//			}else if(i > 0 && i < 2){
//
//			}else{
//				position.x = middle;
//			}

			if(i < 2) module.transform.Rotate(rotation);
			module.transform.localPosition 	= position;
		}
	}

	// Update is called once per frame
	void Update () {
		//update neurons
		//for each neuron, send message to matching module
		foreach (GameObject module in modules) {
			/*
			module.SendMessage("AmplitudePulse", 5.0);
			module.SendMessage("DirectionPulse", new Vector3(1.0f, 1.0f, 0.0f));
			*/
		}
	}
}
