using System.Collections.Generic;

namespace console_classes_testing
{
  public abstract class SlideMovablePiece : ChessPiece
  {
    protected SlideMovablePiece(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board) { }
    protected void AddValidMovesInDirection(List<ChessLocation> moves, int rowDirection, int colDirection)
    {
      int row = Location.Row + rowDirection;
      int col = Location.Column + colDirection;

      while (ChessLocation.TryCreate(row, col, out ChessLocation targetLocation))
      {
        ChessPiece targetPiece = Board.GetPieceAtLocation(targetLocation);

        // If the target location is empty, it's a valid move
        if (targetPiece == null)
        {
          moves.Add(targetLocation);
        }
        else
        {
          // If the target location contains an opponent piece, include it and stop checking in this direction
          if (targetPiece.Color != Color)
          {
            moves.Add(targetLocation);
          }
          break;
        }

        row += rowDirection;
        col += colDirection;
      }
    }
  }
}
