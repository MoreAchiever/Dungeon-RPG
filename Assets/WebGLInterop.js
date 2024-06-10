//mergeInto(LibraryManager.library, {
//    onUnload: function() {
//        // Function called when the browser window is closed or refreshed
//        unityInstance.SendMessage("GameManager", "DeleteAllPlayerPrefs");
//    }
//});

window.addEventListener("beforeunload", function() {
    unityInstance.SendMessage("GameManager", "DeleteAllPlayerPrefs");
});

