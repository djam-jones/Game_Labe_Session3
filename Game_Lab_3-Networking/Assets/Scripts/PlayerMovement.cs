using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	
	private float _speed;
	private float _moveX;
	private float _zAxis = 0f;
	private bool _jumping;
	private bool _doubleJump;
	private bool _shooting;
	private Vector3 _moveTo;
	private float _attackCooldown;

	public Text text;

	private bool _AttackUsed;
	
	private Rigidbody2D _rigidbody;
	private NetworkView _networkView;

	private int _score;
	private int _totalTime;

	Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();

		_totalTime = 60;
		_attackCooldown = 1;
		_speed = 8f;
		_jumping = false;
		_doubleJump = false;
		_AttackUsed = false;
		
		_rigidbody = GetComponent<Rigidbody2D>();
		_networkView = GetComponent<NetworkView>();

		InvokeRepeating("Timer",1f,1f);
	}
	
	// Update is called once per frame
	void Update () {
		InputMovement();
	}
	
	private void InputMovement()
	{
		//if(_networkView.isMine)
		//{
			if(Input.GetAxis("Horizontal") != 0)
		    {
				if(Input.GetAxis("Horizontal") > 0)
				{
					transform.rotation = Quaternion.Euler(180,0,180);
				}
				else
				{
					transform.rotation = Quaternion.Euler(0,0,0);
				}
				_moveX = Input.GetAxis ("Horizontal");
				_rigidbody.velocity = new Vector3(_moveX * _speed, _rigidbody.velocity.y);
				animator.SetTrigger("Walk");
			}

			if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				Jump();
				animator.SetTrigger("Jump");
			}
			
			if(Input.GetKeyDown(KeyCode.Mouse0) && !_AttackUsed)
			{
				_AttackUsed = true;
				animator.SetTrigger("Dash");
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
			animator.SetTrigger("touchedGround");
			_jumping = false;
			_doubleJump = false;
			Invoke("attackCooldown",_attackCooldown);
		}
		if(collision.gameObject.tag == "player")
		{
			if(_shooting)
			{
				_score++;
			}
		}
	}

	private void Timer()
	{
		_totalTime -= 1;
		text.text = _totalTime.ToString();
		if(_totalTime == 0)
		{
			Screen.lockCursor = true;
		}
	}

	void attackCooldown()
	{
		_AttackUsed = false;
	}
}