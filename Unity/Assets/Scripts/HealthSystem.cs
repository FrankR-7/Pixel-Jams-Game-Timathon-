namespace DefaultNamespace
{
    public class HealthSystem
    {
        public float Health { get; private set; }
        public int maxHealth { get; set; }
        
        public HealthSystem(int max)
        {
            this.maxHealth = max;
            this.Health = max;
        }

        public void TakeDamage(int dmg)
        {
            this.Health -= dmg;
        }

        public void Heal(int heal)
        {
            this.Health += heal;
        }
    }
}