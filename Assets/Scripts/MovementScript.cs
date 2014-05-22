using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public GameObject enemy;
	public float speedMovement;
	public GameObject motherBase;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		this.rigidbody.velocity = new Vector3(0.0f, this.rigidbody.velocity.y, 0.0f);
		
		if(enemy == null)
			enemy = motherBase.GetComponent<BaseManagerScript>().GetClosestEnemy(this.transform.position);
		
		if(enemy != null)
		{
			this.transform.LookAt(enemy.transform.position);
			this.rigidbody.AddRelativeForce(0.0f,0.0f,1.0f * speedMovement * 10 * Time.deltaTime );
		}
	}
	
}
