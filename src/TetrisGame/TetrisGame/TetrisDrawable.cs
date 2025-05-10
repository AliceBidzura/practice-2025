using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

public class TetrisDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.Gray;

        const int rows = 20;
        const int cols = 10;
        const float cellSize = 30;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                canvas.FillRectangle(x * cellSize, y * cellSize, cellSize - 1, cellSize - 1);
            }
        }
    }
}

