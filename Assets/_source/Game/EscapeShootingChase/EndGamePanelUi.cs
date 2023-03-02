using TMPro;
using UnityEngine;

namespace Game.EscapeShootingChase
{
    public class EndGamePanelUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _messageText;


        public TextMeshProUGUI TitleText => _titleText;
        public TextMeshProUGUI MessageText => _messageText;
    }
}
