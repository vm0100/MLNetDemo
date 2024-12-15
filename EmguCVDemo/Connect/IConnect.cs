using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguCVDemo.Connection;

public interface IConnect
{
    public List<string> Connection(string[,] grid);

    protected bool CanConnection(string[,] grid, int x1, int y1, int x2, int y2);
}
