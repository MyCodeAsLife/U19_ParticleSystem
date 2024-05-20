using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _lifeTime;

    private float _timeLeave;
    private float _lastDistance;

    public Vector3 HitPosition;

    public float FireRate => _fireRate;

    private float CalculateDistance(Vector3 firstPos, Vector3 secondPosition) => Vector3.Distance(firstPos, secondPosition);

    private void Start()
    {
        _timeLeave = Time.time + _lifeTime;

        if (HitPosition != Vector3.zero)
            _lastDistance = CalculateDistance(transform.position, HitPosition);
        else
            _lastDistance = -1;
    }

    private void LateUpdate()
    {
        transform.Translate(transform.forward * (_speed * Time.deltaTime), Space.World);

        if (_lastDistance > -1)
        {
            float currentDistance = CalculateDistance(transform.position, HitPosition);

            if (currentDistance < _lastDistance)
                _lastDistance = currentDistance;
            else
                Explosion();
        }

        if (Time.time >= _timeLeave)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Explosion();
    }

    private void Explosion()
    {
        ParticleSystem vfxExplosion = Instantiate(_explosionPrefab, HitPosition, Quaternion.identity);
        _speed = 0;
        Destroy(gameObject);
    }
}
