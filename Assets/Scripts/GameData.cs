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

    public bool FireRune
    {
        get { return fireRune; }
        set { fireRune = value; }
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
}
