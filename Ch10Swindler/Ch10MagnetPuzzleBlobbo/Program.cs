using System.IO;

namespace Ch10MagnetPuzzleBlobbo
{
    class Flobbo
    {
        public Flobbo(string zap)
        {
            this.zap = zap;
        }
        private string zap;
        public StreamWriter Snobbo()
        {
            return new StreamWriter("macaw.txt");
        }
        public bool Blobbo(StreamWriter sw)
        {
            sw.WriteLine(zap);
            zap = "green purple";
            return false;
        }
        public bool Blobbo(bool Already, StreamWriter sw)
        {
            if (Already)
            {
                sw.WriteLine(zap);
                sw.Close();
                return false;
            }
            else
            {
                sw.WriteLine(zap);
                zap = "red orange";
                return true;
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Flobbo f = new Flobbo("blue yellow");
            StreamWriter sw = f.Snobbo();
            f.Blobbo(f.Blobbo(f.Blobbo(sw), sw), sw);
        }
    }
}
