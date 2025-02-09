tinymce.PluginManager.add("gallery", function (editor, url) {
    editor.ui.registry.addButton('gallery', {
        tooltip: 'Insert/modify gallery',
        icon: './tinymce/plugins/gallery/img/icon.gif',
        onAction: function () {
            editor.windowManager.open({
                title: 'Insert gallery',
                url: './tinymce/plugins/gallery/gallery.html',
                width: 900,
                height: 600,
                body: {
                    type: 'panel',
                    items: [{ type: 'htmlpanel', html: '<p>Loading gallery...</p>' }]
                },
                buttons: [
                    {
                        text: 'Insert',
                        type: 'submit',
                        primary: true,
                        onAction: function (api) {
                            var iframe = api.getContentWindow();
                            var htmlContent = iframe.document.getElementById("htmlGallery").innerHTML;
                            editor.insertContent(htmlContent);
                            api.close();
                        }
                    },
                    {
                        text: 'Remove',
                        type: 'custom',
                        onAction: function () {
                            editor.windowManager.open({
                                title: "Remove gallery",
                                body: {
                                    type: "panel",
                                    items: [{ type: "htmlpanel", html: "<p>Are you sure you want to remove the gallery?</p>" }]
                                },
                                buttons: [
                                    {
                                        text: "Yes",
                                        type: "submit",
                                        primary: true,
                                        onAction: function (api) {
                                            prepareHTML(editor);
                                            api.close();
                                        }
                                    },
                                    {
                                        text: "No",
                                        type: "cancel",
                                        onAction: function (api) {
                                            api.close();
                                        }
                                    }
                                ]
                            });
                        }
                    },
                    {
                        text: 'Cancel',
                        type: 'cancel'
                    }
                ]
            });
        }
    });
});

function prepareHTML(editor) {
    var element = editor.selection.getNode();
    while (element && element.nodeName !== "BODY") {
        if (element.nodeName === "DIV" && element.getAttribute("id") === "slides") {
            element.parentNode.removeChild(element);
            break;
        }
        element = element.parentNode;
    }
}
