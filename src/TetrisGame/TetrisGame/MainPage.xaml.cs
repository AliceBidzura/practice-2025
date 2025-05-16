using Microsoft.Maui.Controls;
using System;
using System.Timers;

namespace TetrisGame
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer fallTimer;
        private Board board;
        private GameManager game;

        public TetrisDrawable TetrisDrawable { get; set; }
        public NextPieceDrawable NextPieceDrawable { get; set; }

        public MainPage()
        {
            InitializeComponent();

            var pieces = new Pieces();
            board = new Board(pieces); 
            game = new GameManager(board, pieces);

            TetrisDrawable = new TetrisDrawable(game);
            NextPieceDrawable = new NextPieceDrawable(game);

            BindingContext = this;

            StartTimer();
            GameCanvas.Invalidate();
            NextPieceCanvas.Invalidate();
        }
        private void StartTimer()
        {
            fallTimer = new System.Timers.Timer(500); // 500 мс — начальная скорость
            fallTimer.Elapsed += OnTimerElapsed;
            fallTimer.AutoReset = true;
            fallTimer.Start();
        }
        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                //int newY = game.CurrentY + 1;

                if (board.IsPossibleToMovement(game.CurrentX, game.CurrentY + 1, game.CurrentPiece, game.CurrentRotation))
                {
                    game.CurrentY++;
                }
                else
                {
                    board.StorePiece(game.CurrentX, game.CurrentY, game.CurrentPiece, game.CurrentRotation);
                    board.DeletePossibleLines();

                    if (board.IsGameOver())
                    {
                        fallTimer.Stop();
                        DisplayAlert("Игра окончена", "Ты проиграл", "ОК");
                        return;
                    }

                    game.CreateNewPiece();
                }
                GameCanvas.Invalidate(); // Перерисовка
                NextPieceCanvas.Invalidate();
            });
        }
        // Обработка кнопок управления
        private void HandleInput(string input)
        {
            switch (input)
            {
                case "Left":
                    if (board.IsPossibleToMovement(game.CurrentX - 1, game.CurrentY, game.CurrentPiece, game.CurrentRotation))
                    {
                        game.CurrentX--;
                        GameCanvas.Invalidate();
                    }
                    break;

                case "Right":
                    if (board.IsPossibleToMovement(game.CurrentX + 1, game.CurrentY, game.CurrentPiece, game.CurrentRotation))
                    {
                        game.CurrentX++;
                        GameCanvas.Invalidate();
                    }
                    break;

                case "Down":
                    if (board.IsPossibleToMovement(game.CurrentX, game.CurrentY + 1, game.CurrentPiece, game.CurrentRotation))
                    {
                        game.CurrentY++;
                        GameCanvas.Invalidate();
                    }
                    break;

                case "Rotate":
                    int newRotation = (game.CurrentRotation + 1) % 4;
                    if (board.IsPossibleToMovement(game.CurrentX, game.CurrentY, game.CurrentPiece, newRotation))
                    {
                        game.CurrentRotation = newRotation;
                        GameCanvas.Invalidate();
                    }
                    break;
            }
        }
        // обработчики кнопок
        private void OnLeftClicked(object sender, EventArgs e) => HandleInput("Left");
        private void OnRightClicked(object sender, EventArgs e) => HandleInput("Right");
        private void OnDownClicked(object sender, EventArgs e) => HandleInput("Down");
        private void OnRotateClicked(object sender, EventArgs e) => HandleInput("Rotate");

    }
}
