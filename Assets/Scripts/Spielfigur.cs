using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spielfigur : MonoBehaviour
{



    public int CurrentX { set; get; }
    public int CurrentY { set; get; }



    public bool isBlue;
    public int health;
    public int attack;
    public int defense;
    public int movement;




    public void setPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

   

    public virtual bool[,] erlaubterZug()
    {
        return new bool[8,8];
    }


    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public int Defense
    {
        get
        {
            return defense;
        }

        set
        {
            defense = value;
        }
    }

    public int Movement
    {
        get
        {
            return movement;
        }

        set
        {
            movement = value;
        }
    }

}
