using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickBehaviour : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image bgImage;
	private Image joystickImage;
	private Vector3 inputVector;

	private void Start(){
		bgImage = GetComponent<Image>();
		joystickImage = transform.GetChild(0).GetComponent<Image>();
	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag(ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){
		inputVector = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = inputVector;
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, 
			ped.position, ped.pressEventCamera, out pos)){

			pos.x = (pos.x/bgImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y/bgImage.rectTransform.sizeDelta.y);

			inputVector = new Vector3(pos.x*2, pos.y*2, 0);
			inputVector = (inputVector.magnitude > 1.0f)?inputVector.normalized:inputVector;

			//Move joystick image
			joystickImage.rectTransform.anchoredPosition = new Vector3(
				inputVector.x * (bgImage.rectTransform.sizeDelta.x/3),
				inputVector.y * (bgImage.rectTransform.sizeDelta.y/3));
		}
	}

	//Get the horizontal input
	public float Horizontal(){
		if(inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis("Horizontal");
	}

	//Get the vertical input
	public float Vertical(){
		if(inputVector.y != 0)
			return inputVector.y;
		else
			return Input.GetAxis("Vertical");
	}
}
