using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Validators
{
    public class StringValidator : Validator
    {
        public static string StartsWithCapitalLetter(string word)
        {
            try
            {
                if (!char.IsUpper(word, 0))
                {
                    return "Must start with capital letter";
                }
            }
            catch (Exception) { }
            ; return null;
        }

       
        public static string IsTwoChar(string word)
        {
            try
            {
                if (word != null)
                {
                    if (word.Length != 2)
                    {
                        return "Must have 2 characters";
                    }
                }
            }
            catch (Exception) { }
            ; return null;
        }
        public static string IsMailFormatCorrect(string word)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    string correctFormat = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
                    //@"..." - dosłownie
                    //[A-Za-z0-9._%+-] - dowolne znaki z tego zakresu
                    //@[A-Za-z0-9.-] - @ i po niej dowolne znaki z tego zakresu
                    //\. - jedna kropka
                    //[A-Za-z]{2,4} - dowolne od 2 do 4 znaków z tego zakresu
                    Regex regex = new Regex(correctFormat);
                    if (!regex.IsMatch(word))
                    {
                        return "Email format must be corrected";
                    }
                }
            }
            catch (Exception) { }
            ; return null;
        }
    }
}

