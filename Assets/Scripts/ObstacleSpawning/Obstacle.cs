public class Obstacle : Flyweight, ICargoDamager
{
    new ObstacleSettings Settings => (ObstacleSettings)base.Settings;
    public Cargo[] GetDamagableCargo() => Settings.DamagesCargo;
}
