using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//attribute for custom button
[CustomEditor(typeof(AbstractDungeonGen), true)]

public class RandomDungeonGenEditor : Editor
{
    //Access reference using field "generator"
    AbstractDungeonGen generator;
    
    //
    private void Awake()
    {
        //reference to generator for custom inspector
        generator = (AbstractDungeonGen)target;
    }


    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        //creates custom button in the inspector with name "create dungeon"
        if(GUILayout.Button("Create Dungeon"))
        {
            //If button pressed call the function from abstarct class which calls "RunProceduralGeneration" function and generate a map using the random walk algorithm.
            generator.GenerateDungeon();
        }
    }
}
