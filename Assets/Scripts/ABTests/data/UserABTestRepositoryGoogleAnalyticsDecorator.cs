using ABTests.domain.repositories;
using Zenject;
using static ABTests.domain.repositories.IUserABTestRepository;

namespace ABTests.data
{
    public class UserABTestRepositoryGoogleAnalyticsDecorator: IUserABTestRepository
    {
        [Inject] private IUserABTestRepository target;
        public bool GetABTest(out string test) => target.GetABTest(out test);

        public UserABTestGroupState GetABTestGroup(string test, out string group) => target
            .GetABTestGroup(test, out group);

        public void SetABTestGroup(string test, string group)
        {
            GoogleAnalyticsSDK.SetABGroup();
            target.SetABTestGroup(test, group);
        }

        public void SetABTestControlGroup(string test)
        {
            
            target.SetABTestControlGroup(test);
        }
    }
}