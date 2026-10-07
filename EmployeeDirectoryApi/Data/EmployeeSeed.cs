using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Data;

public static class EmployeeSeed
{
    private static readonly string[] DepartmentNames =
    [
        "Accounts",
        "CEO",
        "Client Service",
        "Content",
        "Copy",
        "Creative",
        "CRM & Catalogue",
        "CTO",
        "Customer Relations",
        "Design",
        "Dev",
        "HR",
        "Management",
        "Media",
        "Operations",
        "Project",
        "Project Management",
        "Project/Strategy",
        "Strategy",
        "Technology",
        "Traffic Manager"
    ];

    // (SubDepartment, JobTitle, manager row index). Row indices are 1-based into this array;
    // 0 means no manager (the root/CEO).
    private static readonly (string SubDepartment, string JobTitle, int Manager)[] Roster =
    [
        ("Accounts", "Accounts Director", 2),                 // 1
        ("CEO", "CEO", 0),                                    // 2
        ("Client Service", "Account Manager", 4),             // 3
        ("Client Service", "Business Unit Director", 2),      // 4
        ("Content", "Digital Campaign Manager", 46),          // 5
        ("Copy", "Digital Copywriter", 10),                   // 6
        ("Copy", "Digital Copywriter", 10),                   // 7
        ("Copy", "Junior Copywriter", 10),                    // 8
        ("Creative", "Senior Designer", 10),                  // 9
        ("Creative", "Creative Director", 2),                 // 10
        ("Creative", "Creative/Visual Designer", 10),         // 11
        ("Creative", "UX Designer", 10),                      // 12
        ("Creative", "UX Designer", 10),                      // 13
        ("Creative", "UX Designer", 10),                      // 14
        ("Creative", "Lead Creative Designer", 10),           // 15
        ("Creative", "Senior Digital UX/UI Designer", 10),    // 16
        ("Creative", "Junior Conceptual Assistant", 10),      // 17
        ("Creative", "Graphic Desigber", 10),                 // 18
        ("Creative", "Senior Designer", 10),                  // 19
        ("Creative", "Creative Director", 2),                 // 20
        ("CTO", "CTO", 2),                                    // 21
        ("Customer Relations", "CRM Manager", 2),             // 22
        ("Design", "Photographer/Retoucher", 10),             // 23
        ("Design", "Junior Creative", 10),                    // 24
        ("Dev", "Front End Developer", 21),                   // 25
        ("Dev", "Front End Developer", 21),                   // 26
        ("Dev", "Front End Developer", 21),                   // 27
        ("Dev", "Junior Developer", 21),                      // 28
        ("Dev", "Junior Developer", 21),                      // 29
        ("Dev", "Intern Front-end Developer", 21),            // 30
        ("Dev", "Full Stack Developer", 21),                  // 31
        ("Dev", "Full Stack Developer", 21),                  // 32
        ("Dev", "Back End Software Developer", 21),           // 33
        ("Dev", "Shopify Technical Lead", 21),                // 34
        ("Dev", "Front End Developer", 21),                   // 35
        ("Dev", "Technical Project Manager", 21),             // 36
        ("Dev", "Junior Backend Developer", 21),              // 37
        ("Dev", "Junior Front-End Developer", 21),            // 38
        ("Dev", "Quality Assurance Tester", 21),              // 39
        ("HR", "Recruitment Manager", 2),                     // 40
        ("Management", "Retail Strategy Director", 2),        // 41
        ("Management", "Executive Assistant", 2),             // 42
        ("Media", "Digital Marketing Strategist", 21),        // 43
        ("Media", "Paid Media Specialist", 46),               // 44
        ("Media", "Digital Campaign Manager", 46),            // 45
        ("Media", "Head Of Paid Performance Media", 41),      // 46
        ("Media", "Media Campaign Manager", 46),              // 47
        ("Media", "Web Quality Assurance Tester", 21),        // 48
        ("Operations", "Traffic Manager", 4),                 // 49
        ("Project", "Head of Operations", 2),                 // 50
        ("Project", "Project Manager", 41),                   // 51
        ("Project", "Product Listing Specialist", 21),        // 52
        ("Project", "Account Assistant", 58),                 // 53
        ("Project Management", "Account Manager", 22),        // 54
        ("Project Management", "Account Director", 41),       // 55
        ("Project Management", "Account Manager", 55),        // 56
        ("Project Management", "Account Executive", 51),      // 57
        ("Project Management", "Account Manager", 41),        // 58
        ("Project Management", "Account Manager", 4),         // 59
        ("Project/Strategy", "Project Co-ordinator", 4),      // 60
        ("Strategy", "Business Intelligence Analyst", 22),    // 61
        ("Traffic Manager", "Traffic Manager", 50),           // 62
        ("CRM & Catalogue", "Catalog & Product Manager", 52)  // 63
    ];

