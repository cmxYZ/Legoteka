using UnityEngine;
using UnityEngine.UI;

public class CameraControll : MonoBehaviour
{
    public enum Direction
    { 
        Horizontal,
        Vertical,
        None
    }

    public Transform cameraParent;
    public Camera mainCamera;
    public float sensitivity = 0.005f;
    public float rotateSensitivity = 0.05f;
    public float zoomSensitivity = 0.05f;
    public float zoomMin = 40;
    public float zoomMax = 90;
    public float YMin = 0;
    public float YMax = 10;
    public Button OnOffButt;

    private bool movingState = false;
    private bool touched;
    private Vector3 startPos;
    private Direction camDirection = Direction.None;
    private Vector3 delta;
    private float zoomPrevMagnitude;
    private bool firstZoomTouch;

    public void Start()
    {
        OnOffButt.onClick.AddListener(PressButton);
    }

    public void Update()
    {
        if (movingState) TrackTouches();
    }

    public void TrackTouches()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPos = Input.mousePosition;
            touched = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touched = false;
            camDirection = Direction.None;
        }
        if (Input.touchCount == 2)
            ZoomCamera();
        else
        {
            firstZoomTouch = true;
            if (touched) MoveCamera();
        }
    }

    public void MoveCamera()
    {
        delta = startPos - Input.mousePosition;
        startPos = Input.mousePosition;
        if (camDirection == Direction.Horizontal || 
            camDirection == Direction.None && Mathf.Abs(delta.x) >= Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > 1.0f)
        {
            camDirection = Direction.Horizontal;
            cameraParent.transform.Rotate(0, -delta.x * rotateSensitivity, 0);
        }
        else if (camDirection == Direction.Vertical || 
            camDirection == Direction.None && Mathf.Abs(delta.x) < Mathf.Abs(delta.y) && Mathf.Abs(delta.y) > 1.0f)
        {
            camDirection = Direction.Vertical;
            transform.position = new Vector3(transform.position.x,
                Mathf.Clamp(transform.position.y + delta.y * sensitivity, YMin, YMax), 
                transform.position.z);
        }
    }

    public void ZoomCamera()
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (firstZoomTouch)
        {
            zoomPrevMagnitude = (touch1.position - touch2.position).magnitude;
            firstZoomTouch = false;
        }

        float currentMagnitude = (touch1.position - touch2.position).magnitude;
        float delta = currentMagnitude - zoomPrevMagnitude;
        zoomPrevMagnitude = currentMagnitude;

        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - delta * zoomSensitivity, zoomMin, zoomMax);
    }

    public void PressButton()
    {
        movingState = !movingState;
    }
}