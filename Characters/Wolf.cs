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
    public class Wolf:IWorkingFrames
    {
        BitmapImage spriteWolfImage = new BitmapImage();
        public Image wolfOne = new Image();
        int totalWolfFrames;
        int wolfCurrentFrame;
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

        public int TotalWolfFrames
        {
            get
            {
                return totalWolfFrames;
            }
            set
            {
                totalWolfFrames = value;
            }
        }

        public int WolfCurrentFrame
        {
            get
            {
                return wolfCurrentFrame;
            }
            set
            {
                wolfCurrentFrame = value;
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
                //if (value > 0)
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

        public Wolf(string path1, int frameY1, int width1, int height1, string mark1, int totalWolfframes1, int wolfCurrentFrame1)
        {
            //spriteWolfImage = new BitmapImage();
            //wolfOne = new Image();
            Path = path1;
            FrameY = frameY1;
            Width = width1;
            Height = height1;
            Mark = mark1;
            TotalWolfFrames = totalWolfframes1;
            WolfCurrentFrame = wolfCurrentFrame1;
            Health = 3;
            Speed = 5;
        }

        public void UploadingImage()
        {
            spriteWolfImage.BeginInit();
            spriteWolfImage.UriSource = new Uri(Path,UriKind.RelativeOrAbsolute);
            spriteWolfImage.EndInit();
        }

        public CroppedBitmap GetFrame(int frame)
        {
          
            int frameX = (frame % TotalWolfFrames) * Width;
            return new CroppedBitmap(spriteWolfImage, new System.Windows.Int32Rect(frameX, FrameY, Width, Height));

        }

        public void InitPictures()
        {
            wolfOne.Tag = Mark;
            wolfOne.Width = Width;
            wolfOne.Height = Height;
            wolfOne.Source = GetFrame(0);
            //Canvas.SetTop(wolfOne, 644);
            //Canvas.SetLeft(wolfOne, 1012);
            //GameWindow.MyCanvas.Children.Add(wolfOne);
        }

        public void MoveLeft()
        {
            WolfCurrentFrame--;
            if (WolfCurrentFrame < 0)
                WolfCurrentFrame += TotalWolfFrames;

            wolfOne.Source = GetFrame(WolfCurrentFrame);
            wolfOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        }

        public void MoveRight()
        {
            WolfCurrentFrame = (WolfCurrentFrame + 1) % TotalWolfFrames;
            wolfOne.Source = GetFrame(WolfCurrentFrame);
            wolfOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }

        public CroppedBitmap ClippingFrame(int f)
        {
            const int w = 190;
            const int h = 140;
            int frameX2 = (f % 3) * w;
            return new CroppedBitmap(spriteWolfImage, new System.Windows.Int32Rect(frameX2, 439, w, h));
        }

        public void AttackRight()
        {
            wolfOne.Width = 190;
            wolfOne.Height = 140;
            WolfCurrentFrame = (WolfCurrentFrame + 1) % 3;
            wolfOne.Source = ClippingFrame(WolfCurrentFrame);
            wolfOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }

        public void AttackLeft()
        {
            wolfOne.Width = 190;
            wolfOne.Height = 140;
            WolfCurrentFrame--;
            if(WolfCurrentFrame < 0)
            {
                WolfCurrentFrame += 3;
            }

            wolfOne.Source = ClippingFrame(WolfCurrentFrame);
            wolfOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        }
    }
}
