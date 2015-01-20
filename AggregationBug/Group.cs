using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregationBug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace GroupFeedr.Services
    {

        public class Group
        {

            /// <summary>
            /// Display name of the group, intended for humans.
            /// </summary>
            public string Name
            {
                get;
                set;
            }

            /// <summary>
            /// Group privacy settings.
            /// </summary>
            public string Privacy
            {
                get;
                set;
            }
        }
    }

}
