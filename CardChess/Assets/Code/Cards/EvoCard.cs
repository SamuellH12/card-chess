using UnityEngine;
using System.Collections.Generic;

public class EvoCard : Card {
    
    void Start(){
        cardType = 1; // Action card
    }

    public override List<Cell> GetHighlightedCells(int player){
        // search in board for pieces of the player that can evolve
        List<Cell> highlightedCells = new List<Cell>();
        foreach(Piece piece in board.pieces){
            if(piece.player == player && piece.CouldEvolve()){
                highlightedCells.Add(piece.cell);
            }
        }
        return highlightedCells;
    }
}
