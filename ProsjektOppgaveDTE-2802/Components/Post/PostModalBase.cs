using Microsoft.AspNetCore.Components;
using ProsjektOppgaveBlazor.Data.CommonModels;

namespace ProsjektOppgaveBlazor.Components.Post;

public abstract class PostModalBase: ComponentBase
{
    public abstract void SelectTag(TagViewModel tagViewModel);

    public abstract void UnselectTag(TagViewModel tagViewModel);
}