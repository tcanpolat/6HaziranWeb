namespace _07_CustomHelpers.Helpers
{
    public static class StringHelper
    {
        // Gelen metindeki ilk harfi büyük geri kalanını küçük yapan method
        public static string CapitalizeForFirstLetter(string input) 
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        // Cümledeki her kelimenin ilk harfini büyük yapıp geri kalanını küçük harf yapan method
        public static string CapitalizeWord(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string[] words = input.Split(' '); // hava çok güzel => [hava,çok,güzel]

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CapitalizeForFirstLetter(words[i]); // [Hava, Çok, Güzel]
            }

            return string.Join(" ", words);
        }

    }
}
