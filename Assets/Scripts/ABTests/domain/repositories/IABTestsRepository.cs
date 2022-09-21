using System.Collections.Generic;
using ABTests.domain.model;

namespace ABTests.domain.repositories
{
    public interface IABTestsRepository
    {
        public List<ABTest> GetTests();
    }
}