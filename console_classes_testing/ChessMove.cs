namespace console_classes_testing
{
  public class ChessMove
  {
    public ChessLocation From { get; }
    public ChessLocation To { get; }
    public ChessPiece CapturedPiece { get; }

    public ChessMove(ChessLocation from, ChessLocation to, ChessPiece capturedPiece)
    {
      From = from;
      To = to;
      CapturedPiece = capturedPiece;
    }
  }
}
