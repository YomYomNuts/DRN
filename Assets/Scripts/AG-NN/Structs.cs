using UnityEngine;
using System.Collections;

public class WeaponType
{
    public ElementGeneticWeapon Scale;
    public NeuronalNetwork Network;
    public int Score;

    public WeaponType()
    {
        this.Scale = new ElementGeneticWeapon();
        this.Network = new NeuronalNetwork();
        this.Score = 0;
    }

    public WeaponType(ElementGeneticWeapon scale, NeuronalNetwork network)
    {
        this.Scale = scale;
        this.Network = network;
        this.Score = 0;
    }
}

public class WillType
{
    public ElementGeneticWill ScaleWills;
    public int Score;

    public WillType()
    {
        this.ScaleWills = new ElementGeneticWill();
        this.Score = 0;
    }

    public WillType(ElementGeneticWill scaleWills)
    {
        this.ScaleWills = scaleWills;
        this.Score = 0;
    }
}
