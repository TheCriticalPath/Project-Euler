using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
using System.Numerics;
namespace Problem_59
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public string _aff;
        public string _dic;
        NHunspell.Hunspell _hunspell;
        //TODO: Replace the project number, Title, and Description
        public int ID { get { return 59; } }
        public string Title { get { return "XOR Decryption"; } }
        public string Description
        {
            get
            {
                return @"Each character on a computer is assigned a unique code and the preferred standard is ASCII (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.
A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.
For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. The user would keep the encrypted message and the encryption key in different locations, and without both ""halves"", it is impossible to decrypt the message.
Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key.If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message.The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.
Your task has been made easy, as the encryption key consists of three lower case characters.Using cipher.txt(right click and 'Save Link/Target As...'), a file containing the encrypted ASCII codes, and the knowledge that the plain text must contain common English words, decrypt the message and find the sum of the ASCII values in the original text.";
            }
        }

        public string Name
        {
            get { return $"Problem {ID}: {Title}"; }
        }
        public EulerPlugin() { }
        private string GetFilePath(string strModifier = "", string defaultPath = "Input\\")
        {
            string path = defaultPath;
            while (!System.IO.File.Exists(path))
            {
                Helpers.InputHelper.Show(Name, $"Enter { strModifier } Path", ref defaultPath);
            }
            return path;
        }
        private long GetLimit(string strModifier = "", string defaultLimit = "1000")
        {
            long lngLimit = 0;
            string strLimit = defaultLimit;

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, $"Enter {strModifier} Limit", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }

        //TODO: Modify for the values in this routine to meet the needs of the specific problem
        public void PerformGetInput(IEulerPluginContext context)
        {
            _upperLimit = GetLimit("Key Length", "3");
            _aff = GetFilePath("AFF File", "Dictionary\\en_US\\en_us.aff");
            _dic = GetFilePath("DIC File", "Dictionary\\en_US\\en_us.dic");

            _hunspell = new NHunspell.Hunspell(_aff, _dic);
        }


        public Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            return Task.Factory.StartNew(() =>
           {
               // need a more elegant solution.
               DateTime dtStart, dtEnd;
               dtStart = DateTime.Now;
               Task<String> s = BruteForceAsync();
               dtEnd = DateTime.Now;
               context.strResultLongText = s.Result;
               context.spnDuration = dtEnd.Subtract(dtStart);
               return context;
           });
        }

        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = "";
            return context;
        }
        async Task<string> BruteForceAsync()
        {
            return BruteForce();
        }
        private string BruteForce()
        {
            List<char> input = FileHelper.GetFileAsCharArray("Input\\p059_cipher.txt", ",");
            string retval = new string(' ', input.Count);   // return string
            char[] ret = new char[input.Count];
            char pKey1 = char.MinValue;
            bool skipKey1 = false;
            long clearSum = 0;
            int i = 0;
            List<string> keys = StringHelper.Combinations("abcdefghijklmnopqrstuvwxyz", (int)_upperLimit);
            char l;
            i = 0;
            foreach (string key in keys)
            {
                retval = string.Empty;
                if (!skipKey1 || pKey1 != key[0])
                {
                    i = 0;
                    foreach (char c in input)
                    {
                        switch (i % 3)
                        {
                            case 0:
                                l = (char)(c ^ key[0]);
                                break;
                            case 1:
                                l = (char)(c ^ key[1]);
                                break;
                            case 2:
                                l = (char)(c ^ key[2]);
                                break;
                            default:
                                l = char.MinValue;
                                break;
                        }
                        retval = retval + l.ToString();
                        i++;
                    }
                    if (checkSpelling(retval)) {
                        foreach (char c in retval.ToCharArray()) {
                            clearSum += (int)c;
                        }
                        retval = $"Key: {key}; Sum of the Key: {(int)key[0] + (int)key[1] + (int)key[2] }; Sum of Text {clearSum}{System.Environment.NewLine}" + retval;
                        break;
                    }
                }
                pKey1 = key[0];
            }
            return retval;
        }

        private bool checkSpelling(string clear)
        {
            bool retVal = false;
            string[] words = clear.Split(' ');
            int correct = 0;
            int incorrect = 0;
            if (clear.Trim().Length == 0)
            {
                retVal = false;
            }
            else {
                foreach(string word in words)
                {
                    if (_hunspell.Spell(word))
                        correct++;
                    else
                        incorrect++;
                    if (incorrect > words.Length / 2 || correct > words.Length / 2)
                        break;
                }
            }
            if (correct > words.Length / 2)
                retVal = true;
            return retVal;
        }  
    }
}
