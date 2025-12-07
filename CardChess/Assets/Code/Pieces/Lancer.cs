using UnityEngine;
using System.Collections.Generic;

public class Lancer : Piece
{
    public override List<Cell> ListOfMoves(Board board)
    {
        List<Cell> moves = new List<Cell>();
        int direction = (player == 0) ? 1 : -1; // direction based on player

        int x = cell.x;
        int y = cell.y;
        int ny = y + direction;

        if(board.IsInsideBoard(x, ny)) moves.Add(board.GetCell(x, ny));

        return moves;
    }

    public override List<Cell> ListOfAttacks(Board board, bool couldAtack = false){
        List<Cell> attacks = new List<Cell>();
        int direction = (player == 0) ? 1 : -1; // direction based on player

        int x = cell.x;
        int y = cell.y;
        int ny = y + direction;
        int nny = y + 2 * direction;

        if(board.IsInsideBoard(x,  ny)) attacks.Add(board.GetCell(x,  ny));
        if(board.IsInsideBoard(x, nny)){
            if(couldAtack || board.GetCell(x, nny).HasEnemyPiece(player))
            attacks.Add(board.GetCell(x, nny));
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
