mergeInto(LibraryManager.library, {
  Send: function(ename) {
      var me = UTF8ToString(ename);
      console.log('GA send event ' + me);
      gtag('event', me);
  },

  ActivateOptimize: function() {
    console.log('Activate optimize event');
	try {
		dataLayer.push({'event': 'optimize.activate'});
	} catch(e) { 
		console.log('Activate optimize event error ' + e);
	}
  }
});