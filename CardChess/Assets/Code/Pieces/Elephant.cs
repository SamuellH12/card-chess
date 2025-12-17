using UnityEngine;
using System.Collections.Generic;

public class Elephant : Piece
{
    public override List<Cell> ListOfMoves(Board board, bool couldAtack = false){
        List<Cell> moves = new List<Cell>();
        if(frozenUntilTurn >= board.globalManager.turnCount) return moves;
        int x = cell.x;
        int y = cell.y;

        // Can move to any cell with manhattan distance of 1 or 2 in a bfs manner
        int[,] directions = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        
        Queue<Cell> queue = new Queue<Cell>();
        HashSet<Cell> visited = new HashSet<Cell>();
        
        queue.Enqueue(cell);
        visited.Add(cell);

        int distance = 0;
        while(queue.Count > 0 && distance < 2){
            int levelSize = queue.Count;
            distance++;
            
            for(int i = 0; i < levelSize; i++){
                Cell current = queue.Dequeue();

                for (int d = 0; d < directions.GetLength(0); d++){
                    int nx = current.x + directions[d, 0];
                    int ny = current.y + directions[d, 1];
                    
                    if (board.IsInsideBoard(nx, ny)){
                        Cell neighbor = board.cells[nx, ny];
                        if(visited.Contains(neighbor)) continue;
                        visited.Add(neighbor);

                        if(neighbor.IsEmpty() || (couldAtack && neighbor.piece.pieceType == "King")){
                            moves.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                        else 
                        if(neighbor.HasEnemyPiece(player) || couldAtack) moves.Add(neighbor);
                    }
                }
            }
        }

        return moves;
    }
    
}
