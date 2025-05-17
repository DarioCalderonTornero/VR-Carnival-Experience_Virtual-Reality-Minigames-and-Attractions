using System.Collections;
using TMPro;
using UnityEngine;

public class ChooseHammerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickUpHammerText;


    private void Start()
    {
        Hide();
        BeginTopos.Instance.OnPlayerChooseHammer += BeginTopos_OnPlayerChooseHammer;
    }

    private void BeginTopos_OnPlayerChooseHammer(object sender, System.EventArgs e)
    {
        StartCoroutine(Show());
    }

    private void Hide()
    {
        pickUpHammerText.gameObject.SetActive(false);
    }

    private IEnumerator Show()
    {
        pickUpHammerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        pickUpHammerText.gameObject.SetActive(false);
    }
}
