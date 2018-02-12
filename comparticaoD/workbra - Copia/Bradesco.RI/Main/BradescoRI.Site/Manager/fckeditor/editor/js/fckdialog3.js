document.body.className += ' ' + langDir ;

var cover = $( 'cover' ) ;
cover.style.backgroundColor = FCKConfig.BackgroundBlockerColor ;
FCKDomTools.SetOpacity( cover, FCKConfig.BackgroundBlockerOpacity ) ;