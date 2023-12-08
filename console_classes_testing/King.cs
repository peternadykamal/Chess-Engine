using System;
using System.Collections.Generic;
using System.Linq;

namespace console_classes_testing
{
  public class King : ChessPiece
  {
    public King(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board)
    {
      Symbol = 'K';
    }
    public bool IsInCheck()
    {
      PieceColor opponenetColor = Color == PieceColor.White ? PieceColor.Black : PieceColor.White;
      List<ChessPiece> opponentPieces = Board.GetPiecesByColor(opponenetColor);

      // Check if any opponent piece is threatening the king
      return opponentPieces.Any(opponentPiece => opponentPiece.IsThreateningKing());
    }
    public override List<ChessLocation> GetValidMoves()
    {
      List<ChessLocation> validMoves = new List<ChessLocation>();

      // Check in all eight possible directions around the king
      for (int row = Location.Row - 1; row <= Location.Row + 1; row++)
      {
        for (int col = Location.Column - 1; col <= Location.Column + 1; col++)
        {
          // Skip the current location
          if (row == Location.Row && col == Location.Column)
            continue;

          // Check if the target location is within the board boundaries
          if (ChessLocation.TryCreate(row, col, out ChessLocation targetLocation))
          {
            ChessPiece targetPiece = Board.GetPieceAtLocation(targetLocation);

            // Check if the target location is empty or contains an opponent piece
            if (targetPiece == null || targetPiece.Color != Color)
            {
              validMoves.Add(targetLocation);
            }
          }
        }
      }
      return validMoves;
    }
    public bool IsValidCastling(ChessLocation to)
    {
      if (Math.Abs(to.Column - Location.Column) == 2)
      {
        List<ChessLocation> validCastlingMoves = new List<ChessLocation>();
        AddCastlingMoves(validCastlingMoves);
        return validCastlingMoves.Contains(to);
      }
      return false;
    }
    private void AddCastlingMoves(List<ChessLocation> moves)
    {
      if (HasMoved() || IsInCheck()) return;

      // Check kingside castling
      if (CanCastleKingside())
      {
        if (ChessLocation.TryCreate(Location.Row, Location.Column + 3, out ChessLocation kingsideRookLocation) &&
            ChessLocation.TryCreate(Location.Row, Location.Column + 2, out ChessLocation castlingMove) &&
            IsPathClear(Location, kingsideRookLocation))
        {
          moves.Add(castlingMove);
        }
      }

      // Check queenside castling
      if (CanCastleQueenside())
      {
        ChessLocation queensideRookLocation;
        if (ChessLocation.TryCreate(Location.Row, Location.Column - 4, out queensideRookLocation) &&
            ChessLocation.TryCreate(Location.Row, Location.Column - 2, out ChessLocation castlingMove) &&
            IsPathClear(Location, queensideRookLocation))
        {
          moves.Add(castlingMove);
        }
      }
    }
    private bool CanCastleKingside()
    {
      ChessPiece kingsideRook = Board.GetPieceAtLocation(Location.Row, Location.Column + 3);
      return kingsideRook is Rook && kingsideRook.Color == Color && !kingsideRook.HasMoved();
    }
    private bool CanCastleQueenside()
    {
      ChessPiece queensideRook = Board.GetPieceAtLocation(Location.Row, Location.Column - 4);
      return queensideRook is Rook && queensideRook.Color == Color && !queensideRook.HasMoved();
    }
    private bool IsPathClear(ChessLocation start, ChessLocation end)
    {
      int direction = (end.Column > start.Column) ? 1 : -1;
      for (int col = start.Column + direction; col != end.Column; col += direction)
      {
        if (Board.GetPieceAtLocation(start.Row, col) != null)
        {
          return false;
        }
      }
      return true;
    }
  }
}
