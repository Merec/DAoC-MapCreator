using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ImageMagick;

namespace MapCreator.Classes.MapCreation.Fixtures
{
    class FixtureDrawMultiple
    {
        private static List<Thread> threads = new List<Thread>();
        private static int maxThreads = 1;
        private static int runningThreads = 0;
        private static bool isFinished = false;

        private static object lockObject = null;

        private static List<MagickImage> models;
        public static List<MagickImage> Models
        {
            get { return FixtureDrawMultiple.models; }
            set { FixtureDrawMultiple.models = value; }
        }

        static FixtureDrawMultiple()
        {
            maxThreads = 6;
        }

        public static void AddModel(MagickImage model)
        {
            lock (lockObject)
            {
                models.Add(model);
            }
        }

    }
}
