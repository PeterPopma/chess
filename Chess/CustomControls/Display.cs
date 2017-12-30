using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Chess.Forms;
using System.Threading;
using Chess.Chess;
using static Chess.Chess.ChessPiece;

namespace Chess.CustomControls
{
    public class Display : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        ContentManager contentManager;
        int scrollWheelValue, lastScrollWheelValue;

        // Input state.
        KeyboardState currentKeyboardState;
        GamePadState currentGamePadState;

        KeyboardState lastKeyboardState;
        GamePadState lastGamePadState;
        FormMain parentForm;
        ChessboardPosition squarePosition = new ChessboardPosition();
        ChessboardPosition squareSelected = new ChessboardPosition();
        ChessboardPosition squarePossible = new ChessboardPosition();
        List<ChessboardPosition> possibleMoves = new List<ChessboardPosition>();
        ChessPiece[,] chessBoard = new ChessPiece[8, 8];
        int activePlayer;

        SpriteBatch spriteBatch;

        Texture2D textureChessboard;
        Texture2D textureSelect;
        Texture2D textureSelected;
        Texture2D texturePossibleMove;
        Texture2D textureWPawn;
        Texture2D textureWRook;
        Texture2D textureWHorse;
        Texture2D textureWBishop;
        Texture2D textureWQueen;
        Texture2D textureWKing;
        Texture2D textureBPawn;
        Texture2D textureBRook;
        Texture2D textureBHorse;
        Texture2D textureBBishop;
        Texture2D textureBQueen;
        Texture2D textureBKing;
        public FormMain ParentForm { get => parentForm; set => parentForm = value; }

        private void MouseWheelHandler(object sender, MouseEventArgs e)
        {
            scrollWheelValue += e.Delta;
        }

        protected override void Initialize()
        {
            ParentForm = (this.Parent as FormMain);
            //                        contentManager = new ContentManager(Services);
            //                        contentManager.RootDirectory = "Content";
            contentManager = new ResourceContentManager(Services, Resources.ResourceManager);
            // LET OP! Content dir wordt niet meer gebruikt. Als je resources wilt toevoegen moet je dit in de resourcemanager doen.
            // Je moet daar dan de .XNB files toevoegen
            // Deze kun je genereren door de uitgecommentarieerde contentmanager terug te zetten, of de XNAContentCompiler te gebruiken.

            // Load the background content. 
            textureChessboard = contentManager.Load<Texture2D>("chessboard");
            textureChessboard.Name = "Chessboard";
            textureSelect = contentManager.Load<Texture2D>("selected");
            textureSelect.Name = "Selection";
            textureSelected = contentManager.Load<Texture2D>("select");
            textureSelected.Name = "Selected";
            texturePossibleMove = contentManager.Load<Texture2D>("possible");
            textureWPawn = contentManager.Load<Texture2D>("wpawn");
            textureWRook = contentManager.Load<Texture2D>("wrook");
            textureWHorse = contentManager.Load<Texture2D>("whorse");
            textureWBishop = contentManager.Load<Texture2D>("wbishop");
            textureWQueen = contentManager.Load<Texture2D>("wqueen");
            textureWKing = contentManager.Load<Texture2D>("wking");
            textureBPawn = contentManager.Load<Texture2D>("bpawn");
            textureBRook = contentManager.Load<Texture2D>("brook");
            textureBHorse = contentManager.Load<Texture2D>("bhorse");
            textureBBishop = contentManager.Load<Texture2D>("bbishop");
            textureBQueen = contentManager.Load<Texture2D>("bqueen");
            textureBKing = contentManager.Load<Texture2D>("bking");

            this.MouseWheel += new MouseEventHandler(MouseWheelHandler);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            parentForm.labelMessage.Text = "";

            InitGame();
        }

        void ClearChessboard(ChessPiece[,] a)
        {
            for (int i = a.GetLowerBound(0); i <= a.GetUpperBound(0); i++)
            {
                for (int j = a.GetLowerBound(1); j <= a.GetUpperBound(1); j++)
                {
                    a[i, j] = new ChessPiece();
                }
            }
        }

