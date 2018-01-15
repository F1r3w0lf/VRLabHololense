using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Spielfigur
{


    private int health { get; set; }
    private int attack { get; set; }
    private int defense { get; set; }
    private int movement { get; set; }


    public override bool[,] erlaubterZug()
    {
        bool[,] r = new bool[8, 8];

        KingMove(CurrentX + 1, CurrentY, ref r);
        KingMove(CurrentX -1, CurrentY, ref r);
        KingMove(CurrentX, CurrentY -1, ref r);
        KingMove(CurrentX, CurrentY +1, ref r);
        KingMove(CurrentX +1, CurrentY - 1, ref r);
        KingMove(CurrentX - 1, CurrentY - 1, ref r);
        KingMove(CurrentX + 1, CurrentY + 1, ref r);
        KingMove(CurrentX - 1, CurrentY + 1, ref r);
        return r;
    }

    public void KingMove(int x, int y, ref bool[,] r)
    {
        Spielfigur c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = SpielfeldManager.Instance.Spielfigur[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isBlue != c.isBlue)
                r[x, y] = true; } }
}
		
	

