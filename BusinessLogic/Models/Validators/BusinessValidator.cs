using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Validators
{
    public class BusinessValidator : Validator
    {
        public static string? IsEqualOrGreaterThanZero(decimal? value, string fieldName)
        {
            if (value < 0)
                return $"{fieldName} must be equal or greater than 0.";

            return null;
        }


        public static string? IsEqualOrGreaterThanZero(int? value, string fieldName)
        {
            if (value < 0)
                return $"{fieldName} must be equal or greater than 0.";

            return null;
        }


        public static string? IsGreaterThanZero(decimal? value, string fieldName)
        {
            if (value <= 0)
                return $"{fieldName} must be greater than 0.";

            return null;
        }


        public static string? IsGreaterThanZero(int? value, string fieldName)
        {
            if (value <= 0)
                return $"{fieldName} must be greater than 0.";

            return null;
        }


        public static string? IsSelected(int? value, string fieldName)
        {
            if (value == null)
                return $"{fieldName} must be selected.";

            return null;
        }

    }
}
