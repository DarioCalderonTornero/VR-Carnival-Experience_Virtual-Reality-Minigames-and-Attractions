using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DangerZoneEffect : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private Vignette vignette;

    private void Start()
    {
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f; 
        }
    }

    public void EnableWarningEffect()
    {
        if (vignette != null)
            vignette.intensity.value = 0.35f;
    }

    public void DisableWarningEffect()
    {
        if (vignette != null)
            vignette.intensity.value = 0f;
    }
}
