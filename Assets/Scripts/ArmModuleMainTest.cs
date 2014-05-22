using UnityEngine;
using System.Collections;

public class ArmModuleMainTest : MonoBehaviour {

	public Transform prefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (prefab);
		}
	}
}
