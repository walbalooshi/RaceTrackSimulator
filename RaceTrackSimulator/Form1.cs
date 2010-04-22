using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RaceTrackSimulator {
    public partial class Form1 : Form {
        Greyhound[] dogs = new Greyhound[4];
        Guy[] guys = new Guy[3];

        public Form1() {
            InitializeComponent();
            SetupRaceTrack();
        }

        private void SetupRaceTrack() {
            MinimumBet.Text = string.Format("Minimum bet {0:c}", (int)BetAmount.Minimum);

            int startingPosition = dog1.Right - racetrack.Left;
            int raceTrackLength = racetrack.Size.Width;

            dogs[0] = new Greyhound() { MyPictureBox = dog1,
                                        RacetrackLength = raceTrackLength, 
                                        StartingPosition = startingPosition };
            dogs[1] = new Greyhound() { MyPictureBox = dog2,
                                        RacetrackLength = raceTrackLength,
                                        StartingPosition = startingPosition };
            dogs[2] = new Greyhound() { MyPictureBox = dog3,
                                        RacetrackLength = raceTrackLength,
                                        StartingPosition = startingPosition };
            dogs[3] = new Greyhound() { MyPictureBox = dog4,
                                        RacetrackLength = raceTrackLength,
                                        StartingPosition = startingPosition };

            guys[0] = new Guy("Joe", null, 50, joeButton, joeBet);
            guys[1] = new Guy("Bob", null, 75, bobButton, bobBet);
            guys[2] = new Guy("Al", null, 45, alButton, alBet);

            foreach (Guy guy in guys) {
                guy.UpdateLabels();
            }
        }

        private void joeButton_CheckedChanged(object sender, EventArgs e) {
            SetBettorNameTextLabel("Joe");
        }

        private void bobButton_CheckedChanged(object sender, EventArgs e) {
            SetBettorNameTextLabel("Bob");
        }

        private void alButton_CheckedChanged(object sender, EventArgs e) {
            SetBettorNameTextLabel("Al");
        }

        private void SetBettorNameTextLabel(string Name) {
            BettorName.Text = Name;
        }

        private void Bets_Click(object sender, EventArgs e) {
            int GuyNumber = 0;

            if (joeButton.Checked) {
                GuyNumber = 0;
            }
            if (bobButton.Checked) {
                GuyNumber = 1;
            }
            if (alButton.Checked) {
                GuyNumber = 2;
            }

            guys[GuyNumber].PlaceBet((int)BetAmount.Value, (int)DogNumber.Value);
            guys[GuyNumber].UpdateLabels();
        }

        private void race_Click(object sender, EventArgs e) {
            bool NoWinner = true;
            int winningDog;
            race.Enabled = false;

            while (NoWinner) {
                Application.DoEvents();
                for (int i = 0; i < dogs.Length; i++ ) {
                    if (dogs[i].Run()) {
                        winningDog = i + 1;
                        NoWinner = false;
                        MessageBox.Show("We have a winner - dog #" + winningDog);
                        foreach (Guy guy in guys) {
                            if (guy.MyBet != null) {
                                guy.Collect(winningDog);
                                guy.MyBet = null;
                                guy.UpdateLabels();
                            }
                        }
                        foreach (Greyhound dog in dogs) {
                            dog.TakeStartingPosition();
                        }
                        break;
                    }                    
                }                
            }

            race.Enabled = true;
        }
    }
}
