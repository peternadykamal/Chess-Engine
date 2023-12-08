namespace console_classes_testing
{
  public class ChessCastlingMove : ChessMove
  {
    public ChessMove RookMove { get; private set; }

    public ChessCastlingMove(ChessLocation KingMovefrom, ChessLocation KingMoveto, ChessLocation RookMovefrom, ChessLocation RookMoveto, ChessPiece capturedPiece) : base(KingMovefrom, KingMoveto, capturedPiece)
    {
      RookMove = new ChessMove(RookMovefrom, RookMoveto, null);
    }
  }
}
