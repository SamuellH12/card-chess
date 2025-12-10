using UnityEngine;
using System.Collections.Generic;

// discard [amountToClear] cards from enemy hand
public class ClearCard : Card {
    public int amountToClear = -1; // -1 means all cards
    
    void Start(){
        cardType = 2; // Action card
    }

    public override List<Cell> GetHighlightedCells(int player){
        List<Cell> highlightedCells = new List<Cell>();

        // get enemy player cells first two rows (confirm use)
        int enemyPlayer = 1 - player;
        for(int x = 0; x < board.W; x++){
            for(int y = 0; y < 2; y++){
                int cellY = enemyPlayer == 0 ? y : board.H - 1 - y;
                highlightedCells.Add(board.GetCell(x, cellY));
            }
        }
        
        return highlightedCells;
    }
    public override void UseCard(int player, Cell cell, CardManager cardManager){
        cardManager.RemoveFromHand(1-player, amountToClear);
    }
}
