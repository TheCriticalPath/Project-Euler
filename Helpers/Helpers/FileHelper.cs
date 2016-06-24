using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helpers
{
    public static class FileHelper
    {
        public static void WriteBinaryFile(List<char> input) {
            FileStream fs = new FileStream("C:\\TEMP\\BINARY.BIN",FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs,System.Text.Encoding.UTF8);
            foreach(char i in input) {
                bw.Write(i);
            }
            bw.Flush();
            bw.Close();
            bw.Dispose();
        }
        public static List<char> GetFileAsCharArray(string path, string delimiter)
        {
            List<string> input = GetFile(path, delimiter);
            List<char> output = new List<char>();
            char c = char.MinValue;
            foreach (string s in input)
            {
                c = (char)int.Parse(s);
                output.Add(c);
              
            }
            input.Clear();
            WriteBinaryFile(output);
            return output;
        }

        public static List<string> GetFile(string path, string delimiter) {
            List<string> input = new List<string>();
            List<string> output = new List<string>();

            input = GetFile(path);
            foreach (string s in input){
                output.AddRange(s.Split(delimiter.ToCharArray()));
            }
            return output;
        }

        public static List<string> GetFile(string path)
        {
            StreamReader reader = new StreamReader(path);
            List<string> file = new List<string>();
            try
            {
                do
                {
                    file.Add(reader.ReadLine().Trim());
                } while (reader.Peek() != -1);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                reader.Close();
            }
            return file;
        }
        public static string[] GetFileAsStringArray(string path)
        {
            return GetFile(path).ToArray();
        }

        public static string[] GetFileAsStringArray(string path, char[] delimiter) {
            List<string> str = GetFile(path);
            string[] arr = null;
            List<string> ret = new List<string>();

            for (int i = 0; i < str.Count; i++) {
                arr = str[i].Split(delimiter);
                for (int j = 0; j < arr.Length; j++) {
                    ret.Add(arr[j]);
                }
            }
            return ret.ToArray();
        }
    }
}
