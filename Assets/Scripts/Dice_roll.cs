using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_roll : MonoBehaviour
{
    public SpriteRenderer imagerenderer;

    public Sprite Dice1;
    public Sprite Dice2;
    public Sprite Dice3;
    public Sprite Dice4;

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

        switch (d4)
        {
        case 1: 
            Debug.Log("1");
            imagerenderer.sprite = Dice1;
            break;
        case 2: 
            Debug.Log("2");
            imagerenderer.sprite = Dice2;
            break;
        case 3: 
            Debug.Log("3");
            imagerenderer.sprite = Dice3;
            break;
        case 4: 
            Debug.Log("4");
            imagerenderer.sprite = Dice4;
            break;
        }

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
