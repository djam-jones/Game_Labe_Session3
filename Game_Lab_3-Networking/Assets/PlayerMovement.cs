using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float _Speed = 12f;
	private float _movex = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		_movex = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2 (_movex * _Speed, -_Speed);

		//if(Input.GetKeyDown(KeyCode.Space))
		//{
		//	GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10000f);
		//}
	}
}