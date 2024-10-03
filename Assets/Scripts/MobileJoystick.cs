using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{

    [SerializeField] private RectTransform joystickTransform;
    [SerializeField] private RectTransform knobTransform;

    [SerializeField] private float moveFactor;
    private Vector3 _move;
    private bool _canControl;
    private Vector3 clickedPosition;


    void Start()
    {
        Hide();
    }
    void Update()
    {
        if(_canControl) {
            Control();
        }
    }
    public void ClickedOnJoystick() {
        clickedPosition = Input.mousePosition;
        joystickTransform.position = clickedPosition;
        Show();
    }
    public Vector3 GetMoveVector() {
        return _move;
    }
    private void Control() {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;


        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;

        float absoluteWidth = joystickTransform.rect.width/2;
        float realWidth  = absoluteWidth * canvasScale;

        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);

        _move = direction.normalized * moveMagnitude;
        Vector3 targetPosition = clickedPosition+_move;

        knobTransform.position = targetPosition;
        if(Input.GetMouseButtonUp(0)) {
            Hide();
        }
    }
    private void Show() {
        joystickTransform.gameObject.SetActive(true);
        _canControl = true;
    }
    private void Hide() {
        joystickTransform.gameObject.SetActive(false);
        _canControl = false;
        _move = Vector3.zero;
    }

}
