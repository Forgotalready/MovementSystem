using UnityEngine;
using Zenject;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _maxVerticalAngel = 45f;

    private MovementController _movementController;

    private float _verticalRotation = 0.0f;

    [Inject]
    private void Construct(MovementController movementController) =>
            _movementController = movementController;

    public void Update()
    {
        Vector2 mouseDelta = _movementController.MouseDelta;
        _verticalRotation -= mouseDelta.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalAngel, _maxVerticalAngel);
        _playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseDelta.x);
    }
}