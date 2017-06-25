using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTextOverTime : MonoBehaviour {

	public float fadeDuration;
	public bool disableOnEnd;
	private float m_startTime;

	private Text fadingText;
	private float initialAlpha;
	// Use this for initialization
	void Start () {
		m_startTime = Time.time;
		fadingText = this.GetComponent<Text> ();
		if (fadingText != null)
		{
			initialAlpha = fadingText.color.a;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		if ((currentTime - m_startTime) < fadeDuration && fadingText != null )
		{
			Color c = fadingText.color;
			c.a =initialAlpha * (1-((currentTime - m_startTime) / fadeDuration));
			fadingText.color = c;
		} else
		{
			Color c = fadingText.color;
			c.a = initialAlpha;
			fadingText.color = c;
			this.gameObject.SetActive (disableOnEnd);
			Destroy (this);
		}
	}
}
