using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ebtkaraat.MVP
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            System.IO.StreamReader file1 = new System.IO.StreamReader(@"C:\Users\Moamen\Desktop\Ebtkaraat.MVP\Match1.txt");
            System.IO.StreamReader file2 = new System.IO.StreamReader(@"C:\Users\Moamen\Desktop\Ebtkaraat.MVP\Match2.txt");
            System.IO.StreamReader file3 = new System.IO.StreamReader(@"C:\Users\Moamen\Desktop\Ebtkaraat.MVP\HandBall 1.txt");
            System.IO.StreamReader file4 = new System.IO.StreamReader(@"C:\Users\Moamen\Desktop\Ebtkaraat.MVP\HandBall 2.txt");
            List<StreamReader> files = new List<StreamReader>
            {
                file1,
                file2,
                file3,
                file4
            };
            string type = "";
            bool basketball = true;
            bool handball = true;
            List<BasketballPlayer> basketballPlayers = new List<BasketballPlayer>();
            List<HandballPlayer> handballPlayers = new List<HandballPlayer>();
            foreach (var file in files)
            {
                int count = 0;

                string team1Name = null, team2Name = null;
                int team1Points = 0, team2Points = 0;
                List<BasketballPlayer> fileBasketballPlayers = new List<BasketballPlayer>();
                List<HandballPlayer> fileHandballPlayers = new List<HandballPlayer>();
                int team1GoalRecived = 0, team2GoalRecived = 0;
                string winner;
                int maxPoints = 0, maxPointIndex = 0;
                while ((line = file.ReadLine()) != null)
                {
                    if (count == 0) 
                    {
                        type = line;
                    }// get headline of file 
                    else
                    {
                        string[] tokens = line.Split(';');
                        if (type == "BASKETBALL" && basketball)
                        {

                            if ((tokens.Length) == 8)
                            {

                                if (String.IsNullOrEmpty(team1Name))
                                {
                                    team1Name = tokens[3];
                                }
                                else if (String.IsNullOrEmpty(team2Name) && team1Name != tokens[3])
                                {
                                    team2Name = tokens[3];
                                }
                                if (tokens[3] == team1Name || tokens[3] == team2Name)
                                {
                                    #region check type of tokens
                                    int num, reb, score, assist;

                                    try
                                    {
                                        num = Convert.ToInt32(tokens[2]);
                                        score = Convert.ToInt32(tokens[5]);
                                        reb = Convert.ToInt32(tokens[6]);
                                        assist = Convert.ToInt32(tokens[7]);
                                    }
                                    catch  // tokens of this row doesn't match standards as it miss the order 
                                    {
                                        basketball = false;
                                        break;
                                    }
                                    #endregion
                                    BasketballPlayer bPlayer = basketballPlayers.FirstOrDefault(a => a.NickName == tokens[1]);
                                    if (bPlayer == null) // player in the players list
                                    {
                                        bPlayer = new BasketballPlayer(tokens[0], tokens[1], num, tokens[3], tokens[4], score, reb, assist);
                                        basketballPlayers.Add(bPlayer);
                                    }
                                    else // add  new player to players list
                                    { bPlayer.AddMatch(tokens[3], tokens[4], score, reb, assist); }
                                    fileBasketballPlayers.Add(bPlayer);
                                    if (bPlayer.Team[^1] == team1Name)
                                    {
                                        team1Points += score;
                                    }
                                    else // player from another team
                                    {
                                        team2Points += score;
                                    }
                                }
                                else // tokens of this row doesn't match standards as token number 4 isn't the team token
                                    basketball = false;
                            }
                            else // tokens of this row doesn't match standards as count !=7
                                basketball = false;
                        }
                        else if (type == "HANDBALL" && handball)
                        {
                            int num, goalM, goalR;
                            List<int> intTokens = new List<int>();

                            if ((tokens.Length) == 7)
                            {
                                #region check type of tokens

                                try
                                {
                                    num = Convert.ToInt32(tokens[2]);
                                    goalM = Convert.ToInt32(tokens[5]);
                                    goalR = Convert.ToInt32(tokens[6]);
                                }
                                catch  // tokens of this row doesn't match standards as it miss the order 
                                {
                                    handball = false;
                                    break;
                                }
                                #endregion
                                #region determine team and each team goal conceded 

                                if (String.IsNullOrEmpty(team1Name))
                                {
                                    team1Name = tokens[3];
                                    team1GoalRecived = goalR;
                                }
                                else if (String.IsNullOrEmpty(team2Name) && team1Name != tokens[3])
                                {
                                    team2Name = tokens[3];
                                    team2GoalRecived = goalR;

                                }
                                #endregion
                                if (tokens[3] == team1Name || tokens[3] == team2Name)
                                {

                                    HandballPlayer bPlayer = handballPlayers.FirstOrDefault(a => a.NickName == tokens[1]);
                                    if (bPlayer == null) // player in the players list
                                    {
                                        bPlayer = new HandballPlayer(tokens[0], tokens[1], num, tokens[3], tokens[4], goalM, goalR);
                                        handballPlayers.Add(bPlayer);
                                    }
                                    else // add  new player to players list
                                    { bPlayer.AddMatch(tokens[3], tokens[4], goalM, goalR); }
                                    fileHandballPlayers.Add(bPlayer);
                                    #region calculate final score points

                                    if (bPlayer.Team[^1] == team1Name)
                                    {
                                        team1Points += goalM;
                                    }
                                    else // player from another team
                                    {
                                        team2Points += goalM;
                                    }
                                    #endregion


                                }
                                else // tokens of this row doesn't match standards as token number 4 isn't the team token
                                    handball = false;
                            }
                            else // tokens of this row doesn't match standards as count !=7
                                handball = false;

                        }
                    } // body of file 
                    count++;
                }
                #region calculate player's scores for each match
                if (type == "HANDBALL")
                {
                    if (team1Points != team2GoalRecived || team2Points != team1GoalRecived)
                    {
                        handball = false;
                    }
                    if (handball)
                    {
                        
                            if (team1Points > team2Points)
                                winner = team1Name;
                            else // team2 wins
                                winner = team2Name;
                            foreach (var player in fileHandballPlayers)
                            {
                                int points = player.CalculatePoints();
                                if (points > maxPoints && player.Team[^1] == winner)
                                {
                                    maxPoints = points;
                                    maxPointIndex = fileHandballPlayers.FindIndex(a => a.NickName == player.NickName);
                                }
                            }
                            fileHandballPlayers[maxPointIndex].Points += 10;
                        
                    }
                }
                #endregion
                #region calculate player's score for each match

                else if (basketball && type == "BASKETBALL")
                {
                    if (team1Points > team2Points)
                        winner = team1Name;
                    else // team2 wins
                        winner = team2Name;
                    foreach (var player in fileBasketballPlayers)
                    {
                        int points = player.CalculatePoints();
                        if (points > maxPoints && player.Team[^1] == winner)
                        {
                            maxPoints = points;
                            maxPointIndex = fileBasketballPlayers.FindIndex(a => a.NickName == player.NickName);
                        }
                    }
                    fileBasketballPlayers[maxPointIndex].Points += 10;
                }

            }
            #endregion

            #region get MVP for each game
            if (handball)
            {


                var mvpHand = handballPlayers.Find(a => a.Points == handballPlayers.Max(a => a.Points));
                
                    Console.WriteLine(mvpHand.NickName);
                    Console.WriteLine(mvpHand.Points);

               

            }
            else
                Console.WriteLine("error in file system ");

            if (basketball)
            {


                var mvpbasket = basketballPlayers.Find(a => a.Points == basketballPlayers.Max(a => a.Points));
                Console.WriteLine(mvpbasket.NickName);
                Console.WriteLine(mvpbasket.Points);
            }
            else
                Console.WriteLine("error in file system ");
            #endregion

        }
    }
}
