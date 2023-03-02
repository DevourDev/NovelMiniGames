namespace Game.Fighting
{
    public sealed class TmpArmorComponent : TurnBasedTemporaryComponent
    {
        private FighterArmorSo _armor;
        private DefenceComponent _defCmp;


        public void InitTmpArmor(FighterArmorSo armor)
        {
            _armor = armor;
        }

        public override void OnAttach()
        {
            _defCmp = GetComponent<DefenceComponent>();
            _defCmp.AddArmor(_armor);
        }

        public override void OnDetach()
        {
            _defCmp.RemoveArmor(_armor);
        }
    }
}
