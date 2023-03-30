using Fwks.AspNetCore.Transformers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Fwks.AspNetCore.Conventions;

public sealed class SlugCaseRouteTransformerConvention : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        action.RouteParameterTransformer = new SlugCaseParameterTransformer();
    }
}