using UnityEngine;
using System.Collections.Generic;

public class Rook : Piece
{
    public override List<Cell> ListOfMoves(Board board, bool couldAtack = false){
        List<Cell> moves = new List<Cell>();
        if(frozenUntilTurn >= board.globalManager.turnCount) return moves;
        int x = cell.x;
        int y = cell.y;

        int[,] directions = new int[,] {
            { 1, 0}, {-1, 0}, {0, 1}, {0, -1}
        };

        for (int d = 0; d < directions.GetLength(0); d++){
            int dx = directions[d, 0];
            int dy = directions[d, 1];
            int nx = x + dx;
            int ny = y + dy;

            while(nx >= 0 && nx < board.H && ny >= 0 && ny < board.W)
            {
                Cell targetCell = board.cells[nx, ny];
                if(targetCell.IsEmpty()) moves.Add(targetCell);
                else
                if(couldAtack && targetCell.piece.pieceType == "King") moves.Add(targetCell);
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
