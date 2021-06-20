using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    public class Snake:IWorkingFrames
    {
        BitmapImage spriteSnakeImage = new BitmapImage();
        public Image snakeOne = new Image();
        int totalSnakeFrames;
        int snakeCurrentFrame;
        int health;
        int speed;
        string mark;
        string path;
        int width;
        int height;
        int frameY;

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        public int FrameY
        {
            get
            {
                return frameY;
            }
            set
            {
                frameY = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public string Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }

        public int TotalSnakeFrames
        {
            get
            {
                return totalSnakeFrames;
            }
            set
            {
                totalSnakeFrames = value;
            }
        }

        public int SnakeCurrentFrame
        {
            get
            {
                return snakeCurrentFrame;
            }
            set
            {
                snakeCurrentFrame = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > 0)
                    health = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public Snake(string path1, int frameY1, int width1, int height1, string mark1, int totalSnakeFrames1, int snakeCurrentFrame1)
        {
            Path = path1;
            FrameY = frameY1;
            Width = width1;
            Height = height1;
            Mark = mark1;
            TotalSnakeFrames = totalSnakeFrames1;
            SnakeCurrentFrame = snakeCurrentFrame1;
            Health = 2;
            Speed = 5;
        }

        public void UploadingImage()
        {
            spriteSnakeImage.BeginInit();
            spriteSnakeImage.UriSource = new Uri(Path, UriKind.RelativeOrAbsolute);
            spriteSnakeImage.EndInit();
        }

        public CroppedBitmap GetFrame(int frame)
        {
            int frameX = (frame % TotalSnakeFrames) * Width;
            return new CroppedBitmap(spriteSnakeImage, new System.Windows.Int32Rect(frameX, FrameY, Width, Height));
        }

        public void InitPictures()
        {
            snakeOne.Tag = Mark;
            snakeOne.Width = Width;
            snakeOne.Height = Height;
            snakeOne.Source = GetFrame(0);
        }

        public void MoveLeft()
        {
            SnakeCurrentFrame--;
            if (SnakeCurrentFrame < 0)
                SnakeCurrentFrame += TotalSnakeFrames;

            snakeOne.Source = GetFrame(SnakeCurrentFrame);
            snakeOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        }

        public void MoveRight()
        {
            SnakeCurrentFrame = (SnakeCurrentFrame + 1) % TotalSnakeFrames;
            snakeOne.Source = GetFrame(SnakeCurrentFrame);
            snakeOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }


        public CroppedBitmap ClippingFrame(int f)
        {
            const int w = 80;
            const int h = 80;
            int frameX2 = (f % 4) * w;
            return new CroppedBitmap(spriteSnakeImage, new System.Windows.Int32Rect(frameX2, 160, w, h));
        }

        public void AttackRight()
        {
            SnakeCurrentFrame = (SnakeCurrentFrame + 1) % 4;
            snakeOne.Source = ClippingFrame(SnakeCurrentFrame);
            snakeOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }

        public void AttackLeft()
        {
            SnakeCurrentFrame--;
            if (SnakeCurrentFrame < 0)
                SnakeCurrentFrame += 4;

            snakeOne.Source = ClippingFrame(SnakeCurrentFrame);
            snakeOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        }
    }
}
