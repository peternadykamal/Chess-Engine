using System.Collections.Generic;

namespace console_classes_testing
{
  public class Bishop : SlideMovablePiece
  {
    public Bishop(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board)
    {
      Symbol = 'B';
    }

    public override List<ChessLocation> GetValidMoves()
    {
      List<ChessLocation> validMoves = new List<ChessLocation>();

      // Check threats diagonally
      AddValidMovesInDirection(validMoves, 1, 1); // Diagonal down-right
      AddValidMovesInDirection(validMoves, 1, -1); // Diagonal down-left
      AddValidMovesInDirection(validMoves, -1, 1); // Diagonal up-right
      AddValidMovesInDirection(validMoves, -1, -1); // Diagonal up-left

      return validMoves;
    }
  }
}
