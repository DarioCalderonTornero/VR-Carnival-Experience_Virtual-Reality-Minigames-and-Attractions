using UnityEngine;

public class KeyboardInputs : MonoBehaviour
{
    public static KeyboardInputs Instance {  get; private set; }

    private InputSystem_Actions keyInputSystem;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        keyInputSystem = new InputSystem_Actions();
        keyInputSystem.Player.Enable();

        keyInputSystem.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Interacted");
    }
}
