using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float _Speed;
	private float _movex;
	private bool _jumping;
	private bool _doublejump;
	private bool _shooting;
	private Vector3 _moveTo;

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
			jump();
		}

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(_shooting)
				return;

			_moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);;
			_shooting = true;
			//GetComponent<Rigidbody2D>().gravityScale -= 1;
		}

		if(_shooting)
		{
			transform.position = Vector3.MoveTowards(transform.position,_moveTo,100f * Time.deltaTime);

			if(transform.position.z >= 800)
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,10);
			}
			if(transform.position == _moveTo)
			{
				_shooting = false;
				//GetComponent<Rigidbody2D>().gravityScale += 1;
			}
		}
	}

	private void jump()
	{
		if(_doublejump)
			return;
		
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
		
		if(_jumping)
			_doublejump = true;
		else
			_jumping = true;
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