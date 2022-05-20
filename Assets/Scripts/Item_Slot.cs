using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Slot : MonoBehaviour, IDropHandler
{

    [SerializeField]
    private Canvas canvas;
    
    // Start is called before the first frame update
    //called when the cursor moves when the player is dragging the mouse
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            
        }
        
    }
}
