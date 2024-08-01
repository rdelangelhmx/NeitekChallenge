using Microsoft.AspNetCore.Components.Forms;

namespace Client.Helpers;

public class CustomFieldClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        return !editContext.GetValidationMessages(fieldIdentifier).Any() ? "is-valid" : "is-invalid";
    }
}