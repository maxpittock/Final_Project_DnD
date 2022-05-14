using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice_roll : MonoBehaviour
{
    //create image vriable for defining the UI image
    public Image imagerenderer;
    
    //create List of sprites to use and switch between
    public Sprite[] DieList;
    
    //set up variable for storing the random generated number
    int d4;
    int d6;
    int d8;
    int d10;
    int d12;
    int d20;
    int d100;

    
    //change sprite function
    public void ChangeSprite(Sprite CurrentRoll)
    {   
        //changes the image sprite to the variable called att runtime
        imagerenderer.sprite = CurrentRoll;
    }

    
    public void D4()
    {
        //gets random number between range into d4 variable
        d4 = Random.Range(1,5);
        //log the number tthats being generated for testign purposes
        Debug.Log(d4);
        //call custom change image function and hand it random dice variable as the parameter
        ChangeImage(d4);
    }
    public void D6()
    {
        d6 = Random.Range(1,7);
        Debug.Log(d6);

        ChangeImage(d6);
    }
    public void D8()
    {
        d8 = Random.Range(1,9);
        Debug.Log(d8);

        ChangeImage(d8);
    }
    public void D10()
    {
        d10 = Random.Range(1,11);
        Debug.Log(d10);

        ChangeImage(d10);
    }
    public void D12()
    {
        d12 = Random.Range(1,13);
        Debug.Log(d12);

        ChangeImage(d12);
    }
    public void D20()
    {
        d20 = Random.Range(1,21);
        Debug.Log(d20);

        ChangeImage(d20);
    }
    public void D100()
    {
        d100 = Random.Range(1,101);
        Debug.Log(d100);

        ChangeImage(d100);
    }

    //change image function - takes dice integer as input when called 
    //This allows it to be called in the different die function and applied to the indivdual dice without repeating code
    public void ChangeImage(int DiceOutcome)
    {
        //switch statement take the parameter integer and use it to know which number has been rolled and which sprite to display to the UI image.
        switch (DiceOutcome)
            {
            case 1: 
            //log that a 1 has been rolled
                Debug.Log("1");
                //change the image UI sprite to the array item specfiied 
                imagerenderer.sprite = DieList[0];
                //break the current case
                break;
            case 2: 
                Debug.Log("2");
                imagerenderer.sprite = DieList[1];
                break;
            case 3: 
                Debug.Log("3");
                imagerenderer.sprite = DieList[2];
                break;
            case 4: 
                Debug.Log("4");
                imagerenderer.sprite = DieList[3];
                break;
            case 5: 
                Debug.Log("5");
                imagerenderer.sprite = DieList[4];
                break;
            case 6: 
                Debug.Log("6");
                imagerenderer.sprite = DieList[5];
                break;
            case 7: 
                Debug.Log("7");
                imagerenderer.sprite = DieList[6];
                break;
            case 8: 
                Debug.Log("8");
                imagerenderer.sprite = DieList[7];
                break;
            case 9: 
                Debug.Log("9");
                imagerenderer.sprite = DieList[8];
                break;
            case 10: 
                Debug.Log("10");
                imagerenderer.sprite = DieList[9];
                break;
            case 11: 
                Debug.Log("1");
                imagerenderer.sprite = DieList[10];
                break;
            case 12: 
                Debug.Log("2");
                imagerenderer.sprite = DieList[11];
                break;
            case 13: 
                Debug.Log("3");
                imagerenderer.sprite = DieList[12];
                break;
            case 14: 
                Debug.Log("4");
                imagerenderer.sprite = DieList[13];
                break;
            case 15: 
                Debug.Log("5");
                imagerenderer.sprite = DieList[14];
                break;
            case 16: 
                Debug.Log("6");
                imagerenderer.sprite = DieList[15];
                break;
            case 17: 
                Debug.Log("7");
                imagerenderer.sprite = DieList[16];
                break;
            case 18: 
                Debug.Log("8");
                imagerenderer.sprite = DieList[17];
                break;
            case 19: 
                Debug.Log("9");
                imagerenderer.sprite = DieList[18];
                break;
            case 20: 
                Debug.Log("10");
                imagerenderer.sprite = DieList[19];
                break;  
            case 21: 
                Debug.Log("21");
                imagerenderer.sprite = DieList[20];
                break;
            case 22: 
                Debug.Log("22");
                imagerenderer.sprite = DieList[21];
                break;
            case 23: 
                Debug.Log("23");
                imagerenderer.sprite = DieList[22];
                break;
            case 24: 
                Debug.Log("24");
                imagerenderer.sprite = DieList[23];
                break;
            case 25: 
                Debug.Log("25");
                imagerenderer.sprite = DieList[24];
                break;
            case 26: 
                Debug.Log("26");
                imagerenderer.sprite = DieList[25];
                break;
            case 27: 
                Debug.Log("27");
                imagerenderer.sprite = DieList[26];
                break;
            case 28: 
                Debug.Log("28");
                imagerenderer.sprite = DieList[27];
                break;
            case 29: 
                Debug.Log("29");
                imagerenderer.sprite = DieList[28];
                break;
            case 30: 
                Debug.Log("30");
                imagerenderer.sprite = DieList[29];
                break;
            case 31: 
                Debug.Log("31");
                imagerenderer.sprite = DieList[30];
                break;
            case 32: 
                Debug.Log("32");
                imagerenderer.sprite = DieList[31];
                break;
            case 33: 
                Debug.Log("33");
                imagerenderer.sprite = DieList[32];
                break;
            case 34: 
                Debug.Log("34");
                imagerenderer.sprite = DieList[33];
                break;
            case 35: 
                Debug.Log("35");
                imagerenderer.sprite = DieList[34];
                break;
            case 36: 
                Debug.Log("36");
                imagerenderer.sprite = DieList[35];
                break;
            case 37: 
                Debug.Log("37");
                imagerenderer.sprite = DieList[36];
                break;
            case 38: 
                Debug.Log("38");
                imagerenderer.sprite = DieList[37];
                break;
            case 39: 
                Debug.Log("39");
                imagerenderer.sprite = DieList[38];
                break;
            case 40: 
                Debug.Log("40");
                imagerenderer.sprite = DieList[39];
                break;  
            case 41: 
                Debug.Log("41");
                imagerenderer.sprite = DieList[40];
                break;
            case 42: 
                Debug.Log("42");
                imagerenderer.sprite = DieList[41];
                break;
            case 43: 
                Debug.Log("43");
                imagerenderer.sprite = DieList[42];
                break;
            case 44:
                Debug.Log("44");
                imagerenderer.sprite = DieList[43];
                break;
            case 45: 
                Debug.Log("45");
                imagerenderer.sprite = DieList[44];
                break;
            case 46: 
                Debug.Log("46");
                imagerenderer.sprite = DieList[45];
                break;
            case 47: 
                Debug.Log("47");
                imagerenderer.sprite = DieList[46];
                break;
            case 48: 
                Debug.Log("48");
                imagerenderer.sprite = DieList[47];
                break;
            case 49: 
                Debug.Log("49");
                imagerenderer.sprite = DieList[48];
                break;
            case 50: 
                Debug.Log("50");
                imagerenderer.sprite = DieList[49];
                break;
            case 51: 
                Debug.Log("51");
                imagerenderer.sprite = DieList[50];
                break;
            case 52: 
                Debug.Log("52");
                imagerenderer.sprite = DieList[51];
                break;
            case 53: 
                Debug.Log("53");
                imagerenderer.sprite = DieList[52];
                break;
            case 54: 
                Debug.Log("54");
                imagerenderer.sprite = DieList[53];
                break;
            case 55: 
                Debug.Log("55");
                imagerenderer.sprite = DieList[54];
                break;
            case 56: 
                Debug.Log("56");
                imagerenderer.sprite = DieList[55];
                break;
            case 57: 
                Debug.Log("57");
                imagerenderer.sprite = DieList[56];
                break;
            case 58: 
                Debug.Log("58");
                imagerenderer.sprite = DieList[57];
                break;
            case 59: 
                Debug.Log("59");
                imagerenderer.sprite = DieList[58];
                break;
            case 60: 
                Debug.Log("60");
                imagerenderer.sprite = DieList[59];
                break;  
            case 61: 
                Debug.Log("61");
                imagerenderer.sprite = DieList[60];
                break;
            case 62: 
                Debug.Log("62");
                imagerenderer.sprite = DieList[61];
                break;
            case 63: 
                Debug.Log("63");
                imagerenderer.sprite = DieList[62];
                break;
            case 64: 
                Debug.Log("64");
                imagerenderer.sprite = DieList[63];
                break;
            case 65: 
                Debug.Log("65");
                imagerenderer.sprite = DieList[64];
                break;
            case 66: 
                Debug.Log("66");
                imagerenderer.sprite = DieList[65];
                break;
            case 67: 
                Debug.Log("67");
                imagerenderer.sprite = DieList[66];
                break;
            case 68: 
                Debug.Log("68");
                imagerenderer.sprite = DieList[67];
                break;
            case 69: 
                Debug.Log("69");
                imagerenderer.sprite = DieList[68];
                break;
            case 70: 
                Debug.Log("70");
                imagerenderer.sprite = DieList[69];
                break;
            case 71: 
                Debug.Log("71");
                imagerenderer.sprite = DieList[70];
                break;
            case 72: 
                Debug.Log("72");
                imagerenderer.sprite = DieList[71];
                break;
            case 73: 
                Debug.Log("73");
                imagerenderer.sprite = DieList[72];
                break;
            case 74: 
                Debug.Log("74");
                imagerenderer.sprite = DieList[73];
                break;
            case 75: 
                Debug.Log("75");
                imagerenderer.sprite = DieList[74];
                break;
            case 76: 
                Debug.Log("76");
                imagerenderer.sprite = DieList[75];
                break;
            case 77: 
                Debug.Log("77");
                imagerenderer.sprite = DieList[76];
                break;
            case 78: 
                Debug.Log("78");
                imagerenderer.sprite = DieList[77];
                break;
            case 79: 
                Debug.Log("79");
                imagerenderer.sprite = DieList[78];
                break;
            case 80: 
                Debug.Log("80");
                imagerenderer.sprite = DieList[79];
                break;  
            case 81: 
                Debug.Log("81");
                imagerenderer.sprite = DieList[80];
                break;
            case 82: 
                Debug.Log("82");
                imagerenderer.sprite = DieList[81];
                break;
            case 83: 
                Debug.Log("83");
                imagerenderer.sprite = DieList[82];
                break;
            case 84: 
                Debug.Log("84");
                imagerenderer.sprite = DieList[83];
                break;
            case 85: 
                Debug.Log("85");
                imagerenderer.sprite = DieList[84];
                break;
            case 86: 
                Debug.Log("86");
                imagerenderer.sprite = DieList[85];
                break;
            case 87: 
                Debug.Log("87");
                imagerenderer.sprite = DieList[86];
                break;
            case 88: 
                Debug.Log("88");
                imagerenderer.sprite = DieList[87];
                break;
            case 89: 
                Debug.Log("89");
                imagerenderer.sprite = DieList[88];
                break;
            case 90: 
                Debug.Log("90");
                imagerenderer.sprite = DieList[89];
                break;
            case 91: 
                Debug.Log("91");
                imagerenderer.sprite = DieList[90];
                break;
            case 92: 
                Debug.Log("92");
                imagerenderer.sprite = DieList[91];
                break;
            case 93: 
                Debug.Log("93");
                imagerenderer.sprite = DieList[92];
                break;
            case 94: 
                Debug.Log("94");
                imagerenderer.sprite = DieList[93];
                break;
            case 95: 
                Debug.Log("95");
                imagerenderer.sprite = DieList[94];
                break;
            case 96: 
                Debug.Log("96");
                imagerenderer.sprite = DieList[95];
                break;
            case 97: 
                Debug.Log("97");
                imagerenderer.sprite = DieList[96];
                break;
            case 98: 
                Debug.Log("98");
                imagerenderer.sprite = DieList[97];
                break;
            case 99: 
                Debug.Log("99");
                imagerenderer.sprite = DieList[98];
                break;
            case 100: 
                Debug.Log("100");
                imagerenderer.sprite = DieList[99];
                break;  


    }

    }
}
