﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielfeldManager : MonoBehaviour {

    public static SpielfeldManager Instance { set; get; }
    private bool [,] allowedMoves { set; get; }

    public Spielfigur[,] Spielfigur { set; get; }
    private Spielfigur SelectedSpielfigur;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> SpielfigurPrefab;
    private List<GameObject> activeSpielfigur = new List<GameObject> ();

    private Quaternion orientationBlue = Quaternion.Euler(0, 0, 0);
    private Quaternion orientationRed = Quaternion.Euler(0, 180, 0);

    public bool isBlaueRunde = true;

    private void Start()
    {
        Instance = this;
        SpawnAllSpielfiguren();
    }

    private void Update()
    {
        UpdateSelection();
        DrawSpielfeld();

        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
                if(SelectedSpielfigur == null)
                {
                    SelectSpielfigur(selectionX, selectionY);
                }
                else
                {
                    moveSpielfigur(selectionX, selectionY);
                }
            }
        }
    }

    //Spielfigur für den Zug auswählen
    private void SelectSpielfigur(int x, int y)
    {
        //Wenn auf dem ausgewählten Feld keine Figur steht, nichts machen
        if (Spielfigur[x, y] == null)
            return;
        
        //Wenn auf dem ausgewählten Feld eine Figur steht, die aber nicht dran ist, nichts machen
        if(Spielfigur[x,y].isBlue!= isBlaueRunde)
            return;

        //Wenn auf dem ausgewählten Feld eine Figur steht und die dran ist, schauen welche Züge erlaubt sind
        allowedMoves = Spielfigur[x, y].erlaubterZug();

        //Die ausgewählte Figur als "SelectedSpielfigur" wählen
        SelectedSpielfigur = Spielfigur[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void moveSpielfigur(int x, int y )
    {
        if (allowedMoves[x,y])
        {
            Spielfigur c = Spielfigur[x, y];

            if(c != null && c.isBlue != isBlaueRunde)
            {

                kampf(c, SelectedSpielfigur);

            } else if(c == null)
            {
                Spielfigur[SelectedSpielfigur.CurrentX, SelectedSpielfigur.CurrentY] = null;
                SelectedSpielfigur.transform.position = GetTileCenter(x, y);
                SelectedSpielfigur.setPosition(x, y);
                Spielfigur[x, y] = SelectedSpielfigur;
            }
           


            isBlaueRunde = !isBlaueRunde;
        }

        BoardHighlights.Instance.HideHighlights();

        SelectedSpielfigur = null;
    }

    private void kampf(Spielfigur def, Spielfigur att)
    {
        Debug.Log(def.GetInstanceID() + ": Leben: " + def.health + " Angriff: " + def.attack + " Verteidigung: " + def.defense);
        Debug.Log(att.GetInstanceID() + ": Leben: " + att.health + " Angriff: " + att.attack + " Verteidigung: " + att.defense);
        System.Random rnd= new System.Random();



        int defensive = def.defense; //rnd.Next(1, def.defense);

        Debug.Log("Der Verteidiger würfelt mit einem 1W" + def.defense +" Würfel und bekommt eine: " + defensive);

        int angriff = att.attack; //rnd.Next(1, att.attack);

        Debug.Log("Der Angreifer würfelt mit einem 1W" + att.attack + " Würfel und bekommt eine: " + angriff);

        int ergebnis = defensive - angriff;

          if (ergebnis >= 0)
        {
          Debug.Log("Das Ergebnis ist: "+ ergebnis +" somit hat der Verteidiger gewonnen");
            return;
        }
        else
        {
          def.health = def.health - Mathf.Abs(ergebnis);
          Debug.Log("Das Ergebnis ist: " + ergebnis + " somit hat der Angreifer gewonnen gewonnen und der Vertediger verliert: " + ergebnis);
        }

        if(def.health <= 0)
        {
            
            Debug.Log("Verteidiger ist gestorben");
            Debug.Log(def.GetInstanceID() + "wird aus dem spielt entfernt");
            activeSpielfigur.Remove(def.gameObject);
            Destroy(def.gameObject);
        }
        else
        {
            Debug.Log("Verteidiger hat noch: " + def.health + " HP");
            return;
        }

        

        
        
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("SpielfeldPlane"))){
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }else{
            selectionX = -1;
            selectionY = -1;
        }

       
		
			
    }

    private void SpawnSpielfigurBlue(int index, int x, int y)
    {
        GameObject go = Instantiate(SpielfigurPrefab[index], GetTileCenter(x,y), orientationBlue) as GameObject;
        go.transform.SetParent(transform);
        Spielfigur[x, y] = go.GetComponent<Spielfigur>();
        Spielfigur[x, y].setPosition(x, y);
        activeSpielfigur.Add(go);
    }

    private void SpawnSpielfigurRed(int index, int x, int y)
    {
        GameObject go = Instantiate(SpielfigurPrefab[index], GetTileCenter(x, y), orientationRed) as GameObject;
        go.transform.SetParent(transform);
        Spielfigur[x, y] = go.GetComponent<Spielfigur>();
        Spielfigur[x, y].setPosition(x, y);
        activeSpielfigur.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void SpawnAllSpielfiguren()
    {
        activeSpielfigur = new List<GameObject>();
        Spielfigur = new Spielfigur[8,8];

        //Spielfiguren von Team Blau Spawnen
        SpawnSpielfigurBlue(0, 2, 0);
        SpawnSpielfigurBlue(0, 3, 0);
        SpawnSpielfigurBlue(0, 4, 0);
        SpawnSpielfigurBlue(0, 5, 0);
        SpawnSpielfigurBlue(2, 6, 0);


        //Spielfiguren von Team Rot Spawnen

        SpawnSpielfigurRed(1, 2, 7);
        SpawnSpielfigurRed(1, 3, 7);
        SpawnSpielfigurRed(1, 4, 7);
        SpawnSpielfigurRed(1, 5, 7);
        SpawnSpielfigurRed(3, 1, 7);

    }

    private void DrawSpielfeld()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            //Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                //Debug.DrawLine(start, start + heightLine);
            }
        }

        //Die Auswahl zeichnen
        if(selectionX>=0 && selectionY>= 0)
        {
            //Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            //Debug.DrawLine(Vector3.forward * (selectionY + 1) + Vector3.right * selectionX, Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
}
