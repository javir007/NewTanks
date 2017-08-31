using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAvatar
{
    AvatarTypes AvatarType { get; }
    Transform CachedTransform { get; }
    int Health { get; }
    int Id { get; }
	void SetAvatar(AvatarData data);


}

public enum AvatarTypes
{
    Player,
    AI
}

