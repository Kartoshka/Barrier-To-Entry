using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {


	private Vector3 desiredVelocity;
	private Vector3 currentVelocity;
	private float m_move_v;
	private float m_move_h;

	private float m_look_v;
	private float m_look_h;

	private Vector3 lookDirection;

	public float moveSpeed;
	[Range(0.01f,1f)]
	public float accelerationRatio = 1.0f;
	[Range(0.01f,1f)]
	public float rotationSpeed = 1f;

	public void SetMoveInput(float h_input, float v_input)
	{
		m_move_v = v_input;
		m_move_h = h_input;

		desiredVelocity = new Vector3 (m_move_h, 0, m_move_v);
	}

	public void SetLookInput (float h_input, float v_input)
	{
		m_look_h = h_input;
		m_look_v = v_input;
	}

	void Update()
	{
		currentVelocity = Vector3.Lerp (currentVelocity, desiredVelocity, accelerationRatio);
		this.transform.position += currentVelocity * moveSpeed * Time.deltaTime;
		//this.transform.position += new Vector3 (m_move_h, 0, m_move_v) * moveSpeed * Time.deltaTime;
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation (new Vector3 (m_look_h, 0, m_look_v),new Vector3(0,1,0)) , rotationSpeed);
	}
}
