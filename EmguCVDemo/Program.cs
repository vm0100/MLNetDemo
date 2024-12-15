using System.Drawing;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics.CodeAnalysis;
using EmguCVDemo.Connection;


namespace EmguCVDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("TF_CPP_MIN_LOG_LEVEL", "3");

            // 加载图片
            string imagePath = @"C:\Users\Wei\Desktop\work\Snipaste_2024-12-15_21-54-39.png";
            var image = CvInvoke.Imread(imagePath, ImreadModes.Color);

            // 转换为灰度图
            var grayImageMat = new Mat();
            CvInvoke.CvtColor(image, grayImageMat, ColorConversion.Bgr2Gray);

            var grayImage = grayImageMat.ToImage<Gray, byte>();

            var cardConfig = new CardConfig();
            var gameConfig = new GameConfig();

            var cardGrid = new string[gameConfig.RowCount, gameConfig.ColCount];

            for (int row = 0; row < gameConfig.RowCount; row++)
            {
                var yOffset = 0;
                if (row != 0)
                {
                    yOffset += row * cardConfig.YSplit + row * cardConfig.Height;
                }
                for (int col = 0; col < gameConfig.ColCount; col++)
                {
                    var xOffset = 0;
                    if (col != 0)
                    {
                        xOffset += col * cardConfig.XSplit + col * cardConfig.Width;
                    }

                    var sliceCard = SliceCard(grayImage, new Point(gameConfig.StartLocation.X + xOffset, gameConfig.StartLocation.Y + yOffset), cardConfig.Size);
                    // CvInvoke.Imwrite($@"C:\Users\Wei\Desktop\work\slice\{x}_{y}.png", sliceCard);
                    var predictResult = LinkGameModel.Predict(new LinkGameModel.ModelInput()
                    {
                        ImageSource = CvInvoke.Imencode(".png", sliceCard.Mat) //sliceCard.Bytes,
                    });
                    cardGrid[row, col] = predictResult.PredictedLabel;
                }
            }

            IConnect connect = ConnectFactory.GetConnection(MoveType.Static);
            connect.Connection(cardGrid).ForEach(Console.WriteLine);
        }

        public static Image<Gray, byte> SliceCard(Image<Gray, byte> sourceImage, Point sliceLocation, Size sliceSize)
        {
            return sourceImage.GetSubRect(new Rectangle(sliceLocation, sliceSize));
        }
    }

    public class CardConfig
    {
        public int Height { get; set; } = 103;
        public int Width { get; set; } = 103;

        public int YSplit { get; set; } = 22;
        public int XSplit { get; set; } = 22;

        public Size Size { get; }

        public CardConfig()
        {
            Size = new Size(Width, Height);
        }
    }

    public class GameConfig
    {
        public int RowCount { get; set; } = 14;
        public int ColCount { get; set; } = 8;

        public Point StartLocation { get; set; } = new Point(96, 431);
    }


    public static class MatExtension
    {
        public static byte[] ToByteArray(this Mat mat)
        {
            return CvInvoke.Imencode(".png", mat);
        }
    }

}
