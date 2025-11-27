using UnityEngine;
using System.Collections.Generic;

public class Bishop : Piece
{
    public override List<Cell> ListOfMoves(Board board)
    {
        List<Cell> moves = new List<Cell>();
        int x = cell.x;
        int y = cell.y;

        int[,] directions = new int[,] {
            { 1, 1}, { 1,-1}, {-1, 1}, {-1,-1}
        };

        for (int d = 0; d < directions.GetLength(0); d++){
            int dx = directions[d, 0];
            int dy = directions[d, 1];
            int nx = x + dx;
            int ny = y + dy;

            while(nx >= 0 && nx < board.H && ny >= 0 && ny < board.W)
            {
                Cell targetCell = board.cells[nx, ny];
                if(targetCell.piece == null) moves.Add(targetCell);
                else {
                    if(targetCell.piece.player != player) moves.Add(targetCell);
                    break;
                }
                nx += dx;
                ny += dy;
            }
        }

        return moves;
    }
    
}
