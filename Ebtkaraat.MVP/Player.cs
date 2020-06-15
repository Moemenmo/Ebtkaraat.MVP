using System;
using System.Collections.Generic;
using System.Text;

namespace Ebtkaraat.MVP
{
    public abstract class Player
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public int Number { get; set; }
        public List<string> Team { get; set; }
        public List<string> Position { get; set; }
        public int Points { get; set; }
        public Player(string name, string nick,int number, string team, string position)
        {
            Team = new List<string>();
            Position = new List<string>();
            Name = name;
            NickName = nick;
            Number = number;
            Team.Add(team);
            Position.Add(position);
        }
        public abstract int CalculatePoints(); 
    }
}
//player name;nickname;number;team name;position;goals made;goals received   hand 
//player name;nickname;number;team name;position;scored points;rebounds;assists   baskert
