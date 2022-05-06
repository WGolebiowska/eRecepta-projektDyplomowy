using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.Services.Helpers
{
    public static class RoleHelpers
    {
        public struct RolePair
        {
            public string Name { get; set; }
            public string Description { get; set; }
        };

        public static List<RolePair> Roles = new List<RolePair>()
        {
            new RolePair { Name = "administrator", Description = "Administrator"},
            new RolePair { Name = "doctor", Description = "Doctor" },
            new RolePair { Name = "patient", Description =  "Patient"},
        };
    }
}
