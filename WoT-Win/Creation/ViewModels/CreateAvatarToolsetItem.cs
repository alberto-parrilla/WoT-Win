using System;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace WoT_Win.Creation.ViewModels
{
    public class CreateAvatarToolsetItem
    {
        public CreateAvatarToolsetItem(int id, Bitmap charasetImage, Bitmap facesetImage)
        {
            Id = id;
            CharasetImage = charasetImage == null ? null : Imaging.CreateBitmapSourceFromHBitmap(charasetImage.GetHbitmap(),IntPtr.Zero,System.Windows.Int32Rect.Empty,BitmapSizeOptions.FromWidthAndHeight(charasetImage.Width, charasetImage.Height));
            FacesetImage = facesetImage == null ? null : Imaging.CreateBitmapSourceFromHBitmap(facesetImage.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(facesetImage.Width, facesetImage.Height));
        }

        public int Id { get; }
        public BitmapSource CharasetImage { get; set; }
        public BitmapSource FacesetImage { get; set; }
    }
}
