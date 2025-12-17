using UnityEngine;
using System.Collections.Generic;

public class Knight : Piece
{
    public override List<Cell> ListOfMoves(Board board, bool couldAtack = false){
        List<Cell> moves = new List<Cell>();
        if(frozenUntilTurn >= board.globalManager.turnCount) return moves;
        int x = cell.x;
        int y = cell.y;

        int[,] knightMoves = new int[,] {
            { 2, 1}, { 2,-1}, {-2, 1}, {-2,-1},
            { 1, 2}, { 1,-2}, {-1, 2}, {-1,-2}
        };

        for(int i = 0; i < knightMoves.GetLength(0); i++){
            int nx = x + knightMoves[i, 0];
            int ny = y + knightMoves[i, 1];
            
            if(board.IsInsideBoard(nx, ny)){
                Cell targetCell = board.cells[nx, ny];
                if(targetCell.EmptyOrEnemy(player))  moves.Add(targetCell);
            }
        }

        return moves;
    }
    
}
