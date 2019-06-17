namespace iotc_csharp_service.Helpers
{

    public class VersionUtility
    {
        /// <summary>
        ///  Compare two version number formatted x.x.x
        ///  
        /// </summary>
        /// <param name="version1"></param>
        /// <param name="version2"></param>
        /// <returns></returns>
        public static int Compare(string version1, string version2)
        {

            string[] ver1 = version1.Split('.');
            string[] ver2 = version2.Split('.');
            int counter = 0;
            int len1 = ver1.Length;
            int len2 = ver2.Length;

            foreach (string v in ver1)
            {

                if (len1 > 0 && len2 > 0 && v.CompareTo(ver2[counter]) > 0)
                {
                    return 1;
                }
                else
                {
                    if (len1 > 0 && len2 > 0 && v.CompareTo(ver2[counter]) < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        len1--;
                        len2--;
                        counter++;
                    }

                }
            }

            if (ver1.Length > ver2.Length)
            {
                return 1;
            }
            else if (ver1.Length < ver2.Length)
            {
                return -1;
            }

            return 0;
        }
    }
}