mergeInto(LibraryManager.library, {
  Send: function(ename) {
      var me = UTF8ToString(ename);
      console.log('GA send event ' + me);
      gtag('event', me);
  }
});