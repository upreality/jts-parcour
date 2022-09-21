mergeInto(LibraryManager.library, {
  Send: function(ename) {
      var me = UTF8ToString(ename);
      console.log('GA send event ' + me);
      gtag('event', me);
  }
  
  SetABGroupDimension: function(abGroup) {
      var abGroupText = UTF8ToString(abGroup);
      console.log('GA set AB dimension: ' + abGroupText);
      gtag('event', 'apply_dimension', { 'ab_test_group': abGroupText });
  }
});