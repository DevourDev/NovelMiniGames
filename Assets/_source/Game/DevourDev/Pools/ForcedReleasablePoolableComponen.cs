namespace DevourDev.Pools
{
    public sealed class ForcedReleasablePoolableComponen : ForcedReleasableComponent
    {
        public override void Release()
        {
            SendMessage("ReturnToPool");
        }
    }
}
