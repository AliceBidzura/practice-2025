using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class GameManager
    {
        private const int BOARD_WIDTH = 10;
        //private const int BOARD_HEIGHT = 20;

        // ЧЧЧ —сылки на другие классы ЧЧЧ
        public readonly Pieces pieces;
        private readonly Board board;
        private readonly Random random;

        public int CurrentPiece;
        public int CurrentRotation;
        public int CurrentX;
        public int CurrentY;

        // ЧЧЧ —ледующа€ фигура ЧЧЧ
        private int nextPiece;
        private int nextRotation;

        public GameManager(Board board, Pieces pieces)
        {
            this.pieces = pieces;
            this.board = board;
            random = new Random();

            InitGame();
        }

        private int GetRand(int a, int b)
        {
            return random.Next(a, b + 1); 
        }

        //»нициализаци€ первой и следующей фигуры
        public void InitGame()
        {
            CurrentPiece = GetRand(0, 6);
            CurrentRotation = GetRand(0, 3);
            CurrentX = (BOARD_WIDTH / 2) + pieces.GetXInitialPosition(CurrentPiece, CurrentRotation);
            CurrentY = pieces.GetYInitialPosition(CurrentPiece, CurrentRotation);

            // —ледующа€ фигура
            nextPiece = GetRand(0, 6);
            nextRotation = GetRand(0, 3);
        }

        //—павн следующей фигуры и заготовка новой
        public  void CreateNewPiece()
        {
            CurrentPiece = nextPiece;
            CurrentRotation = nextRotation;
            CurrentX = (BOARD_WIDTH / 2) + pieces.GetXInitialPosition(CurrentPiece, CurrentRotation);
            CurrentY = pieces.GetYInitialPosition(CurrentPiece, CurrentRotation);

            nextPiece = GetRand(0, 6);
            nextRotation = GetRand(0, 3);
        }
    }
}