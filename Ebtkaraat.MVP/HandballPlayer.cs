using System;
using System.Collections.Generic;
using System.Text;

namespace Ebtkaraat.MVP
{
    class HandballPlayer : Player
    {
        public List<int> GoalsMade { get; set; }
        public List<int> GoalsReceived { get; set; }
        
        public HandballPlayer(string name, string nick, int number, string team, string position,int goalsMade,int goalsReceived) 
            :base( name, nick,  number, team,  position)
        {
            GoalsMade = new List<int>();
            GoalsReceived = new List<int>();
            GoalsMade.Add( goalsMade);
            GoalsReceived.Add( goalsReceived);
        }
        asdasd
        public override int CalculatePoints()
        {
            if (Position[^1] == "G")
            {
                int res = 50 + (GoalsMade[^1] * 5) - (GoalsReceived[^1]*2);
                Points += res;
                return res;
            }
            else // Position[Position.Count - 1] == "F"
            {
                int res =20 + (GoalsMade[^1]) - (GoalsReceived[^1]);
                Points += res;
                return res;

            }
        }
        public void AddMatch(string team, string pos, int GM, int GR)
        {
            Team.Add(team);
            Position.Add(pos);
            GoalsMade.Add(GM);
            GoalsReceived.Add(GR);
        }
    }
}
//goals made;goals received   hand 