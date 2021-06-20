using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    interface IWorkingFrames
    {
        void UploadingImage();
        CroppedBitmap GetFrame(int f);
        void InitPictures();
    }
}
