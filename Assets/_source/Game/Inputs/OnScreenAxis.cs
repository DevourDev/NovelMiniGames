using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Game
{
    public class OnScreenAxis : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private float _movementRange = 50f;

        [InputControl(layout = "Vector2")]
        [SerializeField] private string _controlPath;

        private Vector3 _startPos;
        private Vector2 _pointerDownPos;


        protected override string controlPathInternal { get => _controlPath; set => _controlPath = value; }


        private void Start()
        {
            _startPos = ((RectTransform)transform).anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (transform.parent.GetComponentInParent<RectTransform>(),
                eventData.position, eventData.pressEventCamera, out _pointerDownPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
            var delta = position - _pointerDownPos;
            delta.x = 0;
            delta = Vector2.ClampMagnitude(delta, _movementRange);
            ((RectTransform)transform).anchoredPosition = _startPos + (Vector3)delta;

            var newPos = new Vector2(delta.x / _movementRange, delta.y / _movementRange);
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ((RectTransform)transform).anchoredPosition = _startPos;
            SendValueToControl(Vector2.zero);
        }
    }
}
