using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using KernelDLL.Network.Request;

namespace KernelDLL.Creation.Models
{
    [Serializable]
    public class AvatarToolsetModel
    {
        public AvatarToolsetModel(string race, string gender, EnumPlayerDataType dataType, string type, int skinIndex, int index)
        {
            Race = race;
            Gender = gender;
            DataType = dataType;
            Type = type;
            SkinIndex = skinIndex;
            Index = index;
            if (IsCharaset(DataType))
            {
                EncodeCharasetImage = EncodeImageToString(true);
            }

            if (IsFaceset(DataType))
            {
                EncodeFacesetImage = EncodeImageToString(false);
            }
        }

        public string Race { get; }
        public string Gender { get; }
        public EnumPlayerDataType DataType { get; }
        public string Type { get; }
        public int SkinIndex { get; }
        public int Index { get; }
        public string EncodeCharasetImage { get; }
        public string EncodeFacesetImage { get; }
        public Bitmap CharasetImage { get { return DecodeImageFromString(true); } }
        public Bitmap FacesetImage { get { return DecodeImageFromString(false); } }

        private string EncodeImageToString(bool isCharaset)
        {
            try
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                string resourcePath = string.Empty;
                if (isCharaset)
                {
                    switch (DataType)
                    {
                        case EnumPlayerDataType.AvatarBodyToolset:
                            resourcePath = $"KernelDLL.Resources.Player.Generator.Charaset.{Race}_{Gender}_{Type}{SkinIndex}.png";
                            break;

                        case EnumPlayerDataType.AvatarFrontHairToolset:
                        case EnumPlayerDataType.AvatarRearHairToolset:
                        case EnumPlayerDataType.AvatarEarsToolset:
                        case EnumPlayerDataType.AvatarBeardToolset:
                        case EnumPlayerDataType.AvatarExtrasToolset:
                            resourcePath = $"KernelDLL.Resources.Player.Generator.Charaset.{Race}_{Gender}_{Type}{Index}.png";
                            break;
                    }
                }
                else
                {
                    switch (DataType)
                    {
                        case EnumPlayerDataType.AvatarFaceToolset:
                            resourcePath =
                                $"KernelDLL.Resources.Player.Generator.Faceset.{Race}_{Gender}_{Type}{Index}_Skin{SkinIndex}.png";
                            break;
                        case EnumPlayerDataType.AvatarBaseHairToolset:
                        case EnumPlayerDataType.AvatarFrontHairToolset:
                        case EnumPlayerDataType.AvatarRearHairToolset:
                        case EnumPlayerDataType.AvatarEyebrowsToolset:
                        case EnumPlayerDataType.AvatarBaseEyesToolset:
                        case EnumPlayerDataType.AvatarEyesToolset:
                        case EnumPlayerDataType.AvatarEarsToolset:
                        case EnumPlayerDataType.AvatarNoseToolset:
                        case EnumPlayerDataType.AvatarMouthToolset:
                        case EnumPlayerDataType.AvatarBeardToolset:
                        case EnumPlayerDataType.AvatarExtrasToolset:
                            resourcePath =
                                $"KernelDLL.Resources.Player.Generator.Faceset.{Race}_{Gender}_{Type}{Index}.png";
                            break;
                    }
                }

                using (Stream myStream = myAssembly.GetManifestResourceStream(resourcePath))
                {
                    using (MemoryStream myMemoryStream = new MemoryStream())
                    {
                        myStream.CopyTo(myMemoryStream);
                        return Convert.ToBase64String(myMemoryStream.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private Bitmap DecodeImageFromString(bool isCharaset)
        {
            if (isCharaset && string.IsNullOrEmpty(EncodeCharasetImage)) return null;
            if (!isCharaset && string.IsNullOrEmpty(EncodeFacesetImage)) return null;

            byte[] imageAsBytes = System.Convert.FromBase64String(isCharaset ? EncodeCharasetImage : EncodeFacesetImage);
            using (MemoryStream myStream = new MemoryStream(imageAsBytes, 0, imageAsBytes.Length)) {
                Bitmap bm = new Bitmap(myStream);
                return bm;
            }
        }

        private bool IsCharaset(EnumPlayerDataType dataType)
        {
            switch (DataType)
            {
                case EnumPlayerDataType.AvatarBodyToolset:
                case EnumPlayerDataType.AvatarFrontHairToolset:
                case EnumPlayerDataType.AvatarRearHairToolset:
                case EnumPlayerDataType.AvatarEarsToolset:
                case EnumPlayerDataType.AvatarBeardToolset:
                case EnumPlayerDataType.AvatarExtrasToolset:
                    return true;
            }
            
            return false;
        }

        private bool IsFaceset(EnumPlayerDataType dataType)
        {
            switch (DataType)
            {
                case EnumPlayerDataType.AvatarFaceToolset:
                case EnumPlayerDataType.AvatarBaseHairToolset:
                case EnumPlayerDataType.AvatarFrontHairToolset:
                case EnumPlayerDataType.AvatarRearHairToolset:
                case EnumPlayerDataType.AvatarEyebrowsToolset:
                case EnumPlayerDataType.AvatarBaseEyesToolset:
                case EnumPlayerDataType.AvatarEyesToolset:
                case EnumPlayerDataType.AvatarEarsToolset:
                case EnumPlayerDataType.AvatarNoseToolset:
                case EnumPlayerDataType.AvatarMouthToolset:
                case EnumPlayerDataType.AvatarBeardToolset:
                case EnumPlayerDataType.AvatarExtrasToolset:
                    return true;
            }

            return false;
        }
    }
}
