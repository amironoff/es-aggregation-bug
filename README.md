# es-aggregation-bug

1. Create an index using "001. CreateIndex.json"
2. Insert a couple of records using "002. InsertData.json"
3. Specify your ES server name in "AggregationTests"
4. Run the tests and observe the mismatch between aggs returned and aggs deserialized.

Fixed by the NEST team in 1.4! I can't help but to quote the release notes :) *"Andrey Mironoff deserves a special mention here for helping us catch an edge case aggregation parser bug in our bleeding edge builds by providing one of the best reproduce code we’ve ever seen, allowing us to fix the bug within hours."* - https://www.elastic.co/blog/nest-1-4-released. 
