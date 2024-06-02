using System;
namespace ecommerceRestApi.Util
{
	public static class Utils
	{
        public static bool ContainsLetterAndDigit(string input)
        {
            // Harf ve rakam kontrolü için değişkenler
            bool hasLetter = false;
            bool hasDigit = false;

            // Stringi karakter karakter kontrol et
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }

                // Eğer her iki durumu da bulduysak, daha fazla kontrol yapmaya gerek yok
                if (hasLetter && hasDigit)
                {
                    return true;
                }
            }

            // Eğer döngü tamamlandıktan sonra bir harf ve bir rakam bulunamadıysa false döner
            return false;
        }
    }
}

