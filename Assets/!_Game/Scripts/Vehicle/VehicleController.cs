using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FlexusTest.Vehicle
{
  public class VehicleController : MonoBehaviour, IVehicle
  {
    [Serializable]
    private class Wheel
    {
      public WheelController Controller;
      public bool IsMotor;
      public bool IsSteering;
    }

    public UnityEvent OnDriverEnter;
    public UnityEvent OnDriverExit;

    [SerializeField]
    private List<Wheel> _wheels;

    [SerializeField]
    private float _motorForce = 1000;

    [SerializeField]
    private float _maxSteerAngle = 30;

    [SerializeField]
    private float _brakeForce = 2000;

    [SerializeField]
    private Transform _driverExitPoint;

    [Header("Vehicle Input")]
    [SerializeField]
    private Vector2 _moveInput = Vector2.zero;

    [SerializeField]
    private bool _brakeIsPressed;

    private bool _isFree = true;

    private void OnEnable()
    {
      foreach (Wheel wheel in _wheels) 
        wheel.Controller.enabled = true;
    }

    private void OnDisable()
    {
      foreach (Wheel wheel in _wheels)
        wheel.Controller.enabled = false;
    }

    public bool IsFree() =>
      _isFree;

    public void SetIsFree(bool isFree) =>
      _isFree = isFree;

    public void Enter()
    {
      SetIsFree(false);
      OnDriverEnter?.Invoke();
    }

    public void Exit()
    {
      SetIsFree(true);
      
      SetMoveInput(Vector2.zero);
      SetBrakeIsPressed(false);
      
      OnDriverExit?.Invoke();
    }

    public void SetMoveInput(Vector2 input) =>
      _moveInput = input;

    public void SetBrakeIsPressed(bool isPressed) =>
      _brakeIsPressed = isPressed;

    public Vector3 GetDriverExitPosition() =>
      _driverExitPoint.position;

    public Transform GetTransform() =>
      transform;

    private void FixedUpdate()
    {
      bool isAccelerating = Mathf.Abs(_moveInput.y) > 0;
      float brakeForce = _brakeIsPressed ? _brakeForce : 0;

      foreach (Wheel wheel in _wheels)
      {
        if (wheel.IsMotor)
          wheel.Controller.Collider.motorTorque = _moveInput.y * _motorForce;

        if (wheel.IsSteering)
          wheel.Controller.Collider.steerAngle = _moveInput.x * _maxSteerAngle;

        UpdateWheelBrakeForce(wheel, brakeForce, isAccelerating);
      }
    }

    private void UpdateWheelBrakeForce(Wheel wheel, float brakeForce, bool isAccelerating)
    {
      if (wheel.IsMotor && !isAccelerating)
        brakeForce += _motorForce;

      wheel.Controller.Collider.brakeTorque = brakeForce;
    }
  }
}