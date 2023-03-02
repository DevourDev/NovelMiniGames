using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Entities
{
    public sealed class RenImageConverter : IConverter<RenImage, Sprite>
    {
        private readonly RenImagesManager _imagesManager;


        public RenImageConverter(RenImagesManager imagesManager)
        {
            _imagesManager = imagesManager;
        }


        public Sprite Convert(RenImage from)
        {
            return _imagesManager.FindByName(from.Name);
        }
    }
}
