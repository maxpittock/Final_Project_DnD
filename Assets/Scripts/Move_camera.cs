using UnityEngine;

public class Move_camera : MonoBehaviour
{
    //set the move speed for the camera
    public float Camera_move_speed = 20f;
    //sets the border for movement with mouse
    public float Border_Thickness = 10f;
    //create a vector2 variable to store the max x and y variables for the camera movement
    public Vector2 View_Limit;
    //create variable for zooming - float to be safe
    private float zoomspeed = 10; 
    //setup camera variable for zoom
    public Camera cam;
    // Update is called once per frame
    void Update()
    {
        //get the current postition as temp variable
        Vector3 pos = transform.position;
        //|| Input.mousePosition.y >= Screen.height - Border_Thickness
        //if the w key is pressed or the mouse gets to close to the edge of the screen
        if (Input.GetKey("w") )
        {
           //want to move up so use y - multiply by deltatime so whatever framerate we are running at we move the same speed. 
            pos.y += Camera_move_speed * Time.deltaTime;
        } 
        //|| Input.mousePosition.y <= Border_Thickness
        if (Input.GetKey("s") )
        {
          
            pos.y -= Camera_move_speed * Time.deltaTime;
        } 
        //|| Input.mousePosition.x <= Border_Thickness
        if (Input.GetKey("a") )
        {
     
            pos.x -= Camera_move_speed * Time.deltaTime;
        } 
        //|| Input.mousePosition.x >= Screen.width - Border_Thickness
        if (Input.GetKey("d") )
        {
          
            pos.x += Camera_move_speed * Time.deltaTime;
        } 

        //limits the camer movement with a min and max
        pos.x = Mathf.Clamp(pos.x, -View_Limit.x, View_Limit.x);
        pos.y = Mathf.Clamp(pos.y, -View_Limit.y, View_Limit.y);

        //zooming 

        //checks what type the camera is and applies the correct zoom code accordingly
        if(cam.orthographic)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomspeed;
        }
        else
        {
            cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomspeed;
        }

        //set current positioon = to our new position
        transform.position = pos;
    }
}
