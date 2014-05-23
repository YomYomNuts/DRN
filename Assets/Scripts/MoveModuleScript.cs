using UnityEngine;
using System.Collections;

public class MoveModuleScript : MonoBehaviour {

	private GameObject wheelFR;
	private GameObject wheelFL;
	private GameObject wheelBR;
	private GameObject wheelBL;

    public WheelType wheelType;

    public void SetModule(WheelType wheelType)
    {
        this.wheelType = wheelType;

        this.SetWheel(out this.wheelFR, this.wheelType.ScaleWheels.ValueElement[0]);
        this.SetWheel(out this.wheelFL, this.wheelType.ScaleWheels.ValueElement[1]);
        this.SetWheel(out this.wheelBR, this.wheelType.ScaleWheels.ValueElement[2]);
        this.SetWheel(out this.wheelBL, this.wheelType.ScaleWheels.ValueElement[3]);

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
