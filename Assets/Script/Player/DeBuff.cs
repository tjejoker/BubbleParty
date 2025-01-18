using Framework;
using UnityEngine;

public abstract class DeBuff
{
    public abstract void Set(Player player, float size);
    public abstract void Update(float dt);
}


public class FreezeDeBuff : DeBuff
{
    public const string Key = "FreezeDeBuff";
    private Player _player;
    private float _originalSpeed;

    private float _duration;
    private float _ratio;

    public override void Set(Player player, float size)
    {
        _ratio += size;
        _duration += size * 2f;
        _duration = Mathf.Clamp(_duration, 0, 10);

        if (_player != null) return;

        _player = player;
        _originalSpeed = player.speed;
    }

    public override void Update(float dt)
    {
        _duration -= dt;
        _ratio -= dt * 0.03f;
        _ratio = Mathf.Clamp(_ratio, 0f, 1f);
        _player.speed = _originalSpeed * (1 - _ratio);


        if (_duration > 0)
            return;
        Done();
    }


    private void Done()
    {
        _player.speed = _originalSpeed;
        _player.DeleteDeBuff(Key);
    }
}