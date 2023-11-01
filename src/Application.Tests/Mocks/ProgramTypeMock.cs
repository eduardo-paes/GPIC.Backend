using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class ProgramTypeMock
    {
        public static ProgramType MockValidProgramType() => new("Program Name", "Program Description");
    }
}