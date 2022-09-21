namespace ABTests.domain.repositories
{
    public interface IUserABTestRepository
    {
        bool GetABTest(out string test);
        UserABTestGroupState GetABTestGroup(string test, out string group);
        void SetABTestGroup(string test, string group);
        void SetABTestControlGroup(string test);

        public enum UserABTestGroupState
        {
            NotAssigned,
            AnotherTest,
            Control,
            Assigned
        }
    }
}