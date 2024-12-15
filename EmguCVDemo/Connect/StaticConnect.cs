using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguCVDemo.Connection;

public class StaticConnect : IConnect
{
    public List<string> Connection(string[,] grid)
    {
        return new List<string>();
    }

    public bool CanConnection(string[,] grid, int x1, int y1, int x2, int y2)
    {
        // 检查是否越界
        if (x1 < 0 || x1 >= grid.GetLength(1) || y1 < 0 || y1 >= grid.GetLength(0) ||
            x2 < 0 || x2 >= grid.GetLength(1) || y2 < 0 || y2 >= grid.GetLength(0))
        {
            return false;
        }

        // 检查是否是同一个词语
        if (grid[y1, x1] != grid[y2, x2])
        {
            return false;
        }

        // 检查是否相邻
        if (Math.Abs(x1 - x2) + Math.Abs(y1 - y2) == 1)
        {
            return true;
        }

        // 检查L形或直线连接
        int dx = Math.Abs(x1 - x2);
        int dy = Math.Abs(y1 - y2);
        int min = dx > dy ? dx : dy;
        int max = dx > dy ? dy : dx;

        // 检查中间是否有障碍物
        for (int i = 1; i < max; i++)
        {
            if (min == dx && x1 + i * (x2 > x1 ? 1 : -1) != x2 && y1 != y2 + i * (y2 > y1 ? 1 : -1) ||
                min == dy && y1 + i * (y2 > y1 ? 1 : -1) != y2 && x1 != x2 + i * (x2 > x1 ? 1 : -1))
            {
                // 检查是否有词语阻挡
                if (grid[y1 + i * (y2 > y1 ? 1 : -1), x1] != null && grid[y1 + i * (y2 > y1 ? 1 : -1), x1] != grid[y1, x1] ||
                    grid[y1, x1 + i * (x2 > x1 ? 1 : -1)] != null && grid[y1, x1 + i * (x2 > x1 ? 1 : -1)] != grid[y1, x1])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
