# es-aggregation-bug

1. Create an index using "001. CreateIndex.json"
2. Insert a couple of records using "002. InsertData.json"
3. Specify your ES server name in "AggregationTests"
4. Run the tests and observe the mismatch between aggs returned and aggs deserialized.
