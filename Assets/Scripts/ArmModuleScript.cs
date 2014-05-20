using UnityEngine;
using System.Collections;

public class ArmModuleScript : MonoBehaviour {
	private Vector3 speed;
	private float amplitude;
	private GameObject baseObject;

	private float currentTime;
	private bool moveRight = true;

	// Use this for initialization
	void Start () {

		this.baseObject = new GameObject ();
		this.SetModule(new Vector3(2.0f, 2.0f, -2.0f), 1f);
		this.currentTime = this.amplitude / 2f;
		this.transform.parent = this.baseObject.transform;
		this.transform.localPosition = new Vector3(this.transform.localPosition.x - this.transform.localScale.x  / 2, this.transform.localPosition.y, this.transform.localPosition.z);	
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
		this.transform.localScale = new Vector3 (Random.value, Random.value, Random.value);
	}

	private void SetRotation() {
		float temp = (this.moveRight) ? 1f : -1f;
		this.baseObject.transform.Rotate (new Vector3 (1f, 0f, 0f), temp * this.speed.x * Time.deltaTime * 50);
		this.baseObject.transform.Rotate (new Vector3 (0f, 1f, 0f), temp * this.speed.y * Time.deltaTime * 50);
		this.baseObject.transform.Rotate (new Vector3 (0f, 0f, 1f), temp * this.speed.z * Time.deltaTime * 50);
	}
}
