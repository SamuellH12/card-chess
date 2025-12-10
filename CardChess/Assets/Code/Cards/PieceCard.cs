using UnityEngine;
using System.Collections.Generic;

public class PieceCard : Card
{
    // Piece prefab to summon
    public GameObject piecePrefab;

    void Start(){
        // get the sprite renderer from the piece prefab and set as card image
        SpriteRenderer pieceSprite = piecePrefab.GetComponent<SpriteRenderer>();
        cardImage.sprite = pieceSprite.sprite;
        cardType = 0; // Piece card
    }

    public void SummonPiece(Cell cell, int player){
        board.AddPiece(piecePrefab, cell.x, cell.y, player);
    }

    public override List<Cell> GetHighlightedCells(int player){
        List<Cell> highlightedCells = new List<Cell>();
        foreach(Cell cell in board.GetEmptyCells()){ // if cell is empty and is the first or second row for the player
            if((player == 0 && cell.y <= 1) || (player == 1 && cell.y >= board.H - 2)){
                highlightedCells.Add(cell);
            }
        }
        return highlightedCells;
    }
}
