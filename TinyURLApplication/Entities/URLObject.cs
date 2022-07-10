using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyURLApplication.Entities
{
    //creating an url object containing both short and long url properties
    //creating an usage number propertie to the object to be used for the cache table
    public class URLObject
    {
        public string Shorturl { get; set; }
        public string Longurl { get; set; }

        //defineing the set method of the usage number as private - to not be able to change
        public int Usagenumber { get; private set; }

        public URLObject()
        {

        }
        public URLObject(string shorturl, string longurl)
        {
            if (shorturl.Length > longurl.Length)
            {
                Shorturl = longurl;
                Longurl = shorturl;
            }
            else
            {
                Shorturl = shorturl;
                Longurl = longurl;
            }
            Usagenumber = 0;
        }


        public URLObject GetUrlObject(string url)
        {
            Usagenumber++;
            return new URLObject(this.Shorturl, this.Longurl);

        }

    }


}