    private static readonly (string First, string Last, string Email)[] DummyPeople =
    [
        ("Eleanor", "Vance", "eleanor.vance@example.com"),
        ("Marcus", "Reed", "marcus.reed@example.com"),
        ("Priya", "Nair", "priya.nair@example.com"),
        ("Sofia", "Alvarez", "sofia.alvarez@example.com"),
        ("Grace", "Okafor", "grace.okafor@example.com"),
        ("Daniel", "Kim", "daniel.kim@example.com"),
        ("Liam", "O'Brien", "liam.obrien@example.com"),
        ("Aisha", "Rahman", "aisha.rahman@example.com"),
        ("Tomas", "Novak", "tomas.novak@example.com"),
        ("Elena", "Petrova", "elena.petrova@example.com"),
        ("David", "Chen", "david.chen@example.com"),
        ("Fatima", "Al-Sayed", "fatima.alsayed@example.com"),
        ("Jonas", "Weber", "jonas.weber@example.com"),
        ("Mei", "Lin", "mei.lin@example.com"),
        ("Carlos", "Mendes", "carlos.mendes@example.com"),
        ("Hannah", "Brooks", "hannah.brooks@example.com"),
        ("Omar", "Haddad", "omar.haddad@example.com"),
        ("Julia", "Santos", "julia.santos@example.com"),
        ("Ben", "Carter", "ben.carter@example.com"),
        ("Nina", "Kowalski", "nina.kowalski@example.com"),
        ("Aaron", "Bell", "aaron.bell@example.com"),
        ("Leila", "Hosseini", "leila.hosseini@example.com"),
        ("Sam", "Taylor", "sam.taylor@example.com"),
        ("Rachel", "Green", "rachel.green@example.com"),
        ("Victor", "Nguyen", "victor.nguyen@example.com")
    ];

    private static readonly string[] ExtraFirstNames =
    [
        "Noah", "Mia", "Ethan", "Ava", "Lucas", "Isabella",
        "Mason", "Sophia", "Logan", "Amelia"
    ];

    private static readonly string[] ExtraLastNames =
    [
        "Anderson", "Bennett", "Diaz", "Evans", "Foster",
        "Garcia", "Hughes", "Ibrahim", "Jackson", "Klein"
    ];

    public static SeedData Build()
    {
        var departments = new List<DepartmentEntity>();
        var departmentIdByName = new Dictionary<string, long>();

        for (var i = 0; i < DepartmentNames.Length; i++)
        {
            var department = new DepartmentEntity(DepartmentNames[i]);
            department.SetId(i + 1);
            department.SetUniqueId(Guid.NewGuid());

            departments.Add(department);
            departmentIdByName[DepartmentNames[i]] = department.Id;
        }

        // A subdepartment shares its name (and id) with the department of the same name.
        var subDepartments = new List<SubDepartmentEntity>();
        var subDepartmentIdByName = new Dictionary<string, long>();

        for (var i = 0; i < DepartmentNames.Length; i++)
        {
            var name = DepartmentNames[i];
            var subDepartment = new SubDepartmentEntity(name, departmentIdByName[name]);
            subDepartment.SetId(i + 1);
            subDepartment.SetUniqueId(Guid.NewGuid());

            subDepartments.Add(subDepartment);
            subDepartmentIdByName[name] = subDepartment.Id;
        }

        var rows = Roster
            .Select((entry, index) => new RosterRow(index + 1, entry.SubDepartment, entry.JobTitle, entry.Manager))
            .ToList();

        // A position is a group of employees sharing (subdepartment, job title, manager).
        var groups = rows
            .GroupBy(row => (row.SubDepartment, row.JobTitle, row.Manager))
            .Select((group, index) => new
            {
                PositionId = (long)(index + 1),
                group.Key.SubDepartment,
                group.Key.JobTitle,
                group.Key.Manager,
                RowIndices = group.Select(row => row.Index).ToList()
            })
            .ToList();

        var positionIdByRow = new Dictionary<int, long>();
        foreach (var group in groups)
        {
            foreach (var rowIndex in group.RowIndices)
            {
                positionIdByRow[rowIndex] = group.PositionId;
            }
        }

        var random = new Random(20261006);
        var positions = new List<EmployeePositionEntity>();
        var positionParentByPositionId = new Dictionary<long, long>();

        foreach (var group in groups)
        {
            var position = new EmployeePositionEntity(
                group.JobTitle,
                departmentIdByName[group.SubDepartment],
                subDepartmentIdByName[group.SubDepartment],
                null, // parent is wired after the positions are inserted (avoids self-FK insert ordering)
                random.Next(1, 500));

            position.SetId(group.PositionId);
            position.SetUniqueId(Guid.NewGuid());

            positions.Add(position);

            if (group.Manager != 0)
            {
                positionParentByPositionId[group.PositionId] = positionIdByRow[group.Manager];
            }
        }

        var employees = new List<EmployeeEntity>();
        foreach (var row in rows)
        {
            var (firstName, lastName, email) = IdentityFor(row.Index);

            var employee = new EmployeeEntity(
                firstName,
                lastName,
                email,
                positionIdByRow[row.Index],
                $"https://i.pravatar.cc/300?u={email}");

            employee.SetId(row.Index);
            employee.SetUniqueId(Guid.NewGuid());

            employees.Add(employee);
        }

        return new SeedData
        {
            Departments = departments,
            SubDepartments = subDepartments,
            Positions = positions,
            Employees = employees,
            PositionParentByPositionId = positionParentByPositionId
        };
    }

    private static (string First, string Last, string Email) IdentityFor(int rowIndex)
    {
        if (rowIndex <= DummyPeople.Length)
        {
            return DummyPeople[rowIndex - 1];
        }

        var offset = rowIndex - DummyPeople.Length - 1;
        var first = ExtraFirstNames[offset % ExtraFirstNames.Length];
        var last = ExtraLastNames[offset / ExtraFirstNames.Length % ExtraLastNames.Length];
        var email = $"employee{rowIndex}@example.com";

        return (first, last, email);
    }

    private sealed record RosterRow(int Index, string SubDepartment, string JobTitle, int Manager);
}

public sealed class SeedData
{
    public required List<DepartmentEntity> Departments { get; init; }
    public required List<SubDepartmentEntity> SubDepartments { get; init; }
    public required List<EmployeePositionEntity> Positions { get; init; }
    public required List<EmployeeEntity> Employees { get; init; }
    public required Dictionary<long, long> PositionParentByPositionId { get; init; }
}
