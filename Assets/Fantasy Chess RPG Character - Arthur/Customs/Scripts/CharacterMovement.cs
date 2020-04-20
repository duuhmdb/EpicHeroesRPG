using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	//public FixedTouchField fixedTouchField;
	//float _gravity = 5;
	//float _charRotation = 0f;

	float _speed = 0.2f;
	//float _rotationSpeed = 100;

	//Vector3 _moveDirection = Vector3.zero;
	CharacterController _characterController;
	Animator _animator;
	Joystick joystick;
	Rigidbody _rigidbody;

	protected float CameraAngleY;
	protected float CameraAngleSpeed = 0.1f;
	protected float CameraPosY;
	protected float CameraPosSpeed = 0.1f;

	// Start is called before the first frame update
	void Start()
	{
		joystick = FindObjectOfType<Joystick>();
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody>();
		//_rotationSpeed *= Time.deltaTime;
		_characterController = GetComponent<CharacterController>();
	}

	//// Update is called once per frame
	void Update()
	{
		var input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
		var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input * 5f;

		_rigidbody.velocity = new Vector3(vel.x, _rigidbody.velocity.y, vel.z);
		transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);

		//CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

		//Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 4);
		Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);

		if (_rigidbody.velocity.magnitude > 3f)
		{
			_characterController.Move(input);
			//input *= _speed;
			//_moveDirection = transform.TransformDirection(targetDirection);
			_animator.SetBool("running", true);
			_animator.SetInteger("condition", 1);
		}
		else
		{
			_animator.SetBool("running", false);
			_animator.SetInteger("condition", 0);
		}

	}
}
