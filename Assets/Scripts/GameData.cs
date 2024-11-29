using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife;
    [SerializeField] private int currentScene;
    [SerializeField] private int saveSlot;
    [SerializeField] private bool fireRune;
    [SerializeField] private int airRune;
    [SerializeField] private bool earthRune;

    [SerializeField] private float mana;
    [SerializeField] private float maxMana;


    [SerializeField] private Vector3 playerPos;


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

    public int SaveSlot
    {
        get { return saveSlot; }
        set { saveSlot = value; }
    }


    public float Mana
    {
        get { return mana; }
        set { mana = value; }
    }
     
    public float MaxMana
    {
        get { return maxMana; }

        set { maxMana = value; }
    }

    public bool FireRune
    {
        get { return fireRune; }
        set { fireRune = value; }
    }

    public int AirRune 
    { 
        get { return airRune; }
        set { airRune = value; }
    }     

    public bool EarthRune
    {
        get { return earthRune; }
        set { earthRune = value; }
    }
}
