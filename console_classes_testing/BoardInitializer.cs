using System.Collections.Generic;

namespace console_classes_testing
{
  public class BoardInitializer
  {
    private ChessBoard board;
    public King WhiteKing { get; private set; }
    public King BlackKing { get; private set; }
    public BoardInitializer(ChessBoard board)
    {
      this.board = board;
    }

    public List<ChessPiece> GetIntialChessPieces()
    {
      List<ChessPiece> chessPieces = new List<ChessPiece>();

      List<King> kings = CreateIntialKings();
      WhiteKing = kings[0].Color == PieceColor.White ? kings[0] : kings[1];
      BlackKing = kings[0].Color == PieceColor.Black ? kings[0] : kings[1];
      chessPieces.AddRange(kings);
      chessPieces.AddRange(CreateIntialPawns());
      chessPieces.AddRange(createIntialRooks());
      chessPieces.AddRange(CreateInitialKnights());
      chessPieces.AddRange(CreateIntialBishops());
      chessPieces.AddRange(CreateInitialQueens());

      return chessPieces;
    }
    private List<Pawn> CreateIntialPawns()
    {
      List<Pawn> pawns = new List<Pawn>();
      for (int i = 0; i < 8; i++)
      {
        ChessLocation whitePawnLocation;
        ChessLocation.TryCreate(2, (char)('a' + i), out whitePawnLocation);

        ChessLocation blackPawnLocation;
        ChessLocation.TryCreate(7, (char)('a' + i), out blackPawnLocation);

        pawns.Add(new Pawn(PieceColor.White, whitePawnLocation, board));
        pawns.Add(new Pawn(PieceColor.Black, blackPawnLocation, board));
      }
      return pawns;
    }
    private List<King> CreateIntialKings()
    {
      List<King> kings = new List<King>();

      ChessLocation whiteKingLocation;
      ChessLocation.TryCreate(1, 'e', out whiteKingLocation);
      kings.Add(new King(PieceColor.White, whiteKingLocation, board));

      ChessLocation blackKingLocation;
      ChessLocation.TryCreate(8, 'e', out blackKingLocation);
      kings.Add(new King(PieceColor.Black, blackKingLocation, board));

      return kings;
    }
    private List<Knight> CreateInitialKnights()
    {
      List<Knight> knights = new List<Knight>();

      // Create white knights
      ChessLocation whiteKnight1Location;
      ChessLocation.TryCreate(1, 'b', out whiteKnight1Location);
      knights.Add(new Knight(PieceColor.White, whiteKnight1Location, board));

      ChessLocation whiteKnight2Location;
      ChessLocation.TryCreate(1, 'g', out whiteKnight2Location);
      knights.Add(new Knight(PieceColor.White, whiteKnight2Location, board));

      // Create black knights
      ChessLocation blackKnight1Location;
      ChessLocation.TryCreate(8, 'b', out blackKnight1Location);
      knights.Add(new Knight(PieceColor.Black, blackKnight1Location, board));

      ChessLocation blackKnight2Location;
      ChessLocation.TryCreate(8, 'g', out blackKnight2Location);
      knights.Add(new Knight(PieceColor.Black, blackKnight2Location, board));

      return knights;
    }
    private List<Rook> createIntialRooks()
    {
      List<Rook> rooks = new List<Rook>();

      ChessLocation whiteRook1Location;
      ChessLocation.TryCreate(1, 'a', out whiteRook1Location);
      rooks.Add(new Rook(PieceColor.White, whiteRook1Location, board));

      ChessLocation whiteRook2Location;
      ChessLocation.TryCreate(1, 'h', out whiteRook2Location);
      rooks.Add(new Rook(PieceColor.White, whiteRook2Location, board));

      ChessLocation blackRook1Location;
      ChessLocation.TryCreate(8, 'a', out blackRook1Location);
      rooks.Add(new Rook(PieceColor.Black, blackRook1Location, board));

      ChessLocation blackRook2Location;
      ChessLocation.TryCreate(8, 'h', out blackRook2Location);
      rooks.Add(new Rook(PieceColor.Black, blackRook2Location, board));

      return rooks;
    }
    private List<Bishop> CreateIntialBishops()
    {
      List<Bishop> bishops = new List<Bishop>();

      ChessLocation whiteBishop1Location;
      ChessLocation.TryCreate(1, 'c', out whiteBishop1Location);
      bishops.Add(new Bishop(PieceColor.White, whiteBishop1Location, board));

      ChessLocation whiteBishop2Location;
      ChessLocation.TryCreate(1, 'f', out whiteBishop2Location);
      bishops.Add(new Bishop(PieceColor.White, whiteBishop2Location, board));

      ChessLocation blackBishop1Location;
      ChessLocation.TryCreate(8, 'c', out blackBishop1Location);
      bishops.Add(new Bishop(PieceColor.Black, blackBishop1Location, board));

      ChessLocation blackBishop2Location;
      ChessLocation.TryCreate(8, 'f', out blackBishop2Location);
      bishops.Add(new Bishop(PieceColor.Black, blackBishop2Location, board));

      return bishops;
    }
    private List<Queen> CreateInitialQueens()
    {
      List<Queen> queens = new List<Queen>();

      ChessLocation whiteQueenLocation;
      ChessLocation.TryCreate(1, 'd', out whiteQueenLocation);
      queens.Add(new Queen(PieceColor.White, whiteQueenLocation, board));

      ChessLocation blackQueenLocation;
      ChessLocation.TryCreate(8, 'd', out blackQueenLocation);
      queens.Add(new Queen(PieceColor.Black, blackQueenLocation, board));

      return queens;
    }
  }
}
