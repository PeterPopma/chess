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

        KeyboardState currentKeyboardState;
        GamePadState currentGamePadState;

        KeyboardState lastKeyboardState;
        GamePadState lastGamePadState;
        FormMain parentForm;
        ChessboardPosition squarePosition = new ChessboardPosition();
        ChessboardPosition squareSelected = new ChessboardPosition();
        ChessboardPosition squarePossible = new ChessboardPosition();
        ChessboardPosition previousPositionFrom;
        ChessboardPosition previousPositionTo;
        ChessPiece previousPieceFrom;
        ChessPiece previousPieceTo;
        List<ChessboardPosition> possibleMoves = new List<ChessboardPosition>();
        ChessPiece[,] chessBoard = new ChessPiece[8, 8];
        bool activePlayer;
        const bool PlayerWhite = true;
        const bool PlayerBlack = false;

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
        public ChessPiece[,] ChessBoard { get => chessBoard; set => chessBoard = value; }
        public bool ActivePlayer { get => activePlayer; set => activePlayer = value; }

        private void MouseWheelHandler(object sender, MouseEventArgs e)
        {
            scrollWheelValue += e.Delta;
        }

        public void UndoLastMove()
        {
            ChessBoard[previousPositionTo.X, previousPositionTo.Y] = previousPieceTo;
            ChessBoard[previousPositionFrom.X, previousPositionFrom.Y] = previousPieceFrom;
            NextPlayer();
            UpdateMessage();
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
            ClearChessboard(ChessBoard);
            ChessBoard[0, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black);
            ChessBoard[1, 0] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.Black);
            ChessBoard[2, 0] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black);
            ChessBoard[3, 0] = new ChessPiece(ChessPieceType.Queen, ChessPieceColor.Black);
            ChessBoard[4, 0] = new ChessPiece(ChessPieceType.King, ChessPieceColor.Black);
            ChessBoard[5, 0] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black);
            ChessBoard[6, 0] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.Black);
            ChessBoard[7, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black);
            ChessBoard[0, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[1, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[2, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[3, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[4, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[5, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[6, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);
            ChessBoard[7, 1] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black);

            ChessBoard[0, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[1, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[2, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[3, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[4, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[5, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[6, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[7, 6] = new ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White);
            ChessBoard[0, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.White);
            ChessBoard[1, 7] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.White);
            ChessBoard[2, 7] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White);
            ChessBoard[3, 7] = new ChessPiece(ChessPieceType.Queen, ChessPieceColor.White);
            ChessBoard[4, 7] = new ChessPiece(ChessPieceType.King, ChessPieceColor.White);
            ChessBoard[5, 7] = new ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White);
            ChessBoard[6, 7] = new ChessPiece(ChessPieceType.Horse, ChessPieceColor.White);
            ChessBoard[7, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceColor.White);

            ActivePlayer = PlayerWhite;
            UpdateMessage();
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
                    if (!ChessBoard[x, y].Type.Equals(ChessPieceType.None))
                    {
                        Texture2D texture = getTextureByName(ChessBoard[x, y].PieceName, ChessBoard[x, y].IsWhite);
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
                if (ChessBoard[position.X, y].IsNone || ChessBoard[position.X, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(position.X, y));
                }
                if (!ChessBoard[position.X, y].IsNone)
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
                if (ChessBoard[position.X, y].IsNone || ChessBoard[position.X, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(position.X, y));
                }
                if (!ChessBoard[position.X, y].IsNone)
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
                if (ChessBoard[x, position.Y].IsNone || ChessBoard[x, position.Y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, position.Y));
                }
                if (!ChessBoard[x, position.Y].IsNone)
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
                if (ChessBoard[x, position.Y].IsNone || ChessBoard[x, position.Y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, position.Y));
                }
                if (!ChessBoard[x, position.Y].IsNone)
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
                if (ChessBoard[x, y].IsNone || ChessBoard[x, y].IsWhite != isWhite)
                {
                    return true;
                }
            }
            return false;
        }

        List<ChessboardPosition> ValidateMovesInCheck(List<ChessboardPosition> moves, ChessboardPosition startPosition, bool isWhite)
        {
            List<ChessboardPosition> validMoves = new List<ChessboardPosition>();
            foreach(ChessboardPosition position in moves)
            {
                if (!MoveInCheck(startPosition, position))
                    validMoves.Add(position);
            }

            return validMoves;
        }

        List<ChessboardPosition> CheckCastling(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();

            ChessPiece piece = ChessBoard[position.X, position.Y];
            if (!piece.HasMoved)
            {
                if (isWhite)
                {
                    if (ChessBoard[0, 7].Type.Equals(ChessPieceType.Rook)
                        && !ChessBoard[0, 7].HasMoved
                        && ChessBoard[1, 7].IsNone
                        && ChessBoard[2, 7].IsNone
                        && ChessBoard[3, 7].IsNone)
                    {
                        if (!MoveInCheck(position, new ChessboardPosition(position.X, position.Y)) &&
                            !MoveInCheck(position, new ChessboardPosition(position.X - 1, position.Y)) &&
                            !MoveInCheck(position, new ChessboardPosition(position.X - 2, position.Y)))
                        {
                            moves.Add(new ChessboardPosition(position.X - 2, position.Y));
                        }
                    }
                    if (ChessBoard[7, 7].Type.Equals(ChessPieceType.Rook)
                        && !ChessBoard[7, 7].HasMoved
                        && ChessBoard[5, 7].IsNone
                        && ChessBoard[6, 7].IsNone)
                    {
                        if (!MoveInCheck(position, new ChessboardPosition(position.X, position.Y)) &&
                            !MoveInCheck(position, new ChessboardPosition(position.X + 1, position.Y)) &&
                            !MoveInCheck(position, new ChessboardPosition(position.X + 2, position.Y)))
                        {
                            moves.Add(new ChessboardPosition(position.X + 2, position.Y));
                        }
                    }
                }
                else
                {
                    if (ChessBoard[0, 0].Type.Equals(ChessPieceType.Rook)
                        && !ChessBoard[0, 0].HasMoved
                        && ChessBoard[1, 0].IsNone
                        && ChessBoard[2, 0].IsNone
                        && ChessBoard[3, 0].IsNone)
                    {
                        if (!MoveInCheck(position, new ChessboardPosition(position.X, position.Y)) &&
                            !MoveInCheck(position, new ChessboardPosition(position.X - 1, position.Y)) &&
                            !MoveInCheck(position, new ChessboardPosition(position.X - 2, position.Y)))
                        {
                            moves.Add(new ChessboardPosition(position.X - 2, position.Y));
                        }
                    }
                    if (ChessBoard[7, 0].Type.Equals(ChessPieceType.Rook)
                        && !ChessBoard[7, 0].HasMoved
                        && ChessBoard[5, 0].IsNone
                        && ChessBoard[6, 0].IsNone)
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

            return moves;
        }

        List<ChessboardPosition> checkKingMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();
            for (int x = position.X - 1; x <= position.X + 1; x++)
            {
                if (CheckMove(x, position.Y - 1, isWhite))
                {
                    moves.Add(new ChessboardPosition(x, position.Y - 1));
                }
                if (x != position.X && CheckMove(x, position.Y, isWhite))
                {
                    moves.Add(new ChessboardPosition(x, position.Y));
                }
                if (CheckMove(x, position.Y + 1, isWhite))
                {
                    moves.Add(new ChessboardPosition(x, position.Y + 1));
                }
            }
            return moves;
        }

        List<ChessboardPosition> checkPawnMoves(ChessboardPosition position, bool isWhite)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();
            if (isWhite)
            {
                if (CheckMove(position.X, position.Y - 1, isWhite) && !ChessBoard[position.X, position.Y - 1].IsBlack)
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y - 1));
                }
                if (CheckMove(position.X + 1, position.Y - 1, isWhite) && ChessBoard[position.X + 1, position.Y - 1].IsBlack)
                {
                    moves.Add(new ChessboardPosition(position.X + 1, position.Y - 1));
                }
                if (CheckMove(position.X - 1, position.Y - 1, isWhite) && ChessBoard[position.X - 1, position.Y - 1].IsBlack)
                {
                    moves.Add(new ChessboardPosition(position.X - 1, position.Y - 1));
                }
                if (position.Y == 6 && CheckMove(position.X, position.Y - 2, isWhite) && !ChessBoard[position.X, position.Y - 2].IsBlack && ChessBoard[position.X, position.Y - 1].IsNone)
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y - 2));
                }
            }
            else
            {
                if (CheckMove(position.X, position.Y + 1, isWhite) && !ChessBoard[position.X, position.Y + 1].IsWhite)
                {
                    moves.Add(new ChessboardPosition(position.X, position.Y + 1));
                }
                if (CheckMove(position.X + 1, position.Y + 1, isWhite) && ChessBoard[position.X + 1, position.Y + 1].IsWhite)
                {
                    moves.Add(new ChessboardPosition(position.X + 1, position.Y + 1));
                }
                if (CheckMove(position.X - 1, position.Y + 1, isWhite) && ChessBoard[position.X - 1, position.Y + 1].IsWhite)
                {
                    moves.Add(new ChessboardPosition(position.X - 1, position.Y + 1));
                }
                if (position.Y == 1 && CheckMove(position.X, position.Y + 2, isWhite) && !ChessBoard[position.X, position.Y + 2].IsWhite && ChessBoard[position.X, position.Y + 1].IsNone)
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
                if (ChessBoard[x, y].IsNone || ChessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!ChessBoard[x, y].IsNone)
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
                if (ChessBoard[x, y].IsNone || ChessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!ChessBoard[x, y].IsNone)
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
                if (ChessBoard[x, y].IsNone || ChessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!ChessBoard[x, y].IsNone)
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
                if (ChessBoard[x, y].IsNone || ChessBoard[x, y].IsWhite != isWhite)
                {
                    moves.Add(new ChessboardPosition(x, y));
                }
                if (!ChessBoard[x, y].IsNone)
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

        public List<ChessboardPosition> CheckPossibleMoves(int x, int y)
        {
            return CheckPossibleMoves(new ChessboardPosition(x,y));
        }

        List<ChessboardPosition> CheckPossibleMoves(ChessboardPosition position)
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();
            ChessPiece piece = ChessBoard[position.X, position.Y];

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
                    moves = checkKingMoves(position, piece.IsWhite);
                    break;
                case "Pawn":
                    moves = checkPawnMoves(position, piece.IsWhite);
                    break;
            }

            return moves;
        }

        bool IsCheck(ChessboardPosition kingPosition, bool isWhite)
        {
            bool isCheck = false;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (/*!ChessBoard[x, y].Type.Equals(ChessPieceType.King) &&*/ ((isWhite && ChessBoard[x, y].IsBlack) || (!isWhite && ChessBoard[x, y].IsWhite)))
                    {
                        List<ChessboardPosition> moves = CheckPossibleMoves(new ChessboardPosition(x, y));
                        if (moves.Exists(obj => obj.X == kingPosition.X && obj.Y == kingPosition.Y))
                        {
                            isCheck = true;
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
                    if (ChessBoard[x, y].Type.Equals(ChessPieceType.King) && ChessBoard[x, y].IsWhite == isWhite)
                    {
                        position = new ChessboardPosition(x, y);
                        break;
                    }
                }
            }

            return IsCheck(position, isWhite);
        }

        int CountPieces(ChessPieceType type, bool isWhite)
        {
            int count = 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (ChessBoard[x, y].Type.Equals(type) && ChessBoard[x, y].IsWhite == isWhite)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        void PromotePawn(ChessboardPosition position, bool isWhite)
        {
            if(CountPieces(ChessPieceType.Queen, isWhite)==0)
            {
                ChessBoard[position.X, position.Y].Type = ChessPieceType.Queen;
                return;
            }
            if (CountPieces(ChessPieceType.Rook, isWhite) < 2)
            {
                ChessBoard[position.X, position.Y].Type = ChessPieceType.Rook;
                return;
            }
            if (CountPieces(ChessPieceType.Bishop, isWhite) < 2)
            {
                ChessBoard[position.X, position.Y].Type = ChessPieceType.Bishop;
                return;
            }
            if (CountPieces(ChessPieceType.Horse, isWhite) < 2)
            {
                ChessBoard[position.X, position.Y].Type = ChessPieceType.Horse;
                return;
            }
        }

        void CheckPawnPromotion(bool isWhite)
        {
            for (int x = 0; x < 8; x++)
            {
                if(isWhite)
                {
                    if(ChessBoard[x, 0].Type.Equals(ChessPieceType.Pawn) && ChessBoard[x, 0].IsWhite)
                    {
                        PromotePawn(new ChessboardPosition(x, 0), isWhite);
                    }
                }
                else
                {
                    if (ChessBoard[x, 7].Type.Equals(ChessPieceType.Pawn) && ChessBoard[x, 0].IsBlack)
                    {
                        PromotePawn(new ChessboardPosition(x, 7), isWhite);
                    }
                }
            }
        }

        public List<ChessboardPosition> GetMoveablePositions()
        {
            List<ChessboardPosition> pieces = new List<ChessboardPosition>(); ;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (!ChessBoard[x, y].Type.Equals(ChessPieceType.None) && ChessBoard[x, y].IsWhite == ActivePlayer)
                    {
                        ChessboardPosition position = new ChessboardPosition(x, y);
                        if (FindPossibleMoves(position).Count > 0)
                        {
                            pieces.Add(new ChessboardPosition(x, y));
                        }
                    }
                }
            }

            return pieces;
        }
       

        // Check all possible moves; if none -> checkmate!
        bool IsCheckMate()
        {
            List<ChessboardPosition> moves = new List<ChessboardPosition>();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (!ChessBoard[x, y].Type.Equals(ChessPieceType.None) && ChessBoard[x, y].IsWhite == ActivePlayer)
                    {
                        ChessboardPosition position = new ChessboardPosition(x, y);
                        if(FindPossibleMoves(position).Count>0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        // Check if this is removed, the player would be in check
        // We do this by temporary removing the piece from the board
        bool MoveInCheck(ChessboardPosition fromPosition)
        {
            bool inCheck = false;
            ChessPiece oldFrom = ChessBoard[fromPosition.X, fromPosition.Y];
            ChessBoard[fromPosition.X, fromPosition.Y] = new ChessPiece();
            if (IsCheck(oldFrom.IsWhite))
            {
                inCheck = true;
            }
            ChessBoard[fromPosition.X, fromPosition.Y] = oldFrom;

            return inCheck;
        }

        // Check if this move is made, the player would be in check
        // We do this by temporary moving the piece on the board
        bool MoveInCheck(ChessboardPosition fromPosition, ChessboardPosition toPosition)
        {
            bool inCheck = false;
            ChessPiece oldFrom = ChessBoard[fromPosition.X, fromPosition.Y];
            ChessPiece oldTo = ChessBoard[toPosition.X, toPosition.Y];
            ChessBoard[fromPosition.X, fromPosition.Y] = new ChessPiece();
            ChessBoard[toPosition.X, toPosition.Y] = oldFrom;
            if (IsCheck(oldFrom.IsWhite))
            {
                inCheck = true;
            }
            ChessBoard[fromPosition.X, fromPosition.Y] = oldFrom;
            ChessBoard[toPosition.X, toPosition.Y] = oldTo;

            return inCheck;
        }

        bool IsOwnPiece(ChessPiece piece)
        {
            return (ActivePlayer == PlayerWhite && piece.IsWhite) || (ActivePlayer == PlayerBlack && piece.IsBlack);
        }

        void UpdateSelectionSquare()
        {
            squarePossible.setNone();
            if (!squarePosition.IsNone)
            {
                if (FindPossibleMoves(squarePosition).Count>0)
                {
                    squarePossible.CopyValuesFrom(squarePosition);
                }
            }
        }

        public string MakeMove(int xfrom, int yfrom, int xto, int yto)
        {
            squareSelected = new ChessboardPosition(xfrom, yfrom);
            possibleMoves = FindPossibleMoves(squareSelected);
            if (possibleMoves.Count == 0)
            {
                return "source square invalid";
            }
            squarePosition = new ChessboardPosition(xto, yto);
            if (!possibleMoves.Exists(obj => obj.X == squarePosition.X && obj.Y == squarePosition.Y))
            {
                return "destination square invalid";
            }
            MakeMove();
            squareSelected.setNone();
            possibleMoves.Clear();

            return "success";
        }

        void MakeMove()
        {
            ChessPiece selectedPiece = ChessBoard[squareSelected.X, squareSelected.Y];
            // Check for castling and move rook
            if (selectedPiece.Type.Equals(ChessPieceType.King))
            {
                if (selectedPiece.IsWhite)
                {
                    if (squareSelected.X == 4 && squareSelected.Y == 7 && squarePosition.X == 2 && squarePosition.Y == 7)
                    {
                        ChessBoard[3, 7] = ChessBoard[0, 7];
                        ChessBoard[3, 7].HasMoved = true;
                        ChessBoard[0, 7] = new ChessPiece();
                    }
                    if (squareSelected.X == 4 && squareSelected.Y == 7 && squarePosition.X == 6 && squarePosition.Y == 7)
                    {
                        ChessBoard[5, 7] = ChessBoard[7, 7];
                        ChessBoard[5, 7].HasMoved = true;
                        ChessBoard[7, 7] = new ChessPiece();
                    }
                }
                else
                {
                    if (squareSelected.X == 4 && squareSelected.Y == 0 && squarePosition.X == 2 && squarePosition.Y == 0)
                    {
                        ChessBoard[3, 0] = ChessBoard[0, 0];
                        ChessBoard[3, 0].HasMoved = true;
                        ChessBoard[0, 0] = new ChessPiece();
                    }
                    if (squareSelected.X == 4 && squareSelected.Y == 0 && squarePosition.X == 6 && squarePosition.Y == 0)
                    {
                        ChessBoard[5, 0] = ChessBoard[7, 0];
                        ChessBoard[5, 0].HasMoved = true;
                        ChessBoard[7, 0] = new ChessPiece();
                    }
                }
            }
            previousPositionFrom = new ChessboardPosition(squarePosition.X, squarePosition.Y);
            previousPositionTo = new ChessboardPosition(squareSelected.X, squareSelected.Y);
            previousPieceFrom = ChessBoard[squarePosition.X, squarePosition.Y];
            previousPieceTo = new ChessPiece(selectedPiece.Type, selectedPiece.Color, selectedPiece.HasMoved);

            selectedPiece.HasMoved = true;
            ChessBoard[squarePosition.X, squarePosition.Y] = selectedPiece;
            ChessBoard[squareSelected.X, squareSelected.Y] = new ChessPiece();
            CheckPawnPromotion(ActivePlayer);
            NextPlayer();
            UpdateMessage();
        }

        void UpdateMessage()
        {
            if (IsCheckMate())
            {
                parentForm.labelMessage.Text = "Checkmate!";
            }
            else if (IsCheck(ActivePlayer))
            {
                parentForm.labelMessage.Text = "Check!";
            }
            else
            {
                parentForm.labelMessage.Text = "";
            }
        }

        void NextPlayer()
        {
            ActivePlayer = !ActivePlayer;
        }

        List<ChessboardPosition> FindPossibleMoves(ChessboardPosition position)
        {
            List<ChessboardPosition> possibleMoves = new List<ChessboardPosition>();
            ChessPiece pieceSelected = ChessBoard[position.X, position.Y];
            if (IsOwnPiece(pieceSelected))
            {
                possibleMoves = CheckPossibleMoves(position);
                possibleMoves = ValidateMovesInCheck(possibleMoves, position, pieceSelected.IsWhite);
                if (pieceSelected.Type.Equals(ChessPieceType.King))
                {
                    possibleMoves.AddRange(CheckCastling(position, pieceSelected.IsWhite));
                }
            }

            return possibleMoves;
        }

        public void OnClick(int X, int Y)
        {
            if (!squarePosition.IsNone)
            {
                if (squareSelected.IsNone)
                {
                    possibleMoves = FindPossibleMoves(squarePosition);
                    if (possibleMoves.Count > 0)
                    {
                        squareSelected.CopyValuesFrom(squarePosition);
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
            squarePosition.X = (MousePosition.X - 49) / 120;
            squarePosition.Y = (MousePosition.Y - 71) / 120;
            if(squarePosition.X<0 || squarePosition.Y < 0 || squarePosition.X >7 || squarePosition.Y > 7)
            {
                squarePosition.setNone();
            }

            UpdateSelectionSquare();

            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

    }
}
