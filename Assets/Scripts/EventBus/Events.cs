using System;
using System.Collections.Generic;

public interface IEvent { }

public struct TestEvent : IEvent { }

public struct PlayerEvent : IEvent {
    public int health;
    public int mana;
}
public struct BalanceChangedEvent: IEvent {
    public float newBalance;
}

public struct EnemyDiedEvent: IEvent {}
public struct EnemyHitEvent: IEvent{
    public float damage;
}
public struct PlayerHitEvent: IEvent {
    public float newHealth;
}
public struct PlayerDiedEvent: IEvent {

}

public struct EnemySpottedInWideRangeEvent : IEvent {
    public List<EnemyController> enemiesInRange;
}

public struct EnemySpottedInShortRangeEvent : IEvent {
    public List<EnemyController> enemiesInRange;
}

public struct PlayerStateUpdatedEvent : IEvent {
    public PlayerState newState;
}