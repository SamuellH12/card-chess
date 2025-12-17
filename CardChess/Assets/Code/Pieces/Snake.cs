using UnityEngine;
using System.Collections.Generic;

public class Snake : Piece {
    public override List<Cell> ListOfMoves(Board board, bool couldAtack = false){
        List<Cell> moves = new List<Cell>();
        if(frozenUntilTurn >= board.globalManager.turnCount) return moves;
        int x = cell.x;
        int y = cell.y;

        int[,] snakeMoves = new int[,]{
            {2, 0}, {-2, 0}, {0, 1}, {0, -1},
            {1, 2}, {-1, 2}, {1, -2}, {-1, -2}
        };

        for(int i = 0; i < snakeMoves.GetLength(0); i++){
            int nx = x + snakeMoves[i, 0];
            int ny = y + snakeMoves[i, 1];

            if(board.IsInsideBoard(nx, ny)){
                Cell targetCell = board.cells[nx, ny];
                if(targetCell.EmptyOrEnemy(player))  moves.Add(targetCell);
            }
        }

        return moves;
    }
    
}

/*
    . . . # . . . .  
    . # . . . # . .  
    . . # S # . . .  
    . # . . . # . .  
    . . . # . . . .  

    S = Snake's position
    # = Possible moves
    . = Empty cell
The Snake moves in an "L" shape, similar to a Knight in chess.
    It can move to any cell that is two squares in one direction and one square perpendicular to
    that direction, provided the destination cell is either empty or occupied by an enemy piece.

*/