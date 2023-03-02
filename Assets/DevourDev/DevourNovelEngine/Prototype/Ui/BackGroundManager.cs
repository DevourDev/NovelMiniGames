using DevourNovelEngine.Prototype.Core.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace DevourNovelEngine.Prototype.Ui
{
    public class BackGroundManager : MonoBehaviour
    {
        [SerializeField] private Image _img;


        public void ChangeBackGround(Sprite sprite)
        {
            _img.sprite = sprite;
        }
    }
}
