using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Validators
{
    public class DateValidator : Validator
    {
        public static string? DoDatesMatch(
    DateTime? earlierDate,
    DateTime? laterDate,
    string fieldFrom,
    string fieldTo)
        {
            if (earlierDate.HasValue && laterDate.HasValue)
            {
                if (laterDate.Value.Date < earlierDate.Value.Date)
                    return $"{fieldTo} cannot be earlier than {fieldFrom}.";
            }

            return null;
        }


    }
}