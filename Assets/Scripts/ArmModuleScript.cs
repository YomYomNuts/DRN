using UnityEngine;
using System.Collections;

public class ArmModuleScript : MonoBehaviour {
	private Vector3 speed;
	private float amplitude;
	private GameObject obj;

	private float currentTime;
	private bool moveRight = true;

	// Use this for initialization
	void Start () {
		this.obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		this.obj.transform.parent = this.transform;

		this.SetModule(new Vector3(2.0f, 2.0f, -2.0f), 1f);
		this.currentTime = this.amplitude / 2f;

		this.obj.transform.localPosition = new Vector3(this.transform.localPosition.x - this.obj.transform.localScale.x  / 2, this.transform.localPosition.y, this.transform.localPosition.z);	
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

	public void SetModule(Vector3 _speed, float _amplitude) {
		this.speed = _speed;
		this.amplitude = _amplitude;
		this.obj.transform.localScale = new Vector3 (Random.value, Random.value, Random.value);
	}

	private void SetRotation() {
		float temp = (this.moveRight) ? 1f : -1f;
		this.transform.Rotate (new Vector3 (1f, 0f, 0f), temp * this.speed.x * Time.deltaTime * 50);
		this.transform.Rotate (new Vector3 (0f, 1f, 0f), temp * this.speed.y * Time.deltaTime * 50);
		this.transform.Rotate (new Vector3 (0f, 0f, 1f), temp * this.speed.z * Time.deltaTime * 50);
	}
}
