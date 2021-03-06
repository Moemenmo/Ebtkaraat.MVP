﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ebtkaraat.MVP
{
    class BasketballPlayer :Player
    {
        public List<int> ScoredPoints { get; set; }
        public List<int> Rebounds { get; set; }
        public List<int> Assists { get; set; }
        public BasketballPlayer(string name, string nick, int number, string team, string position, int scoredPoints, int rebounds,int assists)
            : base(name, nick, number, team, position)
        {
            ScoredPoints = new List<int>();
            Rebounds = new List<int>();
            Assists = new List<int>();
            ScoredPoints.Add(scoredPoints);
            Rebounds.Add(rebounds);
            Assists.Add(assists);
            Points = 0;
        }

        public override int CalculatePoints()
        {
            if (Position[^1] == "G")
            {
               int res = (ScoredPoints[^1] * 2) + (Rebounds[^1] * 3) + (Assists[^1]);
                Points += res;
                return res;
            }
            else if (Position[^1] == "F")
            {
                int res = (ScoredPoints[^1] * 2) + (Rebounds[^1] * 2) + (Assists[^1]*2);
                Points += res;
                return res;

            }
            else if (Position[^1] == "C")
            {
                int res = (ScoredPoints[^1] * 2) + (Rebounds[^1] * 1) + (Assists[^1]*3);
                Points += res;
                return res;
            }
            return -1;
        }
        public void AddMatch(string team, string pos ,int Sp,int reb,int assist)
        {
            
            Team.Add(team);
            Position.Add(pos);
            ScoredPoints.Add(Sp);
            Rebounds.Add(reb);
            Assists.Add(assist);
        }
    }
}
//scored points;rebounds;assists   baskert