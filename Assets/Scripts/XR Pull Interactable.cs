using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRPullInteractable : MonoBehaviour
{
    public event Action<float> PullActionReleased;
    public event Action<float> PullUpdated;
    public event Action PullStarted;
    public event Action PullEnded;

    [Header("Pull Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private GameObject _notchPoint;

    public float pullAmount { get; private set; } = 0.0f;

    private LineRenderer _lineRenderer;
    private IXRSelectInteractor _pullingInteractor = null;

    protected override void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        _pullingInteractor = args.interactorObject;
        PullStarted?.Invoke();
    }

    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        PullEnded?.Invoke();
        _pullingInteractor = null;
        pullAmount = 0f;
        _notchPoint.transform.localPosition = new Vector3(_notchPoint.transform.localPosition.x, _notchPoint.transform.localPosition.y, 0f);

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

    }
}
