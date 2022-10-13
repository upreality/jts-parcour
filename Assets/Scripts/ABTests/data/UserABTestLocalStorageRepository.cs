using ABTests.domain.repositories;
using ModestTree;
using Plugins.FileIO;
using static ABTests.domain.repositories.IUserABTestRepository;

namespace ABTests.data
{
    public class UserABTestLocalStorageRepository : IUserABTestRepository
    {
        private const string ABTestKey = "ABTestKey";
        private const string ABGroupKey = "ABGroupKey";

        public bool GetABTest(out string test)
        {
            test = null;
            if (!LocalStorageIO.HasKey(ABTestKey)) return false;
            test = LocalStorageIO.GetString(ABTestKey);
            return !test.IsEmpty();
        }

        public UserABTestGroupState GetABTestGroup(string test, out string group)
        {
            group = null;
            if (!LocalStorageIO.HasKey(ABTestKey)) return UserABTestGroupState.NotAssigned;
            var assignedTest = LocalStorageIO.GetString(ABTestKey);
            if (assignedTest != test) return UserABTestGroupState.AnotherTest;

            if (!LocalStorageIO.HasKey(ABGroupKey)) return UserABTestGroupState.Control;
            group = LocalStorageIO.GetString(ABGroupKey);
            return group.IsEmpty() ? UserABTestGroupState.Control : UserABTestGroupState.Assigned;
        }

        public void SetABTestControlGroup(string test)
        {
            LocalStorageIO.SetString(ABTestKey, test);
        }

        public void SetABTestGroup(string test, string group)
        {
            LocalStorageIO.SetString(ABTestKey, test);
            LocalStorageIO.SetString(ABGroupKey, group);
        }
    }
}