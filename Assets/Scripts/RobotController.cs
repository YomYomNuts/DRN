using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour {
	/* Parameters */ 
	//Modules: 0 (left arm) 1 (right arm) 2 (bottom module)
	public List<GameObject> modules = new List<GameObject>();

    public BaseManagerScript baseManager;

	/* Privates Attributes */
	private List<Transform> sockets = new List<Transform>();

	private int numerOfArm = 1;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform.parent)
        {
			foreach(Transform subchild in child.transform)
            {
				sockets.Add(subchild);
			}
		}

		for (int i = 0; i < modules.Count; i++)
        {
			GameObject module 				= (GameObject) GameObject.Instantiate(modules[i]);
            float middle = 0f;
            if (i < sockets.Count)
            {
                module.transform.parent = sockets[i];
                middle = sockets[i].localScale.x / 2.0f;
            }
            else
            {
                module.transform.parent = this.transform.parent;
                middle = this.transform.parent.localScale.x / 2.0f;
            }

			module.transform.localRotation = Quaternion.identity;
			module.transform.localPosition = Vector3.zero;
            module.transform.localScale = Vector3.one;

            if (i < this.numerOfArm)
                module.GetComponent<ArmModuleScript>().SetModule(this.baseManager.GetGeneticAlgorithm().GetNewWeapon());
            else
                module.GetComponent<MoveModuleScript>().SetModule(this.baseManager.GetGeneticAlgorithm().GetNewWill());

			Vector3 rotation 				= Vector3.zero;
			Vector3 position 				= Vector3.zero;

			if(this.numerOfArm == 2) {
				if(i == 0) {
					position.x = middle;
				} else if (i == 1) {
					rotation.y = 180;
					position.x = -middle;
				}
			} else {
				if(i == 0) {
					rotation.y = -90;
					position.z = middle;
				}
			}


//			if(i > 1 && i < 2){
//				rotation.z = 90;
//				position.y = -middle;
//			}else if(i > 0 && i < 2){
//
//			}else{
//				position.x = middle;
//			}

			if(i < this.numerOfArm) module.transform.parent.Rotate(rotation);
            //module.transform.parent.localPosition = position;
		}
	}

	// Update is called once per frame
	void Update () {
		//update neurons
		//for each neuron, send message to matching module
		//foreach (GameObject module in modules) {
			/*
			module.SendMessage("AmplitudePulse", 5.0);
			module.SendMessage("DirectionPulse", new Vector3(1.0f, 1.0f, 0.0f));
			*/
		//}

		if (this.transform.position.y < -3) {
			this.baseManager.RemoveUnit(this.transform.parent.gameObject);
			Destroy(this.transform.parent.gameObject);
		}
	}

    public void SetBaseManager(BaseManagerScript baseManager)
    {
        this.baseManager = baseManager;
    }
	
	/*
	private GameObject lastHitObject = null;
    void OnCollisionEnter(Collision collision)
    {
		if (!this.baseManager.listOfUnits.Contains(collision.gameObject)) {
			if(collision.gameObject.GetComponent<RobotController>())
				lastHitObject = collision.gameObject;
		}

        if (collision.gameObject.layer == Const.LAYER_PLANE)
        {
			for (int i = 0; i < modules.Count; i++){
				MoveModuleScript script = modules[i].GetComponent<MoveModuleScript>();
				if(script) script.willType.Score--;
				if(lastHitObject){
					List<GameObject> mods = lastHitObject.GetComponent<RobotController>().modules;
					for(int j = 0; j < mods.Count; j++){
						var arm = mods[j].GetComponent<ArmModuleScript>();
						if(arm) arm.weaponType.Score++;
					}
				}
			}
            this.baseManager.RemoveUnit(this.transform.parent.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }
    */
	private GameObject lastHitObject = null;
	
	void OnTriggerEnter(Collider collision) {
		if (!this.baseManager.listOfUnits.Contains(collision.gameObject)) {
			if(collision.gameObject.GetComponent<RobotController>())
				lastHitObject = collision.gameObject;
		}
		
		if (collision.gameObject.layer == Const.LAYER_PLANE)
		{
			for (int i = numerOfArm; i < modules.Count; i++){
				MoveModuleScript script = modules[i].GetComponentInChildren<MoveModuleScript>();
				if(script) script.willType.Score--;
				if(lastHitObject){
					List<GameObject> mods = lastHitObject.GetComponent<RobotController>().modules;
					for(int j = 0; j < mods.Count; j++){
						var arm = mods[j].GetComponentInChildren<ArmModuleScript>();
						if(arm) arm.weaponType.Score++;
						lastHitObject.GetComponent<RobotController>().baseManager.unitsKilled++;
						lastHitObject.GetComponent<RobotController>().baseManager.GetComponentsInChildren<TextMesh>()[0].text = "Killed: " + lastHitObject.GetComponent<RobotController>().baseManager.unitsKilled;
					}
				}
			}
			this.baseManager.RemoveUnit(this.transform.parent.gameObject);
			Destroy(this.transform.parent.gameObject);
		}
	}
}
