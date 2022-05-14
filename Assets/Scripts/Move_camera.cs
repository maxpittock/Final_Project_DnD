using UnityEngine;

public class Move_camera : MonoBehaviour
{
    //set the move speed for the camera
    public float Camera_move_speed = 20f;
    //sets the border for movement with mouse
    public float Border_Thickness = 10f;
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
        //set current positioon = to our new position
        transform.position = pos;
    }
}
