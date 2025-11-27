using UnityEngine;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
    public Cell cell = null;
    public int player = 0;

    void Start(){
        
    }

    void Update(){
        
    }

    // move piece to new cell
    // (implement variations in subclasses if needed)
    public virtual void MoveToCell(Cell newCell){
        if(cell) cell.piece = null;

        cell = newCell;
        cell.piece = this;

        transform.position = new Vector3(newCell.x, newCell.y, 0);
    }

    // search for possible moves in the board
    // (to be implemented in subclasses)
    public virtual List<Cell> ListOfMoves(Board board){
        List<Cell> moves = new List<Cell>();

        // default: move for one step in any direction
        int x = cell.x, y = cell.y;
        for(int dx=-1; dx<=1; dx++){
            for(int dy=-1; dy<=1; dy++){
                if(dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if(nx >= 0 && nx < board.H && ny >= 0 && ny < board.W){
                    moves.Add(board.cells[nx, ny]);
                }
            }
        }

        return moves;
    }
}
