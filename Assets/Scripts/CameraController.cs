using UnityEngine;
public class CameraController : MonoBehaviour {




    public float panSpeed = 10f;
    public float panBoarderThickness = 10f;
    public Vector2 panLimit;
    public float scrollSpeed = 20f;
    public float minY = 0f;
    public float maxY = 20f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition)) {
            return;
        }


        // Store position of this object
        Vector3 pos = transform.position;
        if (Input.GetKey("p"))
        {
            Debug.Log("PRESSED P - [Move Camera position to main.]");
            pos.x = 0.0f;
            pos.y = 20.0f;
            pos.z = -10f;
        }

        if ( Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            Debug.Log("[CAMERA CONTROLLER] w key or mouse up pressed");
            pos.z += panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y <= panBoarderThickness)
        {
            Debug.Log("[CAMERA CONTROLLER] s key or mouse down pressed");
            pos.z -= panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            Debug.Log("[CAMERA CONTROLLER] d key or right side pressed");
            pos.x += panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x <= panBoarderThickness)
        {
            Debug.Log("[CAMERA CONTROLLER] a key or left side pressed");
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;


        //// Camera moving limitation.
        //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
	}



    // Currently, not using.
    private void SetXRotation(Transform t, float angle)
    {
        t.localEulerAngles = new Vector3(angle, t.localEulerAngles.y, t.localEulerAngles.z);
    }
}
