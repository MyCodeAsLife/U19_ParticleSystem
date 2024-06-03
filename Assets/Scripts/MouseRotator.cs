using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxLength;

    private Ray _rayMouse;
    private RaycastHit _hit;
    private Vector3 _direction;
    private Quaternion _rotation;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        _rayMouse = _camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(_rayMouse.origin, _rayMouse.direction, out _hit, _maxLength))
        {
            RotateToMouseDirection(gameObject, _hit.point);
        }
        else
        {
            Vector3 position = _rayMouse.GetPoint(_maxLength);
            RotateToMouseDirection(gameObject, position);
        }
    }

    private void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        _direction = destination - obj.transform.position;
        _rotation = Quaternion.LookRotation(_direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, _rotation, 1);
    }

    public Vector3 GetHitPosition()
    {
        return _hit.point;
    }
}//
