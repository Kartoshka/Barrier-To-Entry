using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class PlayerInputManager : MonoBehaviour {

	[Range(1,2)]
	public int playerNumber = 1;

	private CharacterMover m_charMover;
	public WeaponController m_weapon;


	static string MOVE_HORIZONTAL_AXIS = "HorizontalLeft";
	static string MOVE_VERTICAL_AXIS = "VerticalLeft";

	static string LOOK_HORIZONTAL_AXIS = "HorizontalRight";
	static string LOOK_VERTICAL_AXIS = "VerticalRight";

	static string FIRE;
	static string SPECIAL;

	void Start()
	{
		m_charMover = this.GetComponent<CharacterMover> ();
	}

	// Update is called once per frame
	void Update () {

		float move_h_input = Input.GetAxis (getPlayerInputString (MOVE_HORIZONTAL_AXIS));
		float move_v_input = Input.GetAxis (getPlayerInputString (MOVE_VERTICAL_AXIS));

		float look_h_input = Input.GetAxis (getPlayerInputString (LOOK_HORIZONTAL_AXIS));
		float look_v_input = Input.GetAxis (getPlayerInputString (LOOK_VERTICAL_AXIS));

		Debug.Log (new Vector2(look_h_input, look_v_input));
		m_charMover.SetMoveInput (move_h_input, move_v_input);
		m_charMover.SetLookInput (look_h_input, look_v_input);
		
	}

	private string getPlayerInputString(string input)
	{
		if (playerNumber == 1)
		{
			return input += "1";
		} else if (playerNumber == 2)
		{
			return input += "2";
		}

		return input;
	}
}
