using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Spielfigur {

	public override bool[,] erlaubterZug()
    {
        bool[,] r = new bool[8, 8];

        Spielfigur c, c2;
        c = SpielfeldManager.Instance.Spielfigur[CurrentX + 1, CurrentY + 1];
        c2 = SpielfeldManager.Instance.Spielfigur[CurrentX + 1, CurrentY];
        if (c == null && c2 == null)
            r[CurrentX, CurrentY] = true;


        return r;
    }
		
	
}
