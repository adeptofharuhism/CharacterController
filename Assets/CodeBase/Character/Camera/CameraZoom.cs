using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _defaultDistance = 6f;
    [SerializeField] private float _minimunDistance = 1f;
    [SerializeField] private float _maximumDistance = 6f;

    [SerializeField] private float _smoothing = 4f;
    [SerializeField] private float _zoomSensitivity = 1f;

    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineInputProvider _inputProvider;

    private float _currentTargetDistance;

    private void Awake() {
        _framingTransposer = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();

        _inputProvider = GetComponent<CinemachineInputProvider>();

        _currentTargetDistance = _defaultDistance;
    }

    private void Update() {
        Zoom();
    }

    private void Zoom() {
        float zoomValue = _inputProvider.GetAxisValue(2) * _zoomSensitivity;

        _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, _minimunDistance, _maximumDistance);

        float currentDistance = _framingTransposer.m_CameraDistance;
        if (currentDistance == _currentTargetDistance)
            return;

        float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, _smoothing * Time.deltaTime);

        _framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}
