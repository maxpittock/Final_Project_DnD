
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Items : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;
    private RectTransform player1;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        player1 = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        
    }


    //called when the cursor moves when the player is dragging the mouse
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        //moving the item  - divide by scale factor to line up with mopuse perfectly
        player1.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
    }

    //positions constraints
        //lock prefab position on end drag.

}
