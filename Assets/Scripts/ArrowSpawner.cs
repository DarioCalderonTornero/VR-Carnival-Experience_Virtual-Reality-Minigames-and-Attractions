using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _notchPoint;
    [SerializeField] private float _spawnDelay = 1f;

    private XRGrabInteractable _bow;
    private XRPullInteractable _pullInteractable;
    private bool _arrowNotched = false;
    private GameObject _currectArrow = null;

    private void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        _pullInteractable = GetComponentInChildren<XRPullInteractable>();

        if(_pullInteractable != null)
        {
            _pullInteractable.PullActionReleased += NotchEmpty;
        }
    }

    private void OnDestroy()
    {
        if(_pullInteractable != null)
        {
            _pullInteractable.PullActionReleased -= NotchEmpty;
        }
    }

    private void Update()
    {
        if(_bow.isSelected && !_arrowNotched)
        {
            _arrowNotched = true;
            StartCoroutine(DelayedSpawn());
        }

        if(!_bow.isSelected && _currectArrow != null)
        {
            Destroy(_currectArrow);
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currectArrow = null;
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(_spawnDelay);

        _currectArrow = Instantiate(_arrowPrefab, _notchPoint.transform);

        ArrowLauncher launcher = _currectArrow.GetComponent<ArrowLauncher>();
        if(launcher != null && _pullInteractable != null)
        {
            launcher.Initialize(_pullInteractable);
        }
    }
}
