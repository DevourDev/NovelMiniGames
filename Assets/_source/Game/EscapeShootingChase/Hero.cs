namespace Game.EscapeShootingChase
{
    public class Hero : Hittable<Projectile>
    {
        private int _lives;

        public int LivesLeft => _lives;
        public bool IsAlive => LivesLeft > 0;


        public event System.Action<Hero> OnHit;
        public event System.Action<Hero> OnDeath;


        public void InitHero(int livesCount)
        {
            _lives = livesCount;
        }


        protected override void HandleHit(Projectile hitter)
        {
            --_lives;

            OnHit?.Invoke(this);

            if (_lives <= 0)
            {
                OnDeath?.Invoke(this);
                return;
            }
        }
    }
}
