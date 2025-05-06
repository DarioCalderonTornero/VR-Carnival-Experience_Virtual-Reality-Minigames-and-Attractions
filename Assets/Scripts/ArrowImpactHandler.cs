using UnityEngine;

public class ArrowImpactHandler : MonoBehaviour
{
    [SerializeField] private bool _explodeOnImpact = false;
    [SerializeField] private float _stickDuration = 3f;
    [SerializeField] private float _minEmbedDepth = 0.05f;
    [SerializeField] private float _maxEndedDepth = 0.15f;
    [SerializeField] private LayerMask _ignoredLayers;
    [SerializeField] private Transform _tip;

    [SerializeField] private GameObject _impactGameObject;
    [SerializeField] private MeshRenderer _arrowMeshRenderer;


    private ArrowLauncher _arrowLauncher;
    private Rigidbody _rigidBody;
    private bool _hasHit = false;

    private void Awake()
    {
        _arrowLauncher = GetComponent<ArrowLauncher>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit || ((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }

        _hasHit = true;
        _arrowLauncher.StopFlight();

        if (_explodeOnImpact)
        {
            // HandleExplosion();
        }
        else
        {
            // HandleStick(collision);
        }
    }

}
