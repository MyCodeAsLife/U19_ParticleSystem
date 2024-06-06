using UnityEngine;

[RequireComponent(typeof(MouseRotator))]
public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ProjectileMover _bulletPrefab;

    private MouseRotator _mouseRotator;
    private ProjectileMover _projectileMover;
    private float _timeToFire = 0;

    private void Start()
    {
        _mouseRotator = GetComponent<MouseRotator>();
        _projectileMover = _bulletPrefab.GetComponent<ProjectileMover>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1 / _projectileMover.FireRate;
            SpawnVFX();
        }
    }

    private void SpawnVFX()
    {
        RaycastHit hit;

        ProjectileMover vfx = Instantiate(_bulletPrefab, _firePoint.transform.position, Quaternion.identity);
        vfx.transform.rotation = _mouseRotator.transform.rotation;

        if (Physics.Raycast(_firePoint.transform.position, transform.forward, out hit))
            vfx.HitPosition = hit.point;
        else
            vfx.HitPosition = Vector3.zero;
    }
}