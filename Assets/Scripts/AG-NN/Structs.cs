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

public class WheelType
{
    public ElementGeneticWheel ScaleWheels;
    public int Score;

    public WheelType()
    {
        this.ScaleWheels = new ElementGeneticWheel();
        this.Score = 0;
    }

    public WheelType(ElementGeneticWheel scaleWheels)
    {
        this.ScaleWheels = scaleWheels;
        this.Score = 0;
    }
}
