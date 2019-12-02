using BlazingNotes.Editor.Options;
using System;

namespace BlazingNotes.Editor
{
	public class EditorModel
	{
        public EditorModel() { }

        public EditorModel(EditorOptions options) => Options = options;

        public string Id { get; set; } = $"Blazaco_{new Random().Next(0, 1000000).ToString()}";
        public EditorOptions Options { get; set; } = new EditorOptions();
    }
}
