using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Data;

public static class EmployeeSeed
{
    public static List<EmployeeEntity> Employees() =>
    [
        Create(1, "00000000-0000-0000-0000-000000000001", "Eleanor", "Vance", "eleanor.vance@example.com", "Executive", "Leadership", "Chief Executive Officer", null, 1, "https://i.pravatar.cc/300?u=eleanor.vance@example.com"),
        Create(2, "00000000-0000-0000-0000-000000000002", "Marcus", "Reed", "marcus.reed@example.com", "Engineering", "Leadership", "VP of Engineering", 1, 2, "https://i.pravatar.cc/300?u=marcus.reed@example.com"),
        Create(3, "00000000-0000-0000-0000-000000000003", "Priya", "Nair", "priya.nair@example.com", "Sales", "Leadership", "VP of Sales", 1, 3, "https://i.pravatar.cc/300?u=priya.nair@example.com"),
        Create(4, "00000000-0000-0000-0000-000000000004", "Sofia", "Alvarez", "sofia.alvarez@example.com", "Marketing", "Leadership", "Marketing Director", 1, 4, "https://i.pravatar.cc/300?u=sofia.alvarez@example.com"),
        Create(5, "00000000-0000-0000-0000-000000000005", "Grace", "Okafor", "grace.okafor@example.com", "Human Resources", "People Operations", "HR Manager", 1, 5, "https://i.pravatar.cc/300?u=grace.okafor@example.com"),
        Create(6, "00000000-0000-0000-0000-000000000006", "Daniel", "Kim", "daniel.kim@example.com", "Finance", "Accounting", "Finance Manager", 1, 6, "https://i.pravatar.cc/300?u=daniel.kim@example.com"),
        Create(7, "00000000-0000-0000-0000-000000000007", "Liam", "O'Brien", "liam.obrien@example.com", "Operations", "Facilities", "Operations Manager", 1, 7, "https://i.pravatar.cc/300?u=liam.obrien@example.com"),
        Create(8, "00000000-0000-0000-0000-000000000008", "Aisha", "Rahman", "aisha.rahman@example.com", "Engineering", "Platform", "Engineering Manager", 2, 8, "https://i.pravatar.cc/300?u=aisha.rahman@example.com"),
        Create(9, "00000000-0000-0000-0000-000000000009", "Tomas", "Novak", "tomas.novak@example.com", "Engineering", "Mobile", "Engineering Manager", 2, 9, "https://i.pravatar.cc/300?u=tomas.novak@example.com"),
        Create(10, "00000000-0000-0000-0000-000000000010", "Elena", "Petrova", "elena.petrova@example.com", "Engineering", "Platform", "Senior Software Engineer", 8, 10, "https://i.pravatar.cc/300?u=elena.petrova@example.com"),
        Create(11, "00000000-0000-0000-0000-000000000011", "David", "Chen", "david.chen@example.com", "Engineering", "Platform", "Software Engineer", 8, 11, "https://i.pravatar.cc/300?u=david.chen@example.com"),
        Create(12, "00000000-0000-0000-0000-000000000012", "Fatima", "Al-Sayed", "fatima.alsayed@example.com", "Engineering", "Platform", "Software Engineer", 8, 12, "https://i.pravatar.cc/300?u=fatima.alsayed@example.com"),
        Create(13, "00000000-0000-0000-0000-000000000013", "Jonas", "Weber", "jonas.weber@example.com", "Engineering", "Mobile", "Senior Software Engineer", 9, 13, "https://i.pravatar.cc/300?u=jonas.weber@example.com"),
        Create(14, "00000000-0000-0000-0000-000000000014", "Mei", "Lin", "mei.lin@example.com", "Engineering", "Mobile", "Software Engineer", 9, 14, "https://i.pravatar.cc/300?u=mei.lin@example.com"),
        Create(15, "00000000-0000-0000-0000-000000000015", "Carlos", "Mendes", "carlos.mendes@example.com", "Engineering", "Quality Assurance", "QA Engineer", 2, 15, "https://i.pravatar.cc/300?u=carlos.mendes@example.com"),
        Create(16, "00000000-0000-0000-0000-000000000016", "Hannah", "Brooks", "hannah.brooks@example.com", "Engineering", "Platform", "DevOps Engineer", 8, 16, "https://i.pravatar.cc/300?u=hannah.brooks@example.com"),
        Create(17, "00000000-0000-0000-0000-000000000017", "Omar", "Haddad", "omar.haddad@example.com", "Sales", "Enterprise", "Sales Director", 3, 17, "https://i.pravatar.cc/300?u=omar.haddad@example.com"),
        Create(18, "00000000-0000-0000-0000-000000000018", "Julia", "Santos", "julia.santos@example.com", "Sales", "Enterprise", "Account Executive", 17, 18, "https://i.pravatar.cc/300?u=julia.santos@example.com"),
        Create(19, "00000000-0000-0000-0000-000000000019", "Ben", "Carter", "ben.carter@example.com", "Sales", "SMB", "Account Executive", 3, 19, "https://i.pravatar.cc/300?u=ben.carter@example.com"),
        Create(20, "00000000-0000-0000-0000-000000000020", "Nina", "Kowalski", "nina.kowalski@example.com", "Sales", "Enterprise", "Sales Engineer", 17, 20, "https://i.pravatar.cc/300?u=nina.kowalski@example.com"),
        Create(21, "00000000-0000-0000-0000-000000000021", "Aaron", "Bell", "aaron.bell@example.com", "Marketing", "Content", "Content Strategist", 4, 21, "https://i.pravatar.cc/300?u=aaron.bell@example.com"),
        Create(22, "00000000-0000-0000-0000-000000000022", "Leila", "Hosseini", "leila.hosseini@example.com", "Marketing", "Growth", "Growth Marketer", 4, 22, "https://i.pravatar.cc/300?u=leila.hosseini@example.com"),
        Create(23, "00000000-0000-0000-0000-000000000023", "Sam", "Taylor", "sam.taylor@example.com", "Human Resources", "Talent Acquisition", "Technical Recruiter", 5, 23, "https://i.pravatar.cc/300?u=sam.taylor@example.com"),
        Create(24, "00000000-0000-0000-0000-000000000024", "Rachel", "Green", "rachel.green@example.com", "Finance", "Accounting", "Accountant", 6, 24, "https://i.pravatar.cc/300?u=rachel.green@example.com"),
        Create(25, "00000000-0000-0000-0000-000000000025", "Victor", "Nguyen", "victor.nguyen@example.com", "Operations", "Facilities", "Facilities Coordinator", 7, 25, "https://i.pravatar.cc/300?u=victor.nguyen@example.com"),
    ];

    private static EmployeeEntity Create(
        long id,
        string uniqueId,
        string firstName,
        string lastName,
        string email,
        string department,
        string subDepartment,
        string jobTitle,
        long? reportingTo,
        int? seatingPosition,
        string? avatarUrl)
    {
        var employee = new EmployeeEntity
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Department = department,
            SubDepartment = subDepartment,
            JobTitle = jobTitle,
            ReportingTo = reportingTo,
            SeatingPosition = seatingPosition,
            AvatarUrl = avatarUrl,
        };
        employee.SetId(id);
        employee.SetUniqueId(Guid.Parse(uniqueId));
        return employee;
    }
}
