using UnityEngine;
using System.Collections.Generic;

public class Shield : Piece
{
    public override List<Cell> ListOfMoves(Board board, bool couldAtack = false){
        List<Cell> moves = new List<Cell>();
        if(frozenUntilTurn >= board.globalManager.turnCount) return moves;
        int x = cell.x;
        int y = cell.y;
        int direction = (player == 0) ? 1 : -1;
        
        int ny = y + direction;
        int nx1 = x - 1;
        int nx2 = x + 1;

        if(board.IsInsideBoard(x,   ny) && board.cells[x,   ny].EmptyOrEnemy(player)) 
            moves.Add(board.cells[x, ny]);
        
        if(board.IsInsideBoard(nx1, ny) && board.cells[nx1, ny].EmptyOrEnemy(player)) 
            moves.Add(board.cells[nx1, ny]);
        
        if(board.IsInsideBoard(nx2, ny) && board.cells[nx2, ny].EmptyOrEnemy(player)) 
            moves.Add(board.cells[nx2, ny]);

        if(board.IsInsideBoard(nx1,  y) && board.cells[nx1,  y].EmptyOrEnemy(player)) 
            moves.Add(board.cells[nx1, y]);
        
        if(board.IsInsideBoard(nx2,  y) && board.cells[nx2,  y].EmptyOrEnemy(player))
            moves.Add(board.cells[nx2, y]);

        return moves;
    }

    public override bool CanEvolve(){ // Rook or Elephant
        if(cell == null) return false;
        if(player == 0 && cell.canEvolveWhite) return true;
        if(player == 1 && cell.canEvolveBlack) return true;
        return false;
    }
}
