using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models.Enums
{
    // enumerators are not entities in the db
    // enumerators are string representations of int values
    // in the DB -> int

    public enum  Prediction
    {
        Draw = 0,
        Win = 1,
        Lose = 2


    }
}
