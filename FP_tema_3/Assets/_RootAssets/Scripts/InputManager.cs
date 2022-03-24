using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    
    private TouchControls touchControls;

    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += StartTouch;
        touchControls.Touch.TouchPress.canceled += EndTouch;
    }




    private void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch == null) 
            return;
        
        var rawPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        var position = Utils.ScreenToWorldPosition(mainCamera, rawPosition);
        OnStartTouch(position, (float) context.startTime);
    }
    
    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch == null)
            return;
        
        var rawPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        var position = Utils.ScreenToWorldPosition(mainCamera, rawPosition);
        OnEndTouch(position, (float) context.startTime);
    }

    public Vector2 TouchPosition()
    {
        var rawPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        return Utils.ScreenToWorldPosition(mainCamera, rawPosition);
    }

    
    
}
