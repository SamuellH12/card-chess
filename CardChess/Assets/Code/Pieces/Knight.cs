using UnityEngine;
using System.Collections.Generic;

public class Knight : Piece
{
    public override List<Cell> ListOfMoves(Board board)
    {
        List<Cell> moves = new List<Cell>();
        int x = cell.x;
        int y = cell.y;

        int[,] knightMoves = new int[,] {
            { 2, 1}, { 2,-1}, {-2, 1}, {-2,-1},
            { 1, 2}, { 1,-2}, {-1, 2}, {-1,-2}
        };

        for(int i = 0; i < knightMoves.GetLength(0); i++){
            int nx = x + knightMoves[i, 0];
            int ny = y + knightMoves[i, 1];

            if(nx >= 0 && nx < board.H && ny >= 0 && ny < board.W){
                Cell targetCell = board.cells[nx, ny];
                if(targetCell.piece == null || targetCell.piece.player != player) 
                    moves.Add(targetCell);
            }
        }

        return moves;
    }
    
}
