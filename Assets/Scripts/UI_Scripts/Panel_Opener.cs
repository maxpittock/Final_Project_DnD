using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Opener : MonoBehaviour
{
    public GameObject Panel;
    public bool IsOn;

    public void Start()
    {
        IsOn = false;
        Panel.SetActive(false);
    }


    public void OpenPanel()
    {
        if (IsOn == false)
        {
            Panel.SetActive(true);
            IsOn = true;
        }
        else
        {
            Panel.SetActive(false);
            IsOn = false;
        }
    }
}
