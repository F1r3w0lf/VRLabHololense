using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Spielfigur
{

   

    public override bool[,] erlaubterZug()
    {
        bool[,] r = new bool[8, 8];

        HeroMove(CurrentX + 1, CurrentY, ref r);
        HeroMove(CurrentX - 1, CurrentY, ref r);
        HeroMove(CurrentX, CurrentY - 1, ref r);
        HeroMove(CurrentX, CurrentY + 1, ref r);
        HeroMove(CurrentX + 1, CurrentY - 1, ref r);
        HeroMove(CurrentX - 1, CurrentY - 1, ref r);
        HeroMove(CurrentX + 1, CurrentY + 1, ref r);
        HeroMove(CurrentX - 1, CurrentY + 1, ref r);

        HeroMove(CurrentX + 2, CurrentY, ref r);
        HeroMove(CurrentX - 2, CurrentY, ref r);
        HeroMove(CurrentX, CurrentY - 2, ref r);
        HeroMove(CurrentX, CurrentY + 2, ref r);
        HeroMove(CurrentX + 2, CurrentY - 2, ref r);
        HeroMove(CurrentX - 2, CurrentY - 2, ref r);
        HeroMove(CurrentX + 2, CurrentY + 2, ref r);
        HeroMove(CurrentX - 2, CurrentY + 2, ref r);
        
        return r;
    }

    public void HeroMove(int x, int y, ref bool[,] r)
    {
        Spielfigur c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = SpielfeldManager.Instance.Spielfigur[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isBlue != c.isBlue)
                r[x, y] = true;
        }
    }
}


