using System.Collections.Generic;

namespace console_classes_testing
{
  public class Rook : SlideMovablePiece
  {
    public Rook(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board)
    {
      Symbol = 'R';
    }
    public override List<ChessLocation> GetValidMoves()
    {
      List<ChessLocation> validMoves = new List<ChessLocation>();

      // Check threats horizontally
      AddValidMovesInDirection(validMoves, 0, 1); // Right
      AddValidMovesInDirection(validMoves, 0, -1); // Left

      // Check threats vertically
      AddValidMovesInDirection(validMoves, 1, 0); // Down
      AddValidMovesInDirection(validMoves, -1, 0); // Up

      return validMoves;
    }
  }
}
