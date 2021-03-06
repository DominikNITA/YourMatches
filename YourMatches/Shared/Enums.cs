﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public enum Status { SCHEDULED, FINISHED, IN_PLAY, PAUSED, CANCELED, POSTPONED, SUSPENDED, AWARDED }
    public enum Result { NONE, HOME_TEAM, DRAW, AWAY_TEAM}
    public enum League { NONE, ENGLAND_1, GERMANY_1, SPAIN_1, FRANCE_1, ITALY_1}
}
