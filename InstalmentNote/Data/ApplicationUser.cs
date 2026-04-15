using Microsoft.AspNetCore.Identity;

namespace InstalmentNote.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public ICollection<LoanFacility> LoanFacilities { get; set; } = new List<LoanFacility>();
}

