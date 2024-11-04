using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    private float life;
    private float maxLife;
    private int currentScene;
    private Vector3 playerPos;

    public float minLife;

    public float Life
    {
        get{ return life; }
        set{ life = value; }
    }

    public float MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public int CurrentScene
    {
        get { return currentScene; }
        set { currentScene = value; }
    }
    public Vector3 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
}
