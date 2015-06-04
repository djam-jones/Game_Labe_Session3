using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float _Speed;
	private float _movex;
	private bool _jumping;
	private bool _doublejump;

	void Start()
	{
		_Speed = 12f;
		_jumping = false;
		_doublejump = false;
	}

	// Update is called once per frame
	void Update () {
		_movex = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2 (_movex * _Speed, GetComponent<Rigidbody2D>().velocity.y);

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(_doublejump)
				return;

			GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);

			if(_jumping)
				_doublejump = true;
			else
				_jumping = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "floor")
		{
			_jumping = false;
			_doublejump = false;
		}
	}
}