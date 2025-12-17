using UnityEngine;
using System.Collections.Generic;

public class King : Piece {
    
    // list of proibited moves for the king (moves that would place it in check)
    public override List<Cell> ListOfMoves(Board board, bool couldAtack = false){
        if(frozenUntilTurn >= board.globalManager.turnCount) return new List<Cell>();
        List<Cell> moves = base.ListOfMoves(board);
        List<Cell> prohibitedMoves = new List<Cell>();

        Debug.Log("King possible moves count: " + moves.Count);

        foreach(Piece piece in board.pieces){
            if(piece.player != this.player){
                prohibitedMoves.AddRange(piece.ListOfAttacks(board, true));
            }
        }
        
        Debug.Log("King prohibited moves count: " + prohibitedMoves.Count);

        // remove prohibited moves
        moves.RemoveAll(cell => prohibitedMoves.Contains(cell));
        
        return moves;
    }

    public override List<Cell> ListOfAttacks(Board board, bool couldAtack = false){
        if(frozenUntilTurn >= board.globalManager.turnCount) return new List<Cell>();
        if(couldAtack) return base.ListOfMoves(board);
        return ListOfMoves(board);
    }

    // test if king is on check
    public bool OnCheck(Cell testCell){
        Board board = this.cell.board;
        foreach(Piece piece in board.pieces){
            if(piece.player != this.player){
                List<Cell> attackCells = piece.ListOfAttacks(board, true);
                if(attackCells.Contains(testCell)) 
                    return true;
            }
        }
        return false;
    }
    public bool OnCheck(){ return OnCheck(this.cell); }
    
}
