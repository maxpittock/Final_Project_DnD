using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Panel_Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    //create feild for dragging entire panel
    [SerializeField]
    private RectTransform Panel;
    //canvas field - define what canvas the dragger is on
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    //background image definied 
    private Image BackgroundImage;
    //background colour allows me to change the colour when being dragged
    private Color BackgroundColour;

    private void Awake()
    {
        BackgroundColour = BackgroundImage.color;
        //makes it so if no panel is given the code still works and pnale is draggable
        if (Panel == null)
        {
            Panel = transform.parent.GetComponent<RectTransform>();
        }
        //if there is no canvas defined the code will still run - this makes it easy to add new draggable panels as i will be wokring with many 
        if (canvas == null)
        {
            Transform CanvasTransform = transform.parent;
            while (CanvasTransform != null)
            {
                canvas = CanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                CanvasTransform = CanvasTransform.parent;
            } 
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //on being dragged make the window slighty transparent
        BackgroundColour.a = .6f;
        //update colour
        BackgroundImage.color = BackgroundColour;
    }

    //called when the cursor moves when the player is dragging the mouse
    public void OnDrag(PointerEventData eventData)
    {
        //moves the panel to follow user mouse when dragged.
        Panel.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //when finished dragging set the colour back to normal
        BackgroundColour.a = 1f;
        //update the colour
        BackgroundImage.color = BackgroundColour;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Bring the panel to the front when clicked on
        Panel.SetAsLastSibling();
    }

}
