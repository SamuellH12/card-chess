using UnityEngine;
using System.Collections.Generic;

// clear all enemy cards from hand
public class FrozenCard : Card {
    
    public int frozenTurns = 3;

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
        if(cell.IsEmpty()) return;
        cell.piece.FrozenPiece(board.GetTurn() + frozenTurns);
    }
}
