using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_roll : MonoBehaviour
{
    int d4;
    int d6;
    int d8;
    int d10;
    int d12;
    int d20;
    int d100;

    public void D4()
    {
        d4 = Random.Range(1,4);
        Debug.Log(d4);
    }
    public void D6()
    {
        d6 = Random.Range(1,6);
        Debug.Log(d6);
    }
    public void D8()
    {
        d8 = Random.Range(1,8);
        Debug.Log(d8);
    }
    public void D10()
    {
        d10 = Random.Range(1,10);
        Debug.Log(d10);
    }
    public void D12()
    {
        d12 = Random.Range(1,12);
        Debug.Log(d12);
    }
    public void D20()
    {
        d20 = Random.Range(1,20);
        Debug.Log(d20);
    }
    public void D100()
    {
        d100 = Random.Range(1,100);
        Debug.Log(d100);
    }
}
