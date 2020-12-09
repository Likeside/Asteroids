namespace Asteroids
{
    public class HpModelEventArgs
    {
        public float Hp
        {
            get;
            private set;
        }

        public HpModelEventArgs(float hp)
        {
            this.Hp = hp;
        }
    }
}