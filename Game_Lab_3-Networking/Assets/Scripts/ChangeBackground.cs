using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeBackground : MonoBehaviour {

	private Image _backgroundIMG;
	private Color _randomNewColor;

	void Start()
	{
		_backgroundIMG = GetComponent<Image>();
	}

	void Update()
	{
		RandomizeColor();
	}

	void RandomizeColor()
	{
		_randomNewColor = new Color32( (byte)Random.Range(0f, 255f), (byte)Random.Range(0f, 255f), (byte)Random.Range(0f, 255f), 255 );

		_backgroundIMG.color = _randomNewColor;
	}

}
