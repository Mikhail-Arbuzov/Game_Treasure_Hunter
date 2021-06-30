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
    public class Bird:IWorkingFrames
    {
        BitmapImage spriteBirdImage = new BitmapImage();
        public Image birdOne = new Image();
        double countActions = 0;
        
        int totalBirdFrames;
        int birdCurrentFrame;
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

        public int TotalBirdFrames
        {
            get
            {
                return totalBirdFrames;
            }
            set
            {
                totalBirdFrames = value;
            }
        }

        public int BirdCurrentFrame
        {
            get
            {
                return birdCurrentFrame;
            }
            set
            {
                birdCurrentFrame = value;
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

        public Bird(string path1, int frameY1, int width1, int height1, string mark1, int totalBirdframes1, int birdCurrentFrame1)
        {
            Path = path1;
            FrameY = frameY1;
            Width = width1;
            Height = height1;
            Mark = mark1;
            TotalBirdFrames = totalBirdframes1;
            BirdCurrentFrame = birdCurrentFrame1;
            Health = 2;
            Speed = 3.5;
        }

        public void UploadingImage()
        {
            spriteBirdImage.BeginInit();
            spriteBirdImage.UriSource = new Uri(Path, UriKind.RelativeOrAbsolute);
            spriteBirdImage.EndInit();
        }

        public CroppedBitmap GetFrame(int frame)
        {
            int frameX = (frame % TotalBirdFrames) * Width;
            return new CroppedBitmap(spriteBirdImage, new System.Windows.Int32Rect(frameX, FrameY, Width, Height));
        }

        public void InitPictures()
        {
            birdOne.Tag = Mark;
            birdOne.Width = Width;
            birdOne.Height = Height;
            birdOne.Source = GetFrame(0);
        }

        //public void MoveLeft()
        //{
        //    BirdCurrentFrame--;
        //    if (BirdCurrentFrame < 0)
        //        BirdCurrentFrame += TotalBirdFrames;

        //    birdOne.Source = GetFrame(BirdCurrentFrame);
        //    birdOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
        //}

        public void MoveRight()
        {
            countActions += 0.2;
            if(countActions >= 1)
            {
                BirdCurrentFrame = (BirdCurrentFrame + 1) % TotalBirdFrames;
                countActions = 0;
            }
            //BirdCurrentFrame += 1;
            //if (BirdCurrentFrame > TotalBirdFrames)
            //{
            //    BirdCurrentFrame = 0;
            //}
            birdOne.Source = GetFrame(BirdCurrentFrame);
            //birdOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
        }
    }
}
