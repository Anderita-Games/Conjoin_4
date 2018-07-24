using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {
	public string Owner = null;
	public int Row;
	public int Column;
	GameObject The_Canvas;

	void Start () {
		The_Canvas = GameObject.Find("Canvas");
	}

	void Update () {
		if (Owner != "" && The_Canvas.GetComponent<Gamemaster>().Game_Active == true) {
			if (GameObject.Find("Circle " + (Column + 1)  + ":" + Row).GetComponent<Circle>().Owner == Owner) { //Right \ Left
				if (GameObject.Find("Circle " + (Column + 2)  + ":" + Row).GetComponent<Circle>().Owner == Owner) {
					if (GameObject.Find("Circle " + (Column + 3)  + ":" + Row).GetComponent<Circle>().Owner == Owner) {
						The_Canvas.GetComponent<Gamemaster>().Someone_Won();
					}
				}
			}
			if (GameObject.Find("Circle " + Column  + ":" + (Row + 1)).GetComponent<Circle>().Owner == Owner) { //Up \ Down
				if (GameObject.Find("Circle " + Column  + ":" + (Row + 2)).GetComponent<Circle>().Owner == Owner) {
					if (GameObject.Find("Circle " + Column  + ":" + (Row + 3)).GetComponent<Circle>().Owner == Owner) {
						The_Canvas.GetComponent<Gamemaster>().Someone_Won();
					}
				}
			}
			if (GameObject.Find("Circle " + (Column + 1)  + ":" + (Row + 1)).GetComponent<Circle>().Owner == Owner) { //45° NE \ SW
				if (GameObject.Find("Circle " + (Column + 2)  + ":" + (Row + 2)).GetComponent<Circle>().Owner == Owner) {
					if (GameObject.Find("Circle " + (Column + 3)  + ":" + (Row + 3)).GetComponent<Circle>().Owner == Owner) {
						The_Canvas.GetComponent<Gamemaster>().Someone_Won();
					}
				}
			}
			if (GameObject.Find("Circle " + (Column - 1)  + ":" + (Row + 1)).GetComponent<Circle>().Owner == Owner) { //45° NW \ SE
				if (GameObject.Find("Circle " + (Column - 2)  + ":" + (Row + 2)).GetComponent<Circle>().Owner == Owner) {
					if (GameObject.Find("Circle " + (Column - 3)  + ":" + (Row + 3)).GetComponent<Circle>().Owner == Owner) {
						The_Canvas.GetComponent<Gamemaster>().Someone_Won();
					}
				}
			}
		}
	}

	public void Claim () { //Should I always claim or keep?
		GameObject Below_Circle = GameObject.Find("Circle " + Column  + ":" + (Row - 1));
		if (Below_Circle && Below_Circle.GetComponent<Circle>().Owner == "") {
			Below_Circle.GetComponent<Circle>().Claim();
		}else if (Owner == "") {
			Owner = The_Canvas.GetComponent<Gamemaster>().Active_Player;
			if (Owner == "Red") {
				this.GetComponent<UnityEngine.UI.RawImage>().color = Color.red;
				The_Canvas.GetComponent<Gamemaster>().Active_Player = "Blue";
			}else {
				this.GetComponent<UnityEngine.UI.RawImage>().color = Color.blue;
				The_Canvas.GetComponent<Gamemaster>().Active_Player = "Red";
			}
			The_Canvas.GetComponent<Gamemaster>().Check_Stalemate();
		}
	}
}
