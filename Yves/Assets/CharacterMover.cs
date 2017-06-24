using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {


	private float m_move_v;
	private float m_move_h;

	private float m_look_v;
	private float m_look_h;


	public float moveSpeed;

	public void SetMoveInput(float h_input, float v_input)
	{
		m_move_v = v_input;
		m_move_h = h_input;
	}

	public void SetLookInput (float h_input, float v_input)
	{
		m_look_h = h_input;
		m_look_v = v_input;
	}

	void Update()
	{
		this.transform.position += new Vector3 (m_move_h, m_move_v, 0).normalized * moveSpeed * Time.deltaTime;
		this.transform.forward = new Vector3 (m_look_h, m_look_v, 0);
	}
}
