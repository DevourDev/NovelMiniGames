using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevourDev.Unity.Utils;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Ui
{
    public class ItemsOnSceneManager : MonoBehaviour
    {
        private readonly struct SpriteOnScene
        {
            private readonly Sprite _sprite;
            private readonly RelativePosition _position;


            public SpriteOnScene(Sprite sprite, RelativePosition position)
            {
                _sprite = sprite;
                _position = position;
            }


            public Sprite Sprite => _sprite;
            public RelativePosition Position => _position;


            public override bool Equals(object obj)
            {
                return obj is SpriteOnScene otherSos
                       && otherSos._sprite.GetInstanceID() == _sprite.GetInstanceID();

                return obj is SpriteOnScene scene &&
                       EqualityComparer<Sprite>.Default.Equals(_sprite, scene._sprite) &&
                       EqualityComparer<RelativePosition>.Default.Equals(_position, scene._position);
            }

            public override int GetHashCode()
            {
                return _sprite.GetInstanceID();

                return HashCode.Combine(_sprite, _position);
            }
        }


        [SerializeField] private BlockingTarget _waitingTarget;
        [SerializeField] private SceneBounderOld _sceneBounder;
        [SerializeField] private float _translationTime = 0.8f;

        private Dictionary<ICharacterReference, CharacterOnScene2D> _characters;
        private Dictionary<SpriteOnScene, SpriteRenderer> _otherItems;


        private void Awake()
        {
            _characters = new();
            _otherItems = new();
        }


        public void RemoveSpriteFromPosition(RelativePosition relativePosition)
        {
            foreach (var item in _otherItems.Keys)
            {
                if (item.Position == relativePosition)
                {
                    RemoveSpriteSimple(item.Sprite, relativePosition);
                    break;
                }
            }
        }

        public void PlaceSpriteSimple(Sprite sprite, RelativePosition relativePosition)
        {
            SpriteOnScene spriteOnScene = new(sprite, relativePosition);

            foreach (var item in _otherItems.Keys)
            {
                if (item.Position == spriteOnScene.Position
                    || item.Sprite == spriteOnScene.Sprite)
                {
                    RemoveSpriteSimple(item.Sprite, item.Position);
                    break;
                }
            }

            Vector3 pos = RelativePosition.RelativeToAbsolute(relativePosition, _sceneBounder);

            if (_otherItems.TryGetValue(spriteOnScene, out var renderer))
            {
                TranslateFromTo(renderer.transform, renderer.transform.position, pos, _translationTime);
                return;
            }

            renderer = new GameObject(sprite.name).AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            _otherItems.Add(spriteOnScene, renderer);

            TranslateFromTo(renderer.transform, pos - Vector3.up * 1.5f, pos, _translationTime);
        }


        public void RemoveSpriteSimple(Sprite sprite, RelativePosition relativePosition)
        {
            SpriteOnScene spriteOnScene = new(sprite, relativePosition);
            if (!_otherItems.TryGetValue(spriteOnScene, out var renderer))
            {
                Debug.LogError($"sprite to hide ({sprite.name}) not exists");
                return;
            }

            _otherItems.Remove(spriteOnScene);
            var tr = renderer.transform;
            TranslateFromTo(tr, tr.position, tr.position - Vector3.up * 1.5f, _translationTime, (x) => Destroy(x.gameObject));
        }

        public void RemoveCharacterSimple(HideCharacterSo hideCharacter)
        {
            if (!_characters.TryGetValue(hideCharacter.Character, out var charOnScene))
            {
                Debug.LogError("sprite to hide not exists");
                return;
            }

            _characters.Remove(hideCharacter.Character);
            var tr = charOnScene.transform;
            TranslateFromTo(tr, tr.position, tr.position - Vector3.up * 1.5f, _translationTime, (x) => Destroy(x.gameObject));
        }

        public void PlaceCharacterSimple(ShowCharacterSo showCharacterCommand)
        {
            Vector3 pos = RelativePosition.RelativeToAbsolute(showCharacterCommand.Position, _sceneBounder);

            if (_characters.TryGetValue(showCharacterCommand.Character, out var charOnScene))
            {
                TranslateFromTo(charOnScene.transform, charOnScene.transform.position, pos, _translationTime);
                return;
            }

            charOnScene = (CharacterOnScene2D)showCharacterCommand.Character.CreateCharacter();
            _characters.Add(charOnScene.Reference, charOnScene);

            TranslateFromTo(charOnScene.transform, pos - Vector3.down * 1.5f, pos, _translationTime);
        }

        public void PlaceCharacter(CharacterOnScene2D characterInstance, RelativePosition position)
        {
            if (_characters.TryGetValue(characterInstance.Reference, out var oldChar))
            {
                Debug.LogError($"Character with reference {characterInstance.Reference} already exists");
                bool instanceAreTheSame = characterInstance.gameObject.GetInstanceID() == oldChar.gameObject.GetInstanceID();
                Debug.LogError($"Instances are {(instanceAreTheSame ? "the same" : "different")}");
            }

            characterInstance.transform.position = RelativePosition.RelativeToAbsolute(position, _sceneBounder);
            characterInstance.gameObject.SetActive(true);
        }


        private void TranslateFromTo(Transform transform, Vector3 from, Vector3 to,
            float time, System.Action<Transform> callback = null)
        {
            _waitingTarget.RegisterTask();
            StartCoroutine(TranslateFromTo_Co(transform, from, to, time, callback));
        }

        private IEnumerator TranslateFromTo_Co(Transform transform, Vector3 from, Vector3 to,
            float time, System.Action<Transform> callback)
        {
            for (float left = time; left > 0f; left -= Time.deltaTime)
            {
                float t = 1f - left / time;
                transform.position = Vector3.Lerp(from, to, t);
                yield return null;
            }

            transform.position = to;
            callback?.Invoke(transform);
            _waitingTarget.UnregisterTask();
        }
    }
}
