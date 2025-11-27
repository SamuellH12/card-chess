using UnityEngine;
using System.Collections.Generic;

public class King : Piece {
    
    // list of proibited moves for the king (moves that would place it in check)
    public override List<Cell> ListOfMoves(Board board){
        List<Cell> moves = base.ListOfMoves(board);
        List<Cell> prohibitedMoves = new List<Cell>();

        foreach(Piece piece in board.pieces){
            if(piece.player != this.player){
                List<Cell> attackCells = piece.ListOfAttacks(board);
                prohibitedMoves.AddRange(attackCells);
            }
        }
        
        // remove prohibited moves
        moves.RemoveAll(cell => prohibitedMoves.Contains(cell));
        return moves;
    }

    // test if king is on check
    public bool OnCheck(){
        Board board = this.cell.board;
        foreach(Piece piece in board.pieces){
            if(piece.player != this.player){
                List<Cell> attackCells = piece.ListOfAttacks(board);
                if(attackCells.Contains(this.cell)){
                    return true;
                }
            }
        }
        return false;
    }
    
}
