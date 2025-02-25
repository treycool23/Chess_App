using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;
    public String type = null;

    public Sprite move, take, snipe;

    public void Activate()
    {
        switch (type)
        {
            case "move": this.GetComponent<SpriteRenderer>().sprite = move; break;
            case "take": this.GetComponent<SpriteRenderer>().sprite = take; break;
            case "snipe": this.GetComponent<SpriteRenderer>().sprite = snipe; break;
        }
    }

    public void Start()
    {
        switch (type)
        {
            case "move": this.GetComponent<SpriteRenderer>().sprite = move; break;
            case "take": this.GetComponent<SpriteRenderer>().sprite = take; break;
            case "snipe": this.GetComponent<SpriteRenderer>().sprite = snipe; break;
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Destroy the victim Chesspiece
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
        }

        if (!(type == "snipe"))
        {
            //Set the Chesspiece's original location to be empty
            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
            //Move reference chess piece to this position
            reference.GetComponent<Chessman>().SetXBoard(matrixX);
            reference.GetComponent<Chessman>().SetYBoard(matrixY);
            reference.GetComponent<Chessman>().SetCoords();
        }


        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        //Switch Current Player
        controller.GetComponent<Game>().NextTurn();

        //Destroy the move plates including self
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
