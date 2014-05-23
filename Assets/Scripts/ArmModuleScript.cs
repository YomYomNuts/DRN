using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmModuleScript : MonoBehaviour {
	private Vector3 speed;
	private float amplitude;
	private GameObject obj;

	private float currentTime;
	private bool moveRight = true;

    public WeaponType weaponType;

	// Use this for initialization
	void Start () {

		//this.SetModule(new Vector3(2.0f, 2.0f, -2.0f), 1f);

	}
	
	// Update is called once per frame
	void Update () {

		if (this.currentTime >= this.amplitude) {
			this.currentTime = 0;
			this.moveRight = !this.moveRight;
		} else {
			this.currentTime += Time.deltaTime;
		}

		this.SetRotation ();

	}

    public void SetModule(WeaponType weaponType)
    {
		this.obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		this.obj.transform.parent = this.transform;

        this.weaponType = weaponType;
        this.obj.transform.localScale = this.weaponType.Scale.ValueElement;
        List<float> listInputs = new List<float>();
        listInputs.Add(this.obj.transform.localScale.x);
        listInputs.Add(this.obj.transform.localScale.y);
        listInputs.Add(this.obj.transform.localScale.z);
        List<float> listOuputs = this.weaponType.Network.Evaluate(listInputs);
		this.speed = new Vector3(listOuputs[0], listOuputs[1], listOuputs[2]);
        this.amplitude = listOuputs[3];

		this.currentTime = this.amplitude / 2f;
		this.obj.transform.localPosition = new Vector3(this.obj.transform.localScale.x  / 2, 0, 0);
	}

	private void SetRotation() {
		float temp = (this.moveRight) ? 1f : -1f;
		this.transform.Rotate (new Vector3 (1f, 0f, 0f), temp * (this.speed.x / this.amplitude) * Time.deltaTime * 50);
		this.transform.Rotate (new Vector3 (0f, 1f, 0f), temp * (this.speed.y / this.amplitude) * Time.deltaTime * 50);
		this.transform.Rotate (new Vector3 (0f, 0f, 1f), temp * (this.speed.z / this.amplitude) * Time.deltaTime * 50);
	}
}
