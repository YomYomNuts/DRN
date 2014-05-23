using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour {

	private GameObject[] bases;

	public GUISkin skin;

	// Use this for initialization
	void Start () {
		bases = GameObject.FindGameObjectsWithTag("Base");
	}

	void OnGUI() {
		GUI.skin = skin;

		GUI.BeginGroup (new Rect (10, 10, 150, 415));
		GUI.Box (new Rect (0, 0, 140, 415), "");

		for (int i=0; i<bases.Length; i++) {
			GUI.color = bases[i].renderer.material.color;
			GUI.skin.label.fontSize = 20;
			GUI.Label (new Rect (10, 10 + 80 * i, 150, 80), "Base " + (i+1));
			GUI.skin.label.fontSize = 15;

			int frags = this.bases [i].GetComponent<BaseManagerScript> ().unitsKilled;
			int lost = this.bases [i].GetComponent<BaseManagerScript> ().unitsRemoved;

			//GUI.color = Color.green;
			GUI.Label (new Rect (30, 30 + 80 * i, 150, 80), "Frags : " + frags);			
			//GUI.color = Color.red;		
			GUI.Label (new Rect (30, 45 + 80 * i, 150, 80), "Lost : " + lost);

			float ratio = (lost != 0) ? (float)frags/(float)lost : 0; 

			GUI.color = Color.white;		
			GUI.Label (new Rect (30, 60 + 80 * i, 150, 80), "Ratio : " + string.Format("{0:0.00}", ratio));
		}		

		GUI.EndGroup ();
	}
}
