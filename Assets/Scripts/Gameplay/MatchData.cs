using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchData 
{
    public static MatchData Instance = new MatchData();

    public List<IAvatar> Enemies = new List<IAvatar>();
    public IAvatar Me;
}
