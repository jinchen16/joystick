using UnityEngine;
using System.Collections;

public class WrapScript : MonoBehaviour {

	public bool wrapX = true;
	public bool wrapY = true;

	private Renderer renderer;
	private Camera camera;
	private Vector2 viewportPos;
	private bool isWrapX;
	private bool isWrapY;
	private Vector2 newPos;


	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();
		camera = Camera.main;
		viewportPos = Vector2.zero;
		isWrapX = false;
		isWrapY = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		Wrap ();
	}

	private void Wrap(){
		bool isVisible = IsBeingRendered ();

		if (isVisible) {
			isWrapX = false;
			isWrapY = false;
		}

		newPos = transform.position;
		viewportPos = camera.WorldToViewportPoint (newPos);

		if(wrapX){
			if(!isWrapX){
				if(viewportPos.x > 1){
					newPos.x = camera.ViewportToWorldPoint(Vector2.zero).x;
					isWrapX = true;
				}
				else if(viewportPos.x < 0){
					newPos.x = camera.ViewportToWorldPoint(Vector2.one).x;
					isWrapX = true;
				}
			}
		}

		if(wrapY){
			if(!isWrapY){
				if(viewportPos.y > 1){
					newPos.y = camera.ViewportToWorldPoint(Vector2.zero).y;
					isWrapY = true;
				}
				else if(viewportPos.y < 0){
					newPos.y = camera.ViewportToWorldPoint(Vector2.one).y;
					isWrapY = true;
				}
			}
		}

		transform.position = newPos;
	}

	private bool IsBeingRendered(){
		if (renderer.isVisible) {
			return true;
		}
		return false;
	}
}
