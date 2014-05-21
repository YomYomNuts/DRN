using UnityEngine;
using System.Collections;

public class MoveModuleScript : MonoBehaviour {

	private GameObject wheelFR;
	private GameObject wheelFL;
	private GameObject wheelBR;
	private GameObject wheelBL;

	// Use this for initialization
	void Start () {
		float [] scalesList = {Random.value, Random.value, Random.value, Random.value};
		this.SetModule (scalesList);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetModule(float[] scalesList) {
		this.SetWheel(out this.wheelFR, scalesList[0]);
		this.SetWheel(out this.wheelFL, scalesList[1]);
		this.SetWheel(out this.wheelBR, scalesList[2]);
		this.SetWheel(out this.wheelBL, scalesList[3]);

		this.wheelFR.transform.localPosition = new Vector3 (0.5f, -this.wheelFR.transform.localScale.y / 2.0f, 0.5f);
		this.wheelFL.transform.localPosition = new Vector3 (-0.5f, -this.wheelFL.transform.localScale.y / 2.0f, 0.5f);
		this.wheelBR.transform.localPosition = new Vector3 (0.5f, -this.wheelBR.transform.localScale.y / 2.0f, -0.5f);
		this.wheelBL.transform.localPosition = new Vector3 (-0.5f, -this.wheelBL.transform.localScale.y / 2.0f, -0.5f);
	}

	void SetWheel(out GameObject wheel, float scale) {
		wheel = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		wheel.transform.parent = this.transform;
		wheel.transform.localScale = new Vector3 (scale, scale, scale);
	}
}
