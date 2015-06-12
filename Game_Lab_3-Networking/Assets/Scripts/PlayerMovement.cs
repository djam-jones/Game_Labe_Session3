using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	private float _speed;
	private float _moveX;
	private float _zAxis = 0f;
	private bool _jumping;
	private bool _doubleJump;
	private bool _shooting;
	private Vector3 _moveTo;
	private float _attackCooldown;

	private bool _AttackUsed;
	
	private Rigidbody2D _rigidbody;
	private NetworkView _networkView;
	
	void Start()
	{
		_attackCooldown = 1;
		_speed = 8f;
		_jumping = false;
		_doubleJump = false;
		_AttackUsed = false;
		
		_rigidbody = GetComponent<Rigidbody2D>();
		_networkView = GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("test");

		if(Input.anyKey)
			InputMovement();
	}
	
	private void InputMovement()
	{
		//if(_networkView.isMine)
		//{
			_moveX = Input.GetAxis ("Horizontal");
			_rigidbody.velocity = new Vector3(_moveX * _speed, _rigidbody.velocity.y);
			
			if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				Jump();
			}
			
			if(Input.GetKeyDown(KeyCode.Mouse0) && !_AttackUsed)
			{
				_AttackUsed = true;
				Dash();
			}
			
			if(_shooting)
			{
				transform.position = Vector3.MoveTowards(transform.position, _moveTo, 100f * Time.deltaTime);
				
				if(transform.position.z >= 800)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, _zAxis);
				}
				
				if(transform.position == _moveTo)
				{
					_shooting = false;
					//_rigidbody.gravityScale += 1;
				}
			}
		//}
	}
	
	private void Jump()
	{
		if(_doubleJump)
			return;
		
		_rigidbody.AddForce(Vector2.up * 400f);
		
		if(_jumping)
			_doubleJump = true;
		else
			_jumping = true;
	}
	
	private void Dash()
	{
		if(_shooting)
			return;
		
		_moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_moveTo.z = transform.position.z;
		_shooting = true;
		//_rigidbody.gravityScale -= 1;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "floor")
		{
			_jumping = false;
			_doubleJump = false;
			Invoke("attackCooldown",_attackCooldown);
		}
	}

	void attackCooldown()
	{
		_AttackUsed = false;
	}
}