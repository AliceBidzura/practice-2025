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

        // Ссылки на другие классы
        public readonly Pieces pieces;
        private readonly Board board;
        private readonly Random random;

        public int CurrentPiece;
        public int CurrentRotation;
        public int CurrentX;
        public int CurrentY;

        public int Score { get; set; }
        public int Lines { get; set; }
        public int Level => Lines / 10 + 1; // Каждые 10 линий — новый уровень


        //Следующая фигура
        private int nextPiece;
        private int nextRotation;

        //В конструкторе
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

        //Инициализация первой и следующей фигуры
        public void InitGame()
        {
            CurrentPiece = GetRand(0, 6);
            CurrentRotation = GetRand(0, 3);
            CurrentX = (BOARD_WIDTH / 2) + pieces.GetXInitialPosition(CurrentPiece, CurrentRotation);
            CurrentY = pieces.GetYInitialPosition(CurrentPiece, CurrentRotation);

            // Следующая фигура
            nextPiece = GetRand(0, 6);
            nextRotation = GetRand(0, 3);

            Score = 0;
            Lines = 0;
        }

        //Спавн следующей фигуры и заготовка новой
        public bool CreateNewPiece()
        {
            CurrentPiece = nextPiece;
            CurrentRotation = nextRotation;
            CurrentX = (BOARD_WIDTH / 2) + pieces.GetXInitialPosition(CurrentPiece, CurrentRotation);
            CurrentY = pieces.GetYInitialPosition(CurrentPiece, CurrentRotation);

            nextPiece = GetRand(0, 6);
            nextRotation = GetRand(0, 3);

            return board.IsPossibleToMovement(CurrentX, CurrentY, CurrentPiece, CurrentRotation);
        }
        public int getNextPiece()
        {
            return this.nextPiece;
        }

        public int getNextRotation()
        {
            return this.nextRotation;
        }
        public Board getBoard()
        {
            return this.board;
        }
    }
}