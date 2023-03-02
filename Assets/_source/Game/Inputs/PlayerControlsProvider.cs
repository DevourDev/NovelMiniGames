using Game.Inputs;
using UnityEngine;

namespace Game
{
    public class PlayerControlsProvider : MonoBehaviour
    {
        public enum PlayerControlsActionMap
        {
            UpDown,
            Stealth,
            Fighting
        }


        private static PlayerControls _controls;

        private static int[] _mapsConsumersCount;

        //[SerializeField] private bool _controlsNotNull;
        //[SerializeField] private bool _controlsEnabled;
        //[SerializeField] private int _upDown;
        //[SerializeField] private int _stealth;
        //[SerializeField] private int _fighting;


        //private void Update()
        //{
        //    if (_mapsConsumersCount != null)
        //    {
        //        _upDown = _mapsConsumersCount[0];
        //        _stealth = _mapsConsumersCount[1];
        //        _fighting = _mapsConsumersCount[2];
        //    }

        //    if (_controls != null)
        //    {
        //        _controlsNotNull = true;
        //        _controlsEnabled = _controls.Fighting.enabled;
        //    }
        //}


        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //private static void Init()
        //{
        //    if (_controls == null || _controls.asset == null)
        //        _controls = null;

        //    if (_mapsConsumersCount != null)
        //        Array.Fill(_mapsConsumersCount, 0);
        //}

        public static PlayerControls Controls
        {
            get
            {
                if (_controls == null)
                {
                    var go = new GameObject(nameof(PlayerControlsProvider)).AddComponent<PlayerControlsProvider>();
                }

                return _controls;
            }
        }

        public static void AddConsumer(PlayerControlsActionMap map)
        {
            if (++_mapsConsumersCount[(int)map] == 1)
            {
                SetMapActiveStatus(map, true);
            }
        }

        public static void RemoveConsumer(PlayerControlsActionMap map)
        {
            if (--_mapsConsumersCount[(int)map] == 0)
            {
                SetMapActiveStatus(map, false);
            }
        }


        private static void SetMapActiveStatus(PlayerControlsActionMap map, bool enable)
        {
            UnityEngine.Debug.Log($"SetMapActiveStatus: {map}, {enable}");
            switch (map)
            {
                case PlayerControlsActionMap.UpDown:
                    if (enable)
                        Controls.UpDown.Enable();
                    else
                        Controls.UpDown.Disable();
                    break;
                case PlayerControlsActionMap.Stealth:
                    if (enable)
                        Controls.Stealth.Enable();
                    else
                        Controls.Stealth.Disable();
                    break;
                case PlayerControlsActionMap.Fighting:
                    if (enable)
                        Controls.Fighting.Enable();
                    else
                        Controls.Fighting.Disable();
                    break;
                default:
                    break;
            }
        }


        private void Awake()
        {
            _mapsConsumersCount = new int[System.Enum.GetValues(typeof(PlayerControlsActionMap)).Length];
            _controls = new();
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            _controls.Dispose();
        }


    }
}
