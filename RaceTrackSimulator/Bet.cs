using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceTrackSimulator {
    public class Bet {
        public int Amount;
        public int Dog;
        public Guy Bettor;

        public Bet(int Amount, int Dog, Guy Bettor) {
            this.Amount = Amount;
            this.Dog = Dog;
            this.Bettor = Bettor;
        }

        public string GetDescription() {
            string description = "";

            if(Amount > 0) {
                description = String.Format("{0} bets {1} on dog #{2}",
                    Bettor.Name, Amount, Dog);
            } else {
                description = String.Format("{0} hasn't placed any bets", 
                    Bettor.Name);
            }
            return description;
        }

        public int PayOut(int Winner) {
            if (Dog == Winner) {
                return Amount;
            }
            return -Amount;
        }
    }
}
