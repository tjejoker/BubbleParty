using Framework;

public class FireGun : GunBase
{
    public float speed;

    // 直线发射
    public override void Fire()
    {
        if (IsColdDown)
            return;

        IsColdDown = true;

        FireBullet.Create(shootPos.position,
            shootPos.rotation,
            new LineBulletMoveWay(shootPos.right, speed));

        TimerInterval.Create(frequency, () => { IsColdDown = false; });
    }
}