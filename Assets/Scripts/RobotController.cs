using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour {
	/* Parameters */ 
	//Modules: 0 (left arm) 1 (right arm) 2 (bottom module)
	public List<GameObject> modules = new List<GameObject>();

    public GameObject boomPrefab;
    public AudioClip boom;

	/* Privates Attributes */
	private List<Transform> sockets = new List<Transform>();

	private int numerOfArm = 1;
    private Color baseColor;

    private BaseManagerScript baseManager;

    private GameObject lastHitObject = null;

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
            if (i < sockets.Count)
                module.transform.parent = sockets[i];
            else
                module.transform.parent = this.transform.parent;

			module.transform.localRotation = Quaternion.identity;
			module.transform.localPosition = Vector3.zero;
            module.transform.localScale = Vector3.one;

            if (i < this.numerOfArm)
                module.GetComponent<ArmModuleScript>().SetModule(this.baseManager.GetGeneticAlgorithm().GetNewWeapon());
            else
                module.GetComponent<MoveModuleScript>().SetModule(this.baseManager.GetGeneticAlgorithm().GetNewWill());

			Vector3 rotation 				= Vector3.zero;

			if(this.numerOfArm == 2) {
				if (i == 1) {
					rotation.y = 180;
				}
			} else {
				if(i == 0) {
					rotation.y = -90;
				}
			}

			if(i < this.numerOfArm) module.transform.parent.Rotate(rotation);
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < -3) {
			this.baseManager.RemoveUnit(this.transform.parent.gameObject);
			Destroy(this.transform.parent.gameObject);
		}
	}

    public void SetBaseManager(BaseManagerScript baseManager)
    {
        this.baseManager = baseManager;
    }
	
	void OnTriggerEnter(Collider collision) {
		if (!this.baseManager.listOfUnits.Contains(collision.gameObject)) {
			if(collision.gameObject.GetComponent<RobotController>())
				lastHitObject = collision.gameObject;
		}
		
		if (collision.gameObject.layer == Const.LAYER_PLANE)
		{
			for (int i = numerOfArm; i < modules.Count; i++){
				MoveModuleScript script = modules[i].GetComponentInChildren<MoveModuleScript>();
				if(script) script.wheelType.Score--;
				if(lastHitObject){
					List<GameObject> mods = lastHitObject.GetComponent<RobotController>().modules;
					for(int j = 0; j < mods.Count; j++){
						var arm = mods[j].GetComponentInChildren<ArmModuleScript>();
						if(arm) arm.weaponType.Score++;
						lastHitObject.GetComponent<RobotController>().baseManager.unitsKilled++;
					}
				}
			}

			AudioSource.PlayClipAtPoint(boom, this.transform.position);

			baseColor = this.GetComponent<Renderer>().material.color;
            this.boomPrefab.GetComponent<ParticleSystem>().startColor = baseColor;
            GameObject go = (GameObject)Instantiate(this.boomPrefab, this.transform.position, this.boomPrefab.GetComponent<Transform>().localRotation);
            go.transform.parent = this.baseManager.gameObject.transform;

			this.baseManager.RemoveUnit(this.transform.parent.gameObject);
			Destroy(this.transform.parent.gameObject);
		}
	}
}
