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
    public class Bear:IWorkingFrames
    {
        BitmapImage spriteBearImage = new BitmapImage();
        public Image bearOne = new Image();
        double current = 0;

        int totalBearFrames;
        int bearCurrentFrame;
        int health;
        double speed;
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

        public int TotalBearFrames
        {
            get
            {
                return totalBearFrames;
            }
            set
            {
                totalBearFrames = value;
            }
        }

        public int BearCurrentFrame
        {
            get
            {
                return bearCurrentFrame;
            }
            set
            {
                bearCurrentFrame = value;
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

        public double Speed
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

        public Bear(string path1, int frameY1, int width1, int height1, string mark1, int totalBearframes1, int bearCurrentFrame1)
        {
            Path = path1;
            FrameY = frameY1;
            Width = width1;
            Height = height1;
            Mark = mark1;
            TotalBearFrames = totalBearframes1;
            BearCurrentFrame = bearCurrentFrame1;
            Health = 5;
            Speed = 2;
        }

        public void UploadingImage()
        {
            spriteBearImage.BeginInit();
            spriteBearImage.UriSource = new Uri(Path, UriKind.RelativeOrAbsolute);
            spriteBearImage.EndInit();
        }

        public CroppedBitmap GetFrame(int frame)
        {
            int frameX = (frame % TotalBearFrames) * 140;
            return new CroppedBitmap(spriteBearImage, new System.Windows.Int32Rect(frameX+50, FrameY, Width, Height)); 
        }

        public void InitPictures()
        {
            bearOne.Tag = Mark;
            bearOne.Width = Width;
            bearOne.Height = Height;
            bearOne.Source = GetFrame(0);
        }

        //public void MoveRight()
        //{
        //    BearCurrentFrame-=1;
        //    if (BearCurrentFrame < 0)
        //        BearCurrentFrame += TotalBearFrames;
        //    bearOne.Source = GetFrame(BearCurrentFrame);
        //    //bearOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        //}

        public void MoveLeft()
        {
            //BearCurrentFrame+=1;
            //if(BearCurrentFrame > TotalBearFrames)
            //{
            //    BearCurrentFrame = 0;
            //}
            current += 0.2;
            if(current >=1)
            {
                BearCurrentFrame = (BearCurrentFrame + 1) % TotalBearFrames;
                current = 0;
            }
            
            bearOne.Source = GetFrame(BearCurrentFrame);
            //bearOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }

        public CroppedBitmap ClippingFrame(int f)
        {
            const int w = 180;
            const int h = 160;
            int frameX2 = (f % 3) * w;
            return new CroppedBitmap(spriteBearImage, new System.Windows.Int32Rect(frameX2, 370, w, h));
        }

        public void AttackLeft()
        {
            bearOne.Width = 180;
            bearOne.Height = 160;

            current += 0.2;
            if (current >= 3)
            {
                BearCurrentFrame = (BearCurrentFrame + 1) % 3;
                current = 0;
            }
            bearOne.Source = ClippingFrame(BearCurrentFrame);
            //bearOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }

        //public void AttackRight()
        //{
        //    bearOne.Width = 180;
        //    bearOne.Height = 160;
        //    BearCurrentFrame--;
        //    if (BearCurrentFrame < 0)
        //        BearCurrentFrame += 3;

        //    bearOne.Source = ClippingFrame(BearCurrentFrame);
        //    bearOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        //}
    }
}
