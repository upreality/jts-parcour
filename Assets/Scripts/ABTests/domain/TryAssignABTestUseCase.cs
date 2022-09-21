using ABTests.domain.repositories;
using Zenject;
using static ABTests.domain.repositories.IUserABTestRepository;

namespace ABTests.domain
{
    public class TryAssignABTestUseCase
    {
        
        [Inject] private IUserABTestRepository userABTestRepository;
        [Inject] private AssignRandomABTestUseCase assignRandomABTestUseCase;
        public bool Try()
        {
            if (userABTestRepository.GetABTest(out var currentTest)) return false;
            var currentGroupState = userABTestRepository.GetABTestGroup(currentTest, out _);
            if(currentGroupState != UserABTestGroupState.NotAssigned) return false;
            assignRandomABTestUseCase.Assign();
            return true;
        }
    }
}