using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class ActivityTypeMock
    {
        public static ActivityType MockValidActivityType()
        {
            return new ActivityType("Activity Type Name", "Activity Type Description", Guid.NewGuid());
        }
    }
}
