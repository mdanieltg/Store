using System.ComponentModel.DataAnnotations;
using StoreWebAPI.Entities;

namespace StoreWebAPI.ValidationAttributes;

public class UserRoleAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userRole = value as string;
        if (userRole is null) return new ValidationResult("A user role is required");

        if (!Enum.IsDefined(typeof(UserRoles), userRole))
            return new ValidationResult("The role you provided is not valid.");

        return ValidationResult.Success;
    }
}
