using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AlberEOL.CustomClasses
{
    public class Ini
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public Ini(string INIPath)
        {
            if (File.Exists(INIPath))
            {
                path = INIPath;
            }
            else
            {
                throw new Exception("Az ini fájl nem található!");
            }
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();

        }
    }
}
