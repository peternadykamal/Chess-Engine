using System.Collections.Generic;

namespace console_classes_testing
{
  public class Knight : ChessPiece
  {
    public Knight(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board)
    {
      Symbol = 'N';
    }

    public override List<ChessLocation> GetValidMoves()
    {
      List<ChessLocation> validMoves = new List<ChessLocation>();

      // Define the possible move offsets for a knight
      int[] rowOffsets = { 1, 2, 2, 1, -1, -2, -2, -1 };
      int[] colOffsets = { 2, 1, -1, -2, -2, -1, 1, 2 };

      for (int i = 0; i < rowOffsets.Length; i++)
      {
        int newRow = Location.Row + rowOffsets[i];
        int newCol = Location.Column + colOffsets[i];

        // Check if the target location is within the board boundaries
        if (ChessLocation.TryCreate(newRow, newCol, out ChessLocation targetLocation))
        {
          ChessPiece targetPiece = Board.GetPieceAtLocation(targetLocation);

          // Check if the target location is empty or contains an opponent piece
          if (targetPiece == null || targetPiece.Color != Color)
          {
            validMoves.Add(targetLocation);
          }
        }
      }

      return validMoves;
    }
  }
}
