using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour {
    public static InputHandler Instance { get; private set; }

    public static event UnityAction GotEscapeKeyDown;
    public static event UnityAction GotPrimaryMouseButtonDown;

    public static int PrimaryMouseButton { get; private set; } = 0;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) GotEscapeKeyDown?.Invoke();
        if (Input.GetMouseButtonDown(PrimaryMouseButton)) GotPrimaryMouseButtonDown?.Invoke();
    }
}