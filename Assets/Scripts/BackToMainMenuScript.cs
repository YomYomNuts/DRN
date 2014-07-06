 using UnityEngine;
using System.Collections;

public class BackToMainMenuScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnGUI()
    {
        GUI.Button(new Rect(Screen.width - 210, Screen.height - 50, 200, 20), "Esc to return to the main menu");
        if (Input.GetKeyDown("escape"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
