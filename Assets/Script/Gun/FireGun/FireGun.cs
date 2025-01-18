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

        var bullet = FireBullet.Create(shootPos.position, shootPos.rotation);
        bullet.Initialize(shootPos.right, speed);
        
        TimerInterval.Create(frequency, () => IsColdDown = false);
        // TODO: 播放音效
        AudioManager.Instance.PlayEffect(ResSvc.Instance.GetAudioClip("飞行/飞行1"));
    }
}