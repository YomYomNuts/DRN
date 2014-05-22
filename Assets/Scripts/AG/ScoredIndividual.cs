 using UnityEngine;
using System.Collections;

class ScoredIndividual
{
    /// <summary>
    /// La configuration des cubes (solution)
    /// </summary>
    public IndividalGenetic Configuration { get; set; }

    /// <summary>
    /// Le score de la configuration ci-dessus
    /// </summary>
    public float Score { get; set; }
}