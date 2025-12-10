using UnityEngine;
using System.Collections.Generic;

// teleport the king to an safe cell
public class KingTeleportCard : Card {
    
    void Start(){
        cardType = 4; // Action card
    }

    public override List<Cell> GetHighlightedCells(int player){
        King kingPiece = (King)board.GetKing(player);
        if(kingPiece == null) return new List<Cell>();
        
        List<Cell> highlightedCells = new List<Cell>();

        // get all empty cells that are not under attack
        foreach(Cell cell in board.cells)
            if(cell.IsEmpty() && kingPiece.OnCheck(cell) == false)
                highlightedCells.Add(cell);
        
        return highlightedCells;
    }

    public override void UseCard(int player, Cell cell, CardManager cardManager){
        King kingPiece = (King)board.GetKing(player);
        if(kingPiece == null) return;
        kingPiece.MoveToCell(cell);
    }
}
