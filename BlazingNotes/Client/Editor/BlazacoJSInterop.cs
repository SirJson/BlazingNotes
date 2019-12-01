using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazingNotes.Editor
{
    public static class BlazacoJSInterop
    {
        public static ValueTask<bool> InitializeEditor(IJSRuntime runtime, EditorModel editorModel)
            => runtime.InvokeAsync<bool>("BlazingNotes.Editor.InitializeEditor", new[] { editorModel });

        public static ValueTask<string> GetValue(IJSRuntime runtime, string id)
            => runtime.InvokeAsync<string>("BlazingNotes.Editor.GetValue", new[] { id });

        public static ValueTask<bool> SetValue(IJSRuntime runtime, string id, string value)
            => runtime.InvokeAsync<bool>("BlazingNotes.Editor.SetValue", new[] { id, value });

        public static ValueTask<bool> SetTheme(IJSRuntime runtime, string id, string theme)
            => runtime.InvokeAsync<bool>("BlazingNotes.Editor.SetTheme", new[] { id, theme });

        public static ValueTask<bool> Layout(IJSRuntime runtime, string id)
            => runtime.InvokeAsync<bool>("BlazingNotes.Editor.Layout", new[] { id });
    }
}
