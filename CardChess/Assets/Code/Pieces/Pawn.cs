using UnityEngine;
using System.Collections.Generic;

public class Pawn : Piece
{

    public override List<Cell> ListOfMoves(Board board)
    {
        List<Cell> moves = new List<Cell>();
        int direction = (player == 0) ? 1 : -1; // direction based on player

        int x = cell.x;
        int y = cell.y;
        int ny = y + direction;

        // move forward
        if(board.IsInsideBoard(x, ny) && board.cells[x, ny].piece == null)
        {
            moves.Add(board.cells[x, ny]);

            // first move can be two steps
            int nny = y + 2 * direction;
            if(!hasMoved && board.IsInsideBoard(x, nny) && board.cells[x, nny].piece == null)
                moves.Add(board.cells[x, nny]);
        }

        return moves;
    }

    public override List<Cell> ListOfAttacks(Board board)
    {
        List<Cell> attacks = new List<Cell>();
        int direction = (player == 0) ? 1 : -1; // direction based on player

        int x = cell.x;
        int y = cell.y;
        int ny = y + direction;

        // attack diagonally
        for (int dx = -1; dx <= 1; dx += 2)
        {
            int nx = x + dx;
            if (nx >= 0 && nx < board.H && ny >= 0 && ny < board.W)
            {
                Cell targetCell = board.cells[nx, ny];
                if (targetCell.piece != null && targetCell.piece.player != player)
                {
                    attacks.Add(targetCell);
                }
            }
        }

        return attacks;
    }

    public override bool CanEvolve(){
        if(cell == null) return false;

        if(player == 0 && cell.canEvolveWhite) return true;
        if(player == 1 && cell.canEvolveBlack) return true;

        return false;
    }
}
