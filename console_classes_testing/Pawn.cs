using System.Collections.Generic;

namespace console_classes_testing
{
  public class Pawn : ChessPiece
  {
    public Pawn(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board)
    {
      Symbol = 'P';
    }
    public override bool HasMoved()
    {
      // Assuming white pawns start at row 1 and black pawns start at row 6
      int startingRow = (Color == PieceColor.White) ? 1 : 6;
      return Location.Row != startingRow;
    }
    public override bool IsThreateningKing()
    {
      int direction = (Color == PieceColor.White) ? 1 : -1;
      int targetRow = Location.Row + direction;

      // Check left diagonal
      ChessPiece leftDiagonalPiece = Board.GetPieceAtLocation(targetRow, Location.Column - 1);
      bool isPawnAtFarLeftColumn = Location.Column == 0;
      bool isOpponentKingLeft = leftDiagonalPiece is King && leftDiagonalPiece.Color != Color;
      if (!isPawnAtFarLeftColumn && isOpponentKingLeft)
      {
        return true;
      }

      // Check right diagonal
      ChessPiece rightDiagonalPiece = Board.GetPieceAtLocation(targetRow, Location.Column + 1);
      bool isPawnAtFarRightColumn = Location.Column == 7;
      bool isOpponentKingRight = rightDiagonalPiece is King && rightDiagonalPiece.Color != Color;
      if (!isPawnAtFarRightColumn && isOpponentKingRight)
      {
        return true;
      }

      return false;
    }
    public override List<ChessLocation> GetValidMoves()
    {
      List<ChessLocation> validMoves = new List<ChessLocation>();

      int direction = (Color == PieceColor.White) ? 1 : -1;
      int forwardOne = Location.Row + direction;
      int forwardTwo = Location.Row + 2 * direction;

      // Forward one square
      AddValidMoveIfEmpty(validMoves, forwardOne, Location.Column);

      // Forward two squares (only from starting position)
      if (!HasMoved())
      {
        AddValidMoveIfEmpty(validMoves, forwardTwo, Location.Column);
      }

      // Diagonal captures
      AddValidCaptureMove(validMoves, forwardOne, Location.Column - 1);
      AddValidCaptureMove(validMoves, forwardOne, Location.Column + 1);

      return validMoves;
    }
    private void AddValidMoveIfEmpty(List<ChessLocation> moves, int row, int col)
    {
      if (ChessLocation.TryCreate(row, col, out ChessLocation location) && Board.GetPieceAtLocation(location) == null)
      {
        moves.Add(location);
      }
    }
    private void AddValidCaptureMove(List<ChessLocation> moves, int row, int col)
    {
      if (ChessLocation.TryCreate(row, col, out ChessLocation location))
      {
        ChessPiece targetPiece = Board.GetPieceAtLocation(location);
        if (targetPiece != null && targetPiece.Color != Color)
        {
          moves.Add(location);
        }
      }
    }
  }
}
