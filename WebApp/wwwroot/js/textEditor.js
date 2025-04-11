
function initWysiwyg(wysiwygEditorId, wysiwygToolbarId, textareaId, content) {
    const textarea = document.querySelector(textareaId)

    const quill = new Quill(wysiwygEditorId, {
        modules: {
            syntax: true,
            toolbar: wysiwygToolbarId
        },

        placeholder: 'Type something',
        theme: 'snow'
    })

    if (content)
        quill.root.innerHTML = content;

    quill.on('text-change', () => {
        textarea.value = quill.root.innerHTML;
    })

    return quill;
}