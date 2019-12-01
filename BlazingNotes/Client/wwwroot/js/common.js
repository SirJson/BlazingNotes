
window.BlazingNotes = window.BlazingNotes || {};
window.BlazingNotes.Editors = [];

window.BlazingNotes.Editor = {
    InitializeEditor: function (model) {
        let thisEditor = monaco.editor.create(document.getElementById(model.id), model.options);
        if (window.BlazingNotes.Editors.find(e => e.id === model.id)) {
            return false;
        }
        else {
            window.BlazingNotes.Editors.push({ id: model.id, editor: thisEditor });
        }
        return true;
    },
    GetValue: function (id) {
        let myEditor = window.BlazingNotes.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.BlazingNotes.Editors.length}' '${id}'`;
        }
        return myEditor.editor.getValue();
    },
    SetValue: function (id, value) {
        let myEditor = window.BlazingNotes.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.BlazingNotes.Editors.length}' '${id}'`;
        }
        myEditor.editor.setValue(value);
        return true;
    },
    SetTheme: function (id, theme) {
        let myEditor = window.BlazingNotes.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.BlazingNotes.Editors.length}' '${id}'`;
        }
        monaco.editor.setTheme(theme);
        return true;
    },
    Layout: function (id) {
        let myEditor = window.BlazingNotes.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.BlazingNotes.Editors.length}' '${id}'`;
        }
        monaco.editor.layout()
        return true;
    }
}
