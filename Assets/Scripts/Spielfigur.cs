using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spielfigur : MonoBehaviour
{

    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public bool isBlue;

    public void setPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] erlaubterZug()
    {
        return new bool[8,8];
    }
}
