using UnityEngine;
using System.Collections;

public class MoveModuleScript : MonoBehaviour {

	private GameObject wheelFR;
	private GameObject wheelFL;
	private GameObject wheelBR;
	private GameObject wheelBL;

    public WillType willType;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetModule(WillType willType)
    {
        this.willType = willType;

        this.SetWheel(out this.wheelFR, this.willType.ScaleWills.ValueElement[0]);
        this.SetWheel(out this.wheelFL, this.willType.ScaleWills.ValueElement[1]);
        this.SetWheel(out this.wheelBR, this.willType.ScaleWills.ValueElement[2]);
        this.SetWheel(out this.wheelBL, this.willType.ScaleWills.ValueElement[3]);

		this.wheelFR.transform.localPosition = new Vector3 (0.5f, -this.wheelFR.transform.localScale.y / 2.0f, 0.5f);
		this.wheelFL.transform.localPosition = new Vector3 (-0.5f, -this.wheelFL.transform.localScale.y / 2.0f, 0.5f);
		this.wheelBR.transform.localPosition = new Vector3 (0.5f, -this.wheelBR.transform.localScale.y / 2.0f, -0.5f);
		this.wheelBL.transform.localPosition = new Vector3 (-0.5f, -this.wheelBL.transform.localScale.y / 2.0f, -0.5f);
	}

	void SetWheel(out GameObject wheel, Vector3 scale) {
		wheel = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		wheel.transform.parent = this.transform;
        wheel.transform.localScale = scale;
	}
}
