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
    public class Hog:IWorkingFrames
    {
        BitmapImage spriteHogImage = new BitmapImage();
        public Image hogOne = new Image();

        double count = 0;

        int totalHogFrames;
        int hogCurrentFrame;
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

        public int TotalHogFrames
        {
            get
            {
                return totalHogFrames;
            }
            set
            {
                totalHogFrames = value;
            }
        }

        public int HogCurrentFrame
        {
            get
            {
                return hogCurrentFrame;
            }
            set
            {
                hogCurrentFrame = value;
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

        public Hog(string path1, int frameY1, int width1, int height1, string mark1, int totalHogframes1, int hogCurrentFrame1)
        {
            Path = path1;
            FrameY = frameY1;
            Width = width1;
            Height = height1;
            Mark = mark1;
            TotalHogFrames = totalHogframes1;
            HogCurrentFrame = hogCurrentFrame1;
            Health = 3;
            Speed = 1;
        }

        public void UploadingImage()
        {
            spriteHogImage.BeginInit();
            spriteHogImage.UriSource = new Uri(Path, UriKind.RelativeOrAbsolute);
            spriteHogImage.EndInit();
        }

        public CroppedBitmap GetFrame(int frame)
        {
            int frameX = (frame % TotalHogFrames) * Width;
            return new CroppedBitmap(spriteHogImage, new System.Windows.Int32Rect(frameX, FrameY, Width, Height));
        }

        public void InitPictures()
        {
            hogOne.Tag = Mark;
            hogOne.Width = Width;
            hogOne.Height = Height;
            hogOne.Source = GetFrame(0);
        }

        //public void MoveLeft()
        //{
        //    HogCurrentFrame--;
        //    if (HogCurrentFrame < 0)
        //        HogCurrentFrame += TotalHogFrames;

        //    hogOne.Source = GetFrame(HogCurrentFrame);
        //    hogOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        //}

        public void MoveRight()
        {
            count += 0.2;
            if(count >=4)
            {
                HogCurrentFrame = (HogCurrentFrame + 1) % TotalHogFrames;
                count = 0;
            }
          
            hogOne.Source = GetFrame(HogCurrentFrame);
            //hogOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }
    }
}
