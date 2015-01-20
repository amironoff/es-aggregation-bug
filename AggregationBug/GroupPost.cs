using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationBug.GroupFeedr.Services;
using Nest;

namespace AggregationBug
{
    /// <summary>
    /// Represents a single group post
    /// </summary>
    [ElasticType(Name = "feed-post")]
    public class GroupPost
    {

        /// <summary>
        /// Group, where the listing was posted.
        /// </summary>
        [ElasticProperty(Name = "group")]
        public Group GroupOfOrigin { get; set; }

        public GroupPost()
        {
            GroupOfOrigin = new Group();
        }
    }
}
