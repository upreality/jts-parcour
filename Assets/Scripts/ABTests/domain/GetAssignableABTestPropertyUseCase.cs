using System.Linq;
using ABTests.domain.repositories;
using Zenject;
using static ABTests.domain.repositories.IUserABTestRepository;

namespace ABTests.domain
{
    public class GetAssignableABTestPropertyUseCase
    {
        [Inject] private IUserABTestRepository userABTestRepository;
        [Inject] private IABTestsRepository abTestsRepository;

        public bool GetAssignableProperty(out string propertyName)
        {
            propertyName = null;
            if (!userABTestRepository.GetABTest(out var test)) return false;
            var groupState = userABTestRepository.GetABTestGroup(test, out var group);
            if (groupState != UserABTestGroupState.Assigned) return false;

            var clientTest = abTestsRepository.GetTests().FirstOrDefault(abTest => abTest.testName == test);
            if (clientTest == null) return false;

            var clientGroup = clientTest.groups.FirstOrDefault(testGroup => testGroup.groupName == group);
            if (clientGroup == null) return false;

            propertyName = clientGroup.property;
            return true;
        }
    }
}