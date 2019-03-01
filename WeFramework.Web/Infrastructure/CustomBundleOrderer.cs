using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace System.Web.Optimization
{
    public class CustomBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files.OrderBy(file => file.VirtualFile.Name.Length);
        }
    }

    public static class BundleExtensions
    {
        public static Bundle GeneralOrder(this Bundle bundle)
        {
            bundle.Orderer = new CustomBundleOrderer();
            return bundle;
        }
    }
}
