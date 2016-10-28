using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour {

	public float speed = 0.5f;

	private Rigidbody2D rb2d;
	private Animator anim;
	private Vector3 dir;

	public JoystickBehaviour joystick;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Move Character
		dir = Direction();

		if(dir.Equals(Vector3.zero))
			rb2d.velocity = Vector3.zero;
		else
			rb2d.velocity = dir * speed; 
		
		//Check joystick input for animation parameters
		if(dir.y < 0 && dir.x < 0.5f && dir.x > -0.5f){
			SetAnim(true,"frontWalk");
		}else if(dir.y > 0 && dir.x < 0.5f && dir.x > -0.5f){
			SetAnim(true,"backWalk");
		}else if(dir.x < 0 && dir.y < 0.5f && dir.y > -0.5f){
			SetAnim(true,"leftWalk");
		}else if(dir.x > 0 && dir.y < 0.5f && dir.y > -0.5f){
			SetAnim(true,"rightWalk");
		}else if(dir.y == 0 && dir.x == 0){
			SetAnim(false,null);
		}
	}

	//Return a Vector3 with the direction from joystick input
	public Vector3 Direction(){
		Vector3 direction = Vector3.zero;
		direction.x = joystick.Horizontal();
		direction.y = joystick.Vertical();

		if(direction.magnitude > 1){
			direction.Normalize();
		}
		return direction;
	}

	//Set animation 
	public void SetAnim(bool condition, string action){
		anim.SetBool("isMoving",condition);
		if(action != null){
			anim.SetTrigger(action);
		}
	}
}
