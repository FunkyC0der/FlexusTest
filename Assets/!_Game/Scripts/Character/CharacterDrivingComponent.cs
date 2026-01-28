using System.Collections.Generic;
using FlexusTest.Cameras;
using FlexusTest.Vehicle;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Events;
using CameraType = FlexusTest.Cameras.CameraType;

namespace FlexusTest.Character
{
  public class CharacterDrivingComponent : MonoBehaviour<ICharacterInput, ICameraService>
  {
    public UnityEvent OnDrivingStarted;
    public UnityEvent OnDrivingStopped;
      
    private IVehicle _drivingVehicle;
    private readonly List<IVehicle> _vehiclesToEnter = new();

    private ICharacterInput _characterInput;
    private ICameraService _cameraService;

    protected override void Init(ICharacterInput characterInput, ICameraService cameraService)
    {
        _characterInput = characterInput;
        _cameraService = cameraService;
    }

    private bool IsDriving => _drivingVehicle != null;

    private void OnDestroy()
    {
        if (_characterInput != null)
        {
            _characterInput.OnEnterExitVehicle -= OnEnterVehicleAction;
            _characterInput.OnEnterExitVehicle -= OnExitVehicleAction;
        }
    }

    private void Update()
    {
        if (IsDriving)
        {
            UpdateVehicleInput();
            UpdateDriverPosition();
        }
    }

    private void UpdateVehicleInput()
    {
        _drivingVehicle.SetMoveInput(_characterInput.Move());
        _drivingVehicle.SetBrakeIsPressed(_characterInput.BrakeIsPressed());
    }

    private void UpdateDriverPosition() => 
        transform.position = _drivingVehicle.GetTransform().position;

    private void OnTriggerEnter(Collider other)
    {
        var vehicle = other.attachedRigidbody?.GetComponent<IVehicle>();
        if (vehicle == null)
            return;
        
        _vehiclesToEnter.Add(vehicle);
        
        if(IsDriving)
            return;
        
        if(_vehiclesToEnter.Count == 1)
            _characterInput.OnEnterExitVehicle += OnEnterVehicleAction;
    }

    private void OnTriggerExit(Collider other)
    {
        var vehicle = other.attachedRigidbody?.GetComponent<IVehicle>();
        if (vehicle == null)
            return;
        
        _vehiclesToEnter.Remove(vehicle);
        
        if (IsDriving)
            return;
        
        if (_vehiclesToEnter.Count == 0)
            _characterInput.OnEnterExitVehicle -= OnEnterVehicleAction;
    }

    private void EnterToVehicle(IVehicle vehicle)
    {
        if (vehicle == null)
            return;
        
        _drivingVehicle = vehicle;
        _drivingVehicle.Enter();
        
        _cameraService.SetCameraTarget(CameraType.Vehicle, vehicle.GetTransform());
        _cameraService.ActivateCamera(CameraType.Vehicle);
        
        _characterInput.OnEnterExitVehicle -= OnEnterVehicleAction;
        _characterInput.OnEnterExitVehicle += OnExitVehicleAction;
        
        OnDrivingStarted?.Invoke();
    }

    private void ExitFromVehicle()
    {
        transform.position = _drivingVehicle.GetDriverExitPosition();

        _drivingVehicle.Exit();
        _drivingVehicle = null;
        
        _cameraService.ActivateCamera(CameraType.Character);
        
        _characterInput.OnEnterExitVehicle -= OnExitVehicleAction;
        _characterInput.OnEnterExitVehicle += OnEnterVehicleAction;
        
        OnDrivingStopped?.Invoke();
    }

    private void OnEnterVehicleAction() => 
        EnterToVehicle(TryGetClosestFreeVehicle());

    private void OnExitVehicleAction() => 
        ExitFromVehicle();

    private IVehicle TryGetClosestFreeVehicle()
    {
        IVehicle vehicle = null;
        float closestDistance = float.MaxValue;
        foreach (IVehicle vehicleToEnter in _vehiclesToEnter)
        {
            float distance = Vector3.Distance(transform.position, vehicleToEnter.GetTransform().position);
            if (distance < closestDistance && vehicleToEnter.IsFree())
            {
                closestDistance = distance;
                vehicle = vehicleToEnter;
            }
        }
        return vehicle;
    }
  }
}