
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 10f;
    public float panBoarderThickness = 10f;
    public Vector2 panLimit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        // Store position of this object
        Vector3 pos = transform.position;
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
        

        // Camera moving limitation.
        //pos.x = Mathf.Clamp(pos.x)

        transform.position = pos;
	}
}
