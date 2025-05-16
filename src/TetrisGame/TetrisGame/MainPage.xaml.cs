namespace TetrisGame
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer fallTimer;
        private Board board;
        private GameManager game;

        public TetrisDrawable TetrisDrawable { get; set; }
        public MainPage()
        {
            InitializeComponent();

            var pieces = new Pieces();
            board = new Board(pieces); 
            game = new GameManager(board, pieces);

            TetrisDrawable = new TetrisDrawable(game);
            BindingContext = this;

            StartTimer();

            GameCanvas.Invalidate();
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
                int newY = game.CurrentY + 1;

                if (board.IsPossibleMovement(game.CurrentX, newY, game.CurrentPiece, game.CurrentRotation))
                {
                    game.CurrentY = newY;
                }
                else
                {
                    // Фигура упала — сохранить её в поле
                    board.StorePiece(game.CurrentX, game.CurrentY, game.CurrentPiece, game.CurrentRotation);

                    // Удалить заполненные линии
                    board.DeletePossibleLines();

                    // Проверка на Game Over
                    if (board.IsGameOver())
                    {
                        fallTimer.Stop();
                        DisplayAlert("Игра окончена", "Ты проиграл", "OK");
                        return;
                    }

                    // Создать новую фигуру
                    game.CreateNewPiece();
                }

                GameCanvas.Invalidate(); // Перерисовка
            });
        }
    }
}
