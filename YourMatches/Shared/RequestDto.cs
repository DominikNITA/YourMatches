using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YourMatches.Shared
{
    public class RequestDto : IValidatableObject
    {
        private readonly static List<Status> _alwaysSelectedStatus = new List<Status>() { Status.POSTPONED, Status.CANCELED };
        public List<Status> StatusChecked { get; set; } = new List<Status>(_alwaysSelectedStatus);
        public List<League> LeaguesChecked { get; set; } = new List<League>();
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public bool IsEndingDateSelected { get; set; } = false;
        public RequestDto()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();
            if (StatusChecked.Count <= _alwaysSelectedStatus.Count)
            {
                yield return new ValidationResult("You must select at least one match state.", new[] { nameof(StatusChecked) });
            }
            if (LeaguesChecked.Count == 0)
            {
                yield return new ValidationResult("You must select at least one league.", new[] { nameof(LeaguesChecked) });
            }
            if (StartingDate > EndingDate && IsEndingDateSelected)
            {
                yield return new ValidationResult("Starting date cannot be after the ending date.", new[] { nameof(StartingDate), nameof(EndingDate) });
            }
            if ((EndingDate - StartingDate).TotalDays > 10 && IsEndingDateSelected)
            {
                yield return new ValidationResult("The timespan cannot be greater than 10 days.", new[] { nameof(StartingDate), nameof(EndingDate) });
            }
            if(StatusChecked.Contains(Status.IN_PLAY) && !StatusChecked.Contains(Status.PAUSED))
            {
                StatusChecked.Add(Status.PAUSED);
            }
        }
    }
}
