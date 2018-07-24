using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour {
	public GameObject Circle;
	int Rows = 6;
	int Columns = 7;
	RectTransform CanvasSize;

	public string Active_Player = "Red";
	public bool Game_Active = true;

	void Start () {
		CanvasSize = this.gameObject.GetComponent<RectTransform>();
		New_Map();
	}

	void Update () {
		if (Input.GetMouseButtonDown(0) && Game_Active == false) { //Restart
			for (int a = 1; a <= Rows; a++) {
				for (int b = 1; b <= Columns; b++) {
					Destroy(GameObject.Find("Circle " + b + ":" + a));
				}
			}
			New_Map();
			Game_Active = true;
		}
	}

	void New_Map () {
		for (int a = 0; a < Rows; a++) { //TODO: Convert to a = 1
			for (int b = 0; b < Columns; b++) { //TODO: Convert to b = 1
				float TheX = (CanvasSize.sizeDelta.x / Columns) * (b + .5f) - (CanvasSize.sizeDelta.x / 2);
				float TheY = (CanvasSize.sizeDelta.y / Rows) * (a + .5f) - (CanvasSize.sizeDelta.y / 2);
				GameObject Spawn = Instantiate(Circle);
				Spawn.transform.SetParent(this.gameObject.transform);
				Spawn.transform.localPosition = new Vector2(TheX, TheY);
				Spawn.transform.localScale = new Vector2(1, 1);
				RectTransform Spawn_Size = Spawn.GetComponent<RectTransform>();
				float Size = CanvasSize.sizeDelta.y / Mathf.CeilToInt(Mathf.Sqrt(Rows * Columns));
				Spawn_Size.sizeDelta = new Vector2(Size, Size);
				Spawn.name = "Circle " + (b + 1) + ":" + (a + 1); 
				Spawn.GetComponent<Circle>().Row = a + 1;
				Spawn.GetComponent<Circle>().Column = b + 1;
			}
		}
	}

	public void Someone_Won () { //TODO: Add scores and sh!t
		Game_Active = false;
	}

	public void Check_Stalemate () {
		int Total_Claimed = 0;
		for (int a = 1; a <= Rows; a++) {
			for (int b = 1; b <= Columns; b++) {
				if (GameObject.Find("Circle " + b + ":" + a).GetComponent<Circle>().Owner != "") {
					Total_Claimed++;
				}
			}
		}
		if (Total_Claimed == Rows * Columns) { //Stalemate
			Game_Active = false;
		}
	}
}
