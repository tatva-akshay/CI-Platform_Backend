using System.ComponentModel.DataAnnotations;
using CI_Platform_Backend_DBEntity.Context;

namespace CI_Platform_Backend_Utilities.Validators;

public class CountryValidatorAttribute : ValidationAttribute
{
    private readonly ApplicationDbContext _dbContext;

    public CountryValidatorAttribute(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override bool IsValid(object value)
	{
		if (_dbContext.Countries.Any(x=>x.CountryId.ToString() == value.ToString()))
			return true;
	
		return false;
	}
}
