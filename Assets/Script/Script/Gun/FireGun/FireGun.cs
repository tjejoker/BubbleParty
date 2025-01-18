public class FireGun : GunBase
{
    public float speed;
    
    // 直线发射
    protected override void Fire()
    {
        FireBullet.Create(shootPos.position,
            shootPos.rotation,
            new LineBulletMoveWay(shootPos.forward, speed));
    }
}