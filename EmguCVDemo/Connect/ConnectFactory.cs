using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguCVDemo.Connection
{
    public class ConnectFactory
    {
        public static IConnect GetConnection(MoveType moveType)
        {
            return moveType switch
            {
                MoveType.Up => throw new NotImplementedException(),
                MoveType.Down => throw new NotImplementedException(),
                MoveType.Left => throw new NotImplementedException(),
                MoveType.Right => throw new NotImplementedException(),
                MoveType.TopBottomCenter => throw new NotImplementedException(),
                MoveType.TopBottomSplay => throw new NotImplementedException(),
                MoveType.LeftRightCenter => throw new NotImplementedException(),
                MoveType.LeftRightSplay => throw new NotImplementedException(),
                _ => new StaticConnect()
            };
        }
    }

    public enum MoveType
    {
        Static,
        Up,
        Down,
        Left,
        Right,
        TopBottomCenter,
        TopBottomSplay, 
        LeftRightCenter,
        LeftRightSplay,
    }

}
