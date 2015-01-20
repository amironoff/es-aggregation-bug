using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;

namespace AggregationBug
{
    [TestClass]
    public class AggregationTests
    {
        [TestMethod]
        public void Should_Return_Computed_Aggregations_But_Will_Fail()
        {
            //Arrange
            SearchDescriptor <GroupPost> queryDescriptor = CreateQueryDescriptorWithRegionName("regions");
            ElasticClient searchClient = CreateSearchClient();
            int expectedAggregationCount = 3;//we registered 3 aggregations.

            //Act
            var searchResults = searchClient.Search<GroupPost>(queryDescriptor);

            //Assert
            Assert.AreEqual(expectedAggregationCount, searchResults.Aggregations.Count);
        }

        [TestMethod]
        public void Should_Return_Computed_Aggregations_But_Will_Fail_While_Returning_Less_Aggs()
        {
            //Arrange
            SearchDescriptor<GroupPost> queryDescriptor = CreateQueryDescriptorWithRegionName("region_names");
            ElasticClient searchClient = CreateSearchClient();
            int expectedAggregationCount = 3;//we registered 3 aggregations.

            //Act
            var searchResults = searchClient.Search<GroupPost>(queryDescriptor);

            //Assert
            Assert.AreEqual(expectedAggregationCount, searchResults.Aggregations.Count);

        }
        

        private SearchDescriptor<GroupPost> CreateQueryDescriptorWithRegionName(string regionName)
        {
            var returnValue = new SearchDescriptor<GroupPost>()
                .From(0)
                .Size(50)
                    .Query(queryDescriptor => queryDescriptor
                        .QueryString(fieldQuery => fieldQuery
                            .Query("*")
                            .AnalyzeWildcard()
                            .DefaultOperator(Operator.And)
                            .AllowLeadingWildcard(true)));
            //Specify which aggregations should be returned and apply filters.
            var aggregationDefinition = new AggregationDescriptor<GroupPost>();

            aggregationDefinition.Terms("group_privacy",
                terms => terms.Field(post => post.GroupOfOrigin.Privacy));

            aggregationDefinition.Terms("group_categories", terms => terms
                    .Field("group.categories")
                    .Exclude(String.Empty));

            

            //set up the region filter.
            var filterQueries = new Dictionary<string, string>()
                                                                   {
                                                                       {"Center","Center"},
                                                                       {"China Town","China Town"},
                                                                   };
            var filterFields = new[]
                                                                   {
                                                                       "message.original",
                                                                   };
            var filterDescriptors = new List<Func<FilterDescriptor<GroupPost>, FilterContainer>>();
            filterQueries.ToList().ForEach(item => filterDescriptors.Add(new Func<FilterDescriptor<GroupPost>, FilterContainer>(descriptor => descriptor
                .Name(item.Key)
                    .Query(q => q
                        .QueryString(qs => qs
                            .Query(item.Value)
                            .OnFields(filterFields))))));

            aggregationDefinition.Filters(regionName, filters =>
                filters.Filters(filterDescriptors.ToArray()));

            returnValue.Aggregations(_ => aggregationDefinition);

            return returnValue;
        }

        private ElasticClient CreateSearchClient()
        {
            var node = new Uri("http://insert-your-server-name-here:9200/");

            var settings = new ConnectionSettings(
                node,
                defaultIndex: "posts"
            );

            return new ElasticClient(settings);
        }
    }
}
