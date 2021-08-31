using System;
using System.Collections.Generic;

[Serializable]
public class Attack
{
    public Dictionary<DamageType, int> Damages { get; set; }

    private Attack()
    {
        Damages = new Dictionary<DamageType, int>();
    }

    public Attack(List<DamageType> attackTypes, List<int> damages) : this()
    {
        SetAttacks(attackTypes, damages);
    }

    private void SetAttacks(List<DamageType> attackTypes, List<int> damages)
    {
        if (attackTypes.Count != damages.Count)
            throw new InvalidOperationException("Both lists must have the same length");

        for (int i = 0; i < attackTypes.Count; i++)
        {
            if (damages[i] <= 0)
                throw new InvalidOperationException("Invalid damage: " + damages[i]);
            if (attackTypes[i] == DamageType.Unknown)
                throw new InvalidOperationException("Invalid type: " + attackTypes[i]);

            Damages.Add(attackTypes[i], damages[i]);
        }
    }
}

public enum DamageType
{
    Unknown = 0,
    Slash = 1,
    Chop = 2
}