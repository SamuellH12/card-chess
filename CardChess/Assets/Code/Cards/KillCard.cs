using UnityEngine;
using System.Collections.Generic;

// clear all enemy cards from hand
public class KillCard : Card {
    
    void Start(){
        cardType = 3; // Action card
    }

    public override List<Cell> GetHighlightedCells(int player){
        List<Cell> highlightedCells = new List<Cell>();

        // get cells where enemy pieces are located except kings
        int enemyPlayer = 1 - player;
        foreach(Piece piece in board.pieces){
            if(piece.player == enemyPlayer && piece.pieceType != "King"){
                highlightedCells.Add(piece.cell);
            }
        }
        
        return highlightedCells;
    }

    public override void UseCard(int player, Cell cell, CardManager cardManager){
        board.RemovePiece(cell.piece);
    }
}