        public void InitGame()
        {
            ClearChessboard(chessBoard);
            chessBoard[0, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black);
            chessBoard[1, 0] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.Black);
            chessBoard[2, 0] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black);
            chessBoard[3, 0] = new ChessPiece(ChessPieceType.Queen, ChessPieceColor.Black);
            chessBoard[4, 0] = new ChessPiece(ChessPieceType.King, ChessPieceColor.Black);
            chessBoard[5, 0] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black);
            chessBoard[6, 0] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.Black);
            chessBoard[7, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black);
            chessBoard[0, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[1, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[2, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[3, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[4, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[5, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[6, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            chessBoard[7, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);

            chessBoard[0, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[1, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[2, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[3, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[4, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[5, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[6, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[7, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            chessBoard[0, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.White);
            chessBoard[1, 7] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.White);
            chessBoard[2, 7] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White);
            chessBoard[3, 7] = new ChessPiece(ChessPieceType.Queen, ChessPieceColor.White);
            chessBoard[4, 7] = new ChessPiece(ChessPieceType.King, ChessPieceColor.White);
            chessBoard[5, 7] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White);
            chessBoard[6, 7] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.White);
            chessBoard[7, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.White);

            activePlayer = 0;
        }

        BlendState BlendStateSelection = new BlendState()
        {
            ColorSourceBlend = Blend.One,
            AlphaSourceBlend = Blend.SourceColor,
            ColorDestinationBlend = Blend.One,
            AlphaDestinationBlend = Blend.InverseSourceColor,
            ColorBlendFunction = BlendFunction.Add,
            AlphaBlendFunction = BlendFunction.Add
        };

        Texture2D getTextureByName(string name, bool isWhite)
        {
            switch (name)
            {
                case "Pawn":
                    if (isWhite)
                        return textureWPawn;
                    else
                        return textureBPawn;
                case "Rook":
                    if (isWhite)
                        return textureWRook;
                    else
                        return textureBRook;
                case "Horse":
                    if (isWhite)
                        return textureWHorse;
                    else
                        return textureBHorse;
                case "Bishop":
                    if (isWhite)
                        return textureWBishop;
                    else
                        return textureBBishop;
                case "Queen":
                    if (isWhite)
                        return textureWQueen;
                    else
                        return textureBQueen;
                case "King":
                    if (isWhite)
                        return textureWKing;
                    else
                        return textureBKing;
            }

            return null;
        }

        void DrawChessPieces()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (!chessBoard[x, y].Type.Equals(ChessPieceType.None))
                    {
                        Texture2D texture = getTextureByName(chessBoard[x, y].PieceName, chessBoard[x, y].IsWhite);
                        spriteBatch.Draw(texture, new Rectangle(x * 120 + 58, y * 120 - 10, texture.Width, texture.Height), Color.White);
                    }
                }
            }
        }

        override protected void Draw()
        {
            try
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                // Draw chessboard
                spriteBatch.Draw(textureChessboard, new Rectangle(0, 0, textureChessboard.Width, textureChessboard.Height), Color.White);

                if (!squareSelected.IsNone)
                {
                    spriteBatch.Draw(textureSelected, new Rectangle(29 + (squareSelected.X) * 120, 29 + (squareSelected.Y) * 120, textureSelected.Width, textureSelected.Height), Color.White);
                }

                if (possibleMoves.Count > 0)
                {
                    if (squarePosition.X == 0)
                    {
                        int pp = 0;
                    }
                    if (possibleMoves.Exists(obj => obj.X == squarePosition.X && obj.Y == squarePosition.Y))
                    {
                        spriteBatch.Draw(textureSelect, new Rectangle(29 + (squarePosition.X) * 120, 29 + (squarePosition.Y) * 120, textureSelect.Width, textureSelect.Height), Color.White);
                    }
                }
                else
                {
                    if (!squarePossible.IsNone)
                    {
                        spriteBatch.Draw(textureSelect, new Rectangle(29 + (squarePossible.X) * 120, 29 + (squarePossible.Y) * 120, textureSelect.Width, textureSelect.Height), Color.White);
                    }
                }

                foreach (ChessboardPosition move in possibleMoves)
                {
                    if (!(move.X == squarePosition.X && move.Y == squarePosition.Y))
                    {
                        spriteBatch.Draw(texturePossibleMove, new Rectangle(29 + (move.X) * 120, 29 + (move.Y) * 120, texturePossibleMove.Width, texturePossibleMove.Height), Color.White);
                    }
                }

                DrawChessPieces();

                spriteBatch.End();
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Equals("Begin cannot be called again until End had been succesfully called."))
                {
                    spriteBatch.End();
                }
            }
        }

        public void UpdateFrame()
        {
            HandleInput();
        }

        public void UpdateScreen()
        {
            Invalidate();
        }

        List<ChessboardPosition> checkRookMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();

            int y = position.Y + 1;
            while (y < 8)
            {
                if (chessBoard[position.X, y].IsNone || chessBoard[position.X, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(position.X, y));
                }
                if (!chessBoard[position.X, y].IsNone)
                {
                    y = 8;      // encountered a piece, so bail out
                }
                else
                {
                    y++;
                }
            }

            y = position.Y - 1;
            while (y >= 0)
            {
                if (chessBoard[position.X, y].IsNone || chessBoard[position.X, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(position.X, y));
                }
                if (!chessBoard[position.X, y].IsNone)
                {
                    y = -1;      // encountered a piece, so bail out
                }
                else
                {
                    y--;
                }
            }

            int x = position.X + 1;
            while (x < 8)
            {
                if (chessBoard[x, position.Y].IsNone || chessBoard[x, position.Y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, position.Y));
                }
                if (!chessBoard[x, position.Y].IsNone)
                {
                    x = 8;      // encountered a piece, so bail out
                }
                else
                {
                    x++;
                }
            }

            x = position.X - 1;
            while (x >= 0)
            {
                if (chessBoard[x, position.Y].IsNone || chessBoard[x, position.Y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, position.Y));
                }
                if (!chessBoard[x, position.Y].IsNone)
                {
                    x = -1;      // encountered a piece, so bail out
                }
                else
                {
                    x--;
                }
            }

            return moves;
        }

        bool CheckMove(int x, int y, bool isWhite)
        {
            if (x >= 0 && y >= 0 && x < 8 && y < 8)
            {
                if (chessBoard[x, y].IsNone || chessBoard[x, y].IsWhite != isWhite)
                {
                    return true;
                }
            }
            return false;
        }

        List<ChessboardPosition> checkKingMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();
            for (int x = position.X - 1; x <= position.X + 1; x++)
            {
                if (CheckMove(x, position.Y - 1, isWhite) && !MoveInCheck(position, new ChessboardPosition(x, position.Y - 1)))
                {
                    moves.Add(new ChessboardPosition(x, position.Y - 1));
                }
                if (x != position.X && CheckMove(x, position.Y, isWhite) && !MoveInCheck(position, new ChessboardPosition(x, position.Y)))
                {
                    moves.Add(new ChessboardPosition(x, position.Y));
                }
                if (CheckMove(x, position.Y + 1, isWhite) && !MoveInCheck(position, new ChessboardPosition(x, position.Y + 1)))
                {
                    moves.Add(new ChessboardPosition(x, position.Y + 1));
                }
                // Check castling
                ChessPiece piece = chessBoard[position.X, position.Y];
                if (!piece.HasMoved)
                {
                    if (isWhite)
                    {
                        if (chessBoard[0, 7].Type.Equals(ChessPieceType.Rook)
                            && !chessBoard[0, 7].HasMoved
                            && chessBoard[1, 7].IsNone
                            && chessBoard[2, 7].IsNone
                            && chessBoard[3, 7].IsNone)
                        {
                            if (!MoveInCheck(position, new ChessboardPosition(position.X, position.Y)) &&
                                !MoveInCheck(position, new ChessboardPosition(position.X - 1, position.Y)) &&
                                !MoveInCheck(position, new ChessboardPosition(position.X - 2, position.Y)))
                            {
                                moves.Add(new ChessboardPosition(position.X - 2, position.Y));
                            }
                        }
                        if (chessBoard[7, 7].Type.Equals(ChessPieceType.Rook)
                            && !chessBoard[7, 7].HasMoved
                            && chessBoard[5, 7].IsNone
                            && chessBoard[6, 7].IsNone)
                        {
                            if (!MoveInCheck(position, new ChessboardPosition(position.X, position.Y)) &&
                                !MoveInCheck(position, new ChessboardPosition(position.X + 1, position.Y)) &&
                                !MoveInCheck(position, new ChessboardPosition(position.X + 2, position.Y)))
                            {
                                moves.Add(new ChessboardPosition(position.X + 2, position.Y));
                            }
                        }
                    }
                }
            }
            return moves;
        }

        List<ChessboardPosition> checkPawnMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();
            if (isWhite)
            {
                if (CheckMove(position.X, position.Y - 1, isWhite))
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y - 1));
                }
                if (CheckMove(position.X + 1, position.Y - 1, isWhite) && chessBoard[position.X + 1, position.Y - 1].IsBlack)
                {
                    moves.Add(new ChessboardPosition(position.X + 1, position.Y - 1));
                }
                if (CheckMove(position.X - 1, position.Y - 1, isWhite) && chessBoard[position.X - 1, position.Y - 1].IsBlack)
                {
                    moves.Add(new ChessboardPosition(position.X - 1, position.Y - 1));
                }
                if (position.Y == 6 && CheckMove(position.X, position.Y - 2, isWhite) && chessBoard[position.X, position.Y - 1].IsNone)
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y - 2));
                }
            }
            else
            {
                if (CheckMove(position.X, position.Y + 1, isWhite))
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y + 1));
                }
                if (CheckMove(position.X + 1, position.Y + 1, isWhite) && chessBoard[position.X + 1, position.Y + 1].IsWhite)
                {
                    moves.Add(new ChessboardPosition(position.X + 1, position.Y + 1));
                }
                if (CheckMove(position.X - 1, position.Y + 1, isWhite) && chessBoard[position.X - 1, position.Y + 1].IsWhite)
                {
                    moves.Add(new ChessboardPosition(position.X - 1, position.Y + 1));
                }
                if (position.Y == 1 && CheckMove(position.X, position.Y + 2, isWhite) && chessBoard[position.X, position.Y + 1].IsNone)
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y + 2));
                }
            }

            return moves;
        }


        List<ChessboardPosition> checkHorseMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();

            int x = position.X + 2;
            int y = position.Y + 1;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X + 1;
            y = position.Y + 2;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X - 1;
            y = position.Y - 2;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X - 2;
            y = position.Y - 1;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X - 1;
            y = position.Y + 2;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X - 2;
            y = position.Y + 1;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X + 1;
            y = position.Y - 2;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }
            x = position.X + 2;
            y = position.Y - 1;
            if (CheckMove(x, y, isWhite))
            {
                moves.Add(new ChessboardPosition(x, y));
            }

            return moves;
        }


        List<ChessboardPosition> checkBishopMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();

            int x = position.X + 1;
            int y = position.Y + 1;
            while (y < 8 && x < 8)
            {
                if (chessBoard[x, y].IsNone || chessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!chessBoard[x, y].IsNone)
                {
                    y = 8;      // encountered a piece, so bail out
                }
                else
                {
                    x++;
                    y++;
                }
            }

            x = position.X + 1;
            y = position.Y - 1;
            while (x < 8 && y >= 0)
            {
                if (chessBoard[x, y].IsNone || chessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!chessBoard[x, y].IsNone)
                {
                    y = -1;      // encountered a piece, so bail out
                }
                else
                {
                    x++;
                    y--;
                }
            }

            x = position.X - 1;
            y = position.Y + 1;
            while (x >= 0 && y < 8)
            {
                if (chessBoard[x, y].IsNone || chessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!chessBoard[x, y].IsNone)
                {
                    y = 8;      // encountered a piece, so bail out
                }
                else
                {
                    x--;
                    y++;
                }
            }

            x = position.X - 1;
            y = position.Y - 1;
            while (x >= 0 && y >= 0)
            {
                if (chessBoard[x, y].IsNone || chessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!chessBoard[x, y].IsNone)
                {
                    x = -1;      // encountered a piece, so bail out
                }
                else
                {
                    x--;
                    y--;
                }
            }

            return moves;
        }

        List<ChessboardPosition> CheckPossibleMoves(ChessboardPosition position, bool IncludeKing = true)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();
            ChessPiece piece = chessBoard[position.X, position.Y];

            switch (piece.PieceName)
            {
                case "Rook":
                    moves = checkRookMoves(position, piece.IsWhite);
                    break;
                case "Bishop":
                    moves = checkBishopMoves(position, piece.IsWhite);
                    break;
                case "Horse":
                    moves = checkHorseMoves(position, piece.IsWhite);
                    break;
                case "Queen":
                    moves = checkRookMoves(position, piece.IsWhite);
                    moves.AddRange(checkBishopMoves(position, piece.IsWhite));
                    break;
                case "King":
                    if (IncludeKing)
                    {
                        moves = checkKingMoves(position, piece.IsWhite);       // the check for king includes CheckPossibleMoves(), so must be skipped
                    }
                    break;
                case "Pawn":
                    moves = checkPawnMoves(position, piece.IsWhite);
                    break;
            }

            return moves;
        }

        bool IsCheck(ChessboardPosition position, bool isWhite)
        {
            bool isCheck = false;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (isWhite)
                    {
                        if (chessBoard[x, y].IsBlack)
                        {
                            List<ChessboardPosition> moves = CheckPossibleMoves(new ChessboardPosition(x, y), false);
                            if (moves.Exists(obj => obj.X == position.X && obj.Y == position.Y))
                            {
                                isCheck = true;
                            }
                        }
                    }
                    else
                    {
                        if (chessBoard[x, y].IsWhite)
                        {
                            List<ChessboardPosition> moves = CheckPossibleMoves(new ChessboardPosition(x, y), false);
                            if (moves.Exists(obj => obj.X == position.X && obj.Y == position.Y))
                            {
                                isCheck = true;
                            }
                        }
                    }
                }
            }

            return isCheck;
        }

        bool IsCheck(bool isWhite)
        {
            ChessboardPosition position = null;
            // find player's king
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (chessBoard[x, y].Type.Equals(ChessPieceType.King) && chessBoard[x, y].IsWhite == isWhite)
                    {
                        position = new ChessboardPosition(x, y);
                        break;
                    }
                }
            }

            return IsCheck(position, isWhite);
        }

        // Check all possible moves; if none -> checkmate!
        bool IsCheckMate(bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (!chessBoard[x, y].Type.Equals(ChessPieceType.None) && chessBoard[x, y].IsWhite == isWhite)
                    {
                        ChessboardPosition position = new ChessboardPosition(x, y);
                        ChessPiece pieceSelected = chessBoard[position.X, position.Y];
                        if (pieceSelected.Type.Equals(ChessPieceType.King) || !MoveInCheck(position)) // not possible to move piece, because it would mean check
                        {
                            moves.AddRange(CheckPossibleMoves(position));
                        }
                    }
                }
            }

            return moves.Count == 0;
        }

        // Check if this is removed, the player would be in check
        // We do this by temporary removing the piece from the board
        bool MoveInCheck(ChessboardPosition fromPosition)
        {
            bool inCheck = false;
            ChessPiece oldFrom = chessBoard[fromPosition.X, fromPosition.Y];
            chessBoard[fromPosition.X, fromPosition.Y] = new ChessPiece();
            if (IsCheck(oldFrom.IsWhite))
            {
                inCheck = true;
            }
            chessBoard[fromPosition.X, fromPosition.Y] = oldFrom;

            return inCheck;
        }

        // Check if this move is made, the player would be in check
        // We do this by temporary moving the piece on the board
        bool MoveInCheck(ChessboardPosition fromPosition, ChessboardPosition toPosition)
        {
            bool inCheck = false;
            ChessPiece oldFrom = chessBoard[fromPosition.X, fromPosition.Y];
            ChessPiece oldTo = chessBoard[toPosition.X, toPosition.Y];
            chessBoard[fromPosition.X, fromPosition.Y] = new ChessPiece();
            chessBoard[toPosition.X, toPosition.Y] = oldFrom;
            if (IsCheck(oldFrom.IsWhite))
            {
                inCheck = true;
            }
            chessBoard[fromPosition.X, fromPosition.Y] = oldFrom;
            chessBoard[toPosition.X, toPosition.Y] = oldTo;

            return inCheck;
        }

        bool IsOwnPiece(ChessPiece piece)
        {
            return (activePlayer == 0 && piece.IsWhite) || (activePlayer == 1 && piece.IsBlack);
        }

        void UpdateSelectionSquare()
        {
            squarePossible.setNone();
            if (!squarePosition.IsNone)
            {
                ChessPiece pieceSelected = chessBoard[squarePosition.X, squarePosition.Y];
                if (pieceSelected.Type.Equals(ChessPieceType.King) || !MoveInCheck(squarePosition)) // not possible to move piece, because it would mean check
                {
                    if (IsOwnPiece(pieceSelected) && CheckPossibleMoves(squarePosition).Count > 0)
                    {
                        squarePossible.CopyValuesFrom(squarePosition);
                    }
                }
            }
        }

        void MakeMove()
        {
            // Check for castling and move rook
            if (chessBoard[squareSelected.X, squareSelected.Y].Type.Equals(ChessPieceType.King))
            {
                if (chessBoard[squareSelected.X, squareSelected.Y].IsWhite)
                {
                    if (squareSelected.X == 4 && squareSelected.Y == 7 && squarePosition.X == 2 && squarePosition.Y == 7)
                    {
                        chessBoard[3, 7] = chessBoard[0, 7];
                        chessBoard[3, 7].HasMoved = true;
                        chessBoard[0, 7] = new ChessPiece();
                    }
                    if (squareSelected.X == 4 && squareSelected.Y == 7 && squarePosition.X == 6 && squarePosition.Y == 7)
                    {
                        chessBoard[5, 7] = chessBoard[7, 7];
                        chessBoard[5, 7].HasMoved = true;
                        chessBoard[7, 7] = new ChessPiece();
                    }
                }
                else
                {
                    if (squareSelected.X == 4 && squareSelected.Y == 0 && squarePosition.X == 2 && squarePosition.Y == 0)
                    {
                        chessBoard[3, 0] = chessBoard[0, 0];
                        chessBoard[3, 0].HasMoved = true;
                        chessBoard[0, 0] = new ChessPiece();
                    }
                    if (squareSelected.X == 4 && squareSelected.Y == 0 && squarePosition.X == 6 && squarePosition.Y == 0)
                    {
                        chessBoard[5, 0] = chessBoard[7, 0];
                        chessBoard[5, 0].HasMoved = true;
                        chessBoard[7, 0] = new ChessPiece();
                    }
                }
            }
            chessBoard[squarePosition.X, squarePosition.Y] = chessBoard[squareSelected.X, squareSelected.Y];
            chessBoard[squarePosition.X, squarePosition.Y].HasMoved = true;
            chessBoard[squareSelected.X, squareSelected.Y] = new ChessPiece();
            activePlayer++;
            if (activePlayer>1)
            {
                activePlayer = 0;
            }
            if (IsCheckMate(activePlayer == 0))
            {
                parentForm.labelMessage.Text = "Checkmate!";
            }
            else if ((activePlayer == 0 && IsCheck(true)) || (activePlayer == 1 && IsCheck(false)))
            {
                parentForm.labelMessage.Text = "Check!";
            }
            else
            {
                parentForm.labelMessage.Text = "";
            }
        }

        public void OnClick(int X, int Y)
        {
            if (!squarePosition.IsNone)
            {
                if (squareSelected.IsNone)
                {
                    ChessPiece pieceSelected = chessBoard[squarePosition.X, squarePosition.Y];
                    if (IsOwnPiece(pieceSelected))
                    {
                        if (pieceSelected.Type.Equals(ChessPieceType.King) || !MoveInCheck(squarePosition)) // not possible to move piece, because it would mean check
                        {
                            possibleMoves = CheckPossibleMoves(squarePosition);
                            if (possibleMoves.Count > 0)
                            {
                                squareSelected.CopyValuesFrom(squarePosition);
                            }
                        }
                    }
                }
                else
                {
                    if (possibleMoves.Exists(obj => obj.X == squarePosition.X && obj.Y == squarePosition.Y))
                    {
                        MakeMove();
                    }
                    squareSelected.setNone();
                    possibleMoves.Clear();
                }
            }
        }

        /// <summary>
        /// Handles input for quitting the game and cycling
        /// through the different particle effects.
        /// </summary>
        void HandleInput()
        {
            lastKeyboardState = currentKeyboardState;
            lastGamePadState = currentGamePadState;
            lastScrollWheelValue = scrollWheelValue;
            if (MousePosition.X > 49 && MousePosition.Y > 71 && MousePosition.X < 1009 && MousePosition.Y < 1031)
            {
                squarePosition.X = (MousePosition.X - 49) / 120;
                squarePosition.Y = (MousePosition.Y - 71) / 120;
            }
            else
            {
                squarePosition.setNone();
            }

            UpdateSelectionSquare();

            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

    }
}
