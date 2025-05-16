namespace TetrisGame
{
    public partial class MainPage : ContentPage
    {
        //public TetrisDrawable TetrisDrawable { get; set; }
        public MainPage()
        {
            InitializeComponent();

            var pieces = new Pieces();
            var board = new Board(pieces);
            var game = new GameManager(board, pieces);

            TetrisDrawable = new TetrisDrawable(game);
            BindingContext = this;

            GameCanvas.Invalidate();
        }
        
    }
}
