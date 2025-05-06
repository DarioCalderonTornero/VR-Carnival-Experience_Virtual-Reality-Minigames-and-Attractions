using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ArrowLauncher : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    [SerializeField] private GameObject _trailSystem;

    private Rigidbody _rigidBody;
    private bool _OnAir = false;
    private XRPullInteractable _pullInteractable;

    private void Awake()
    {
        InitializeComponents();
        SetPhysics(false);  
    }

    private void InitializeComponents()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if(_rigidBody == null)
        {
            Debug.LogError("NO rigidbody flechas");
        }
    }

    public void Initialize(XRPullInteractable pullInteractable)
    {
        _pullInteractable = pullInteractable;
        _pullInteractable.PullActionReleased += Release;
    }

    private void OnDestroy()
    {
        if(_pullInteractable != null)
        {
            _pullInteractable.PullActionReleased -= Release;
        }
    }

    private void Release(float value)
    {
        if(_pullInteractable != null)
        {
            _pullInteractable.PullActionReleased -= Release;
        }

        gameObject.transform.parent = null;
        _OnAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * _speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _trailSystem.SetActive(true);
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_OnAir)
        {
            if(_rigidBody != null && _rigidBody.linearVelocity.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(_rigidBody.linearVelocity, transform.up);
            }
            yield return null;
        }
    }

    public void StopFlight()
    {
        _OnAir = false;
        SetPhysics(false);
        _trailSystem.SetActive(true);
    }

    private void SetPhysics(bool usePhysics)
    {
        if(_rigidBody != null)
        {
            _rigidBody.useGravity = usePhysics;
            _rigidBody.isKinematic = !usePhysics;
        }
    }
}
