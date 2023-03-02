using DevourNovelEngine.Prototype.Characters.Expressions;
using DevourNovelEngine.Prototype.Variables;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Characters
{
    public abstract class CharacterOnSceneBase : MonoBehaviour, ICharacter
    {
        private CharacterReferenceSo _reference;
        private VariablesCollection _variablesCollection;
        private ExpressionSo _expression;


        public ICharacterReference Reference => _reference;
        public VariablesCollection VariablesCollection => _variablesCollection;

        public ExpressionSo Expression
        {
            get => _expression;
            set => ChangeExpression(value);
        }


        /// <summary>
        /// sender, prev, cur
        /// </summary>
        public event System.Action<CharacterOnSceneBase, ExpressionSo, ExpressionSo> OnExpressionChanged;


        internal void Init(CharacterReferenceSo reference)
        {
            _reference = reference;
            _variablesCollection = new();

            Init();
        }


        private void ChangeExpression(ExpressionSo value)
        {
            if (_expression == value)
                return;

            var prev = _expression;
            _expression = value;
            OnExpressionChanged?.Invoke(this, prev, value);
        }


        protected virtual void Init() { }
    }
}
